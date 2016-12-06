namespace Fanex.CustomerSetting.Module.Core.Services.Impl
{
    using Criteria;
    using Data;
    using Fanex.Caching;
    using Fanex.Common.Helper;
    using Fanex.CustomerSetting.Module.Core.Repository;
    using Fanex.CustomerSetting.Module.Core.Services.Interface;
    using Fanex.NAP.Core.Web.Context;
    using Fanex.NAP.Core.Web.Session;
    using Fanex.NAP.Framework.Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class ServiceBase : IServiceBase
    {
        #region generated code
        protected readonly IDynamicRepository DynamicRepository;

        protected readonly ICache _cacheManager;

        protected readonly BaseRepository _repository;

        protected readonly ICurrentContext _context;

        protected readonly ICurrentSession _session;
        #endregion
        public ServiceBase(IDynamicRepository repository = null)
        {
            this.DynamicRepository = repository ?? new DynamicRepository();
            this._cacheManager = CacheService.CreateInstance();
            this._repository = new BaseRepository();
            this._context = this._context ?? new CurrentContext();
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

        public void CheckCustomerBelongToSpecialSite(CheckCustomerIsSpecialSiteCriteria criteria, ref bool isSpecialSite, ref bool isSpecialHeadline)
        {
            bool result = false;
            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSpParams());
                db.ExecuteNonQuery();
                var outputs = db.GetOuputParams();
                isSpecialSite = Convert.IsDBNull(outputs["isspecialsite"]) ? false : Convert.ToBoolean(outputs["isspecialsite"]);
                isSpecialHeadline = Convert.IsDBNull(outputs["isspecialheadline"]) ? false : Convert.ToBoolean(outputs["isspecialheadline"]);
            }
        }

        public void CheckCustomerIsInternalAdmin(CheckCustomerIsInternalAdminCriteria criteria, ref bool isInternalAdmin, ref bool isAllowAccess)
        {
            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSpParams());
                db.ExecuteNonQuery();
                var outputs = db.GetOuputParams();
                isInternalAdmin = Convert.IsDBNull(outputs["isinternaladmin"]) ? false : Convert.ToBoolean(outputs["isinternaladmin"]);
                isAllowAccess = Convert.IsDBNull(outputs["isallowaccess"]) ? false : Convert.ToBoolean(outputs["isallowaccess"]);
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
    }
}