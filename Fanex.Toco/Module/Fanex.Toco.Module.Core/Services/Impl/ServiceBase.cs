namespace Fanex.CustomerSetting.Module.Core.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Criteria;
    using Data;
    using Fanex.Caching;
    using Fanex.Common.Helper;
    using Fanex.CustomerSetting.Module.Core.Constants;
    using Fanex.CustomerSetting.Module.Core.Repository;
    using Fanex.CustomerSetting.Module.Core.Services.Interface;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.NAP.Core.Web.Context;
    using Fanex.NAP.Core.Web.Session;
    using Fanex.NAP.Framework.Data;
    using Fanex.RemotingRef;
    using Models;
    using NAP.Core.Context;

    public class ServiceBase : IServiceBase
    {
        #region generated code
        protected readonly IDynamicRepository DynamicRepository;

        protected readonly ICache _cacheManager;

        protected readonly BaseRepository _repository;

        protected readonly Web.ICurrentContext _context;

        protected readonly Web.ICurrentSession _session;
        #endregion
        public ServiceBase(IDynamicRepository repository = null)
        {
            this.DynamicRepository = repository ?? new DynamicRepository();
            this._cacheManager = CacheService.CreateInstance();
            this._repository = new BaseRepository();
            this._context = this._context ?? new Web.CurrentContext();
        }

        protected IDynamicRepository Repository
        {
            get { return this.DynamicRepository; }
        }

        public void HandleException(Exception ex)
        {
            LoggingHandler.Log(ex);
        }

        public virtual IEnumerable<T> GetListData<T>(ICriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                return DynamicRepository.Fetch<T>(criteria);
            }

            return new List<T>();
        }

        public Result CheckSpecialSitePermission(CheckSpecialSitePermissionCriteria criteria)
        {
            Result result = new Result();
            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSpParams());
                db.ExecuteNonQuery();
                var outputs = db.GetOuputParams();
                result.ErrorCode = Convert.IsDBNull(outputs["errcode"]) ? 0 : Convert.ToInt32(outputs["errcode"]);
                result.ErrorMessage = Convert.IsDBNull(outputs["errmsg"]) ? string.Empty : outputs["errmsg"].ToString();
            }

            return result;
        }

        public CheckCustomerBelongToSpecialSiteResult CheckCustomerBelongToSpecialSite(CheckCustomerIsSpecialSiteCriteria criteria)
        {
            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSpParams());
                db.ExecuteNonQuery();
                var outputs = db.GetOuputParams();
                return new CheckCustomerBelongToSpecialSiteResult(
                    Convert.IsDBNull(outputs["isspecialsite"]) ? false : Convert.ToBoolean(outputs["isspecialsite"]),
                    Convert.IsDBNull(outputs["isspecialheadline"]) ? false : Convert.ToBoolean(outputs["isspecialheadline"]),
                    Convert.IsDBNull(outputs["site"]) ? string.Empty : outputs["site"].ToString());
            }
        }

        public CheckCustomerIsInternalAdminResult CheckCustomerIsInternalAdmin(CheckCustomerIsInternalAdminCriteria criteria)
        {
            CheckCustomerIsInternalAdminResult result = null;
            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSpParams());
                db.ExecuteNonQuery();
                var outputs = db.GetOuputParams();
                return new CheckCustomerIsInternalAdminResult(
                    Convert.IsDBNull(outputs["isinternaladmin"]) ? false : Convert.ToBoolean(outputs["isinternaladmin"]),
                    Convert.IsDBNull(outputs["isallowaccess"]) ? false : Convert.ToBoolean(outputs["isallowaccess"]));
            }
        }

        public bool CheckCustomerHasSpecialSitePermission(CheckCustomerHasSpecialSiteFunctionPermissionCriteria criteria)
        {
            bool result = false;
            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSpParams());
                db.ExecuteNonQuery();
                var outputs = db.GetOuputParams();
                result = Convert.IsDBNull(outputs["errcode"]) ? true : Convert.ToInt32(outputs["errcode"]) == 0;
            }

            return result;
        }

        public Result CheckUM(int custId, NonDcsProduct product, string langKey)
        {
            var criteria = new CheckUmCriteria(custId, (byte)product, langKey);
            if (!criteria.IsValid())
            {
                return new Result(500, $"Invalid request custId: {custId}");
            }

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());

                var errMsg = Utils.ToString(db.GetParameterValue("errmsg"));
                var formatedErrMsg = string.Format(errMsg, 
                    CacheRef.GetRefResourceByRefIdAndResourceId(
                        "Product", 
                        ((byte)product).ToString(),
                        product.ToString()));

                return new Result(Utils.ToInt32(db.GetParameterValue("errcode")), formatedErrMsg);
            }
        }
    }
}