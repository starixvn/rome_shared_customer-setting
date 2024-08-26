namespace Fanex.CustomerSetting.Module.Core.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
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

        protected readonly ICacheService _cacheManager;
        protected readonly BaseRepository _repository;

        protected readonly Web.ICurrentContext _context;

        protected readonly Web.ICurrentSession _session;
        #endregion
        public ServiceBase(IDynamicRepository repository = null)
        {
            this.DynamicRepository = repository ?? new DynamicRepository();
            this._cacheManager = new CacheService();
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
            criteria.DeniedList = Acc_DeniedSiteCurrency_Sel(0);
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

        public string Acc_DeniedSiteCurrency_Sel(int mode)
        {
            Acc_DeniedSiteCurrency_Sel criteria = new Acc_DeniedSiteCurrency_Sel()
            {
                mode = mode
            };

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.AssignParamValues(criteria.GetSpParams());
                objDb.ExecuteNonQuery();
                return objDb.GetOuputParams()["deniedlist"].ToString();
            }
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

        public void AddMsgAfterCreateNewSMA(AddMsgAfterCreateNewSMACriteria criteria)
        {
            Task.Factory.StartNew(Task_AddMsgAfterCreateNewSMA, criteria);
        }

        private void Task_AddMsgAfterCreateNewSMA(object criteriaObject)
        {
            try
            {
                Check.Argument.IsNotNull(criteriaObject, nameof(criteriaObject));
                AddMsgAfterCreateNewSMACriteria criteria = criteriaObject as AddMsgAfterCreateNewSMACriteria;
                Check.Argument.IsNotNegativeOrZero(criteria.CustId, nameof(criteria.CustId));
                int errCode;
                string errMsg;
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    db.ExecuteNonQuery(criteria.GetSpParams());
                    var outputs = db.GetOuputParams();
                    errCode = Utils.ToInt32(outputs["errcode"]);
                    errMsg = Utils.ToString(outputs["errmsg"]);
                }

                if (errCode > 0 || !string.IsNullOrWhiteSpace(errMsg))
                {
                    LoggingHandler.Log(string.Format("[Age_MsgCustReadStatus_Ins] CustId: {0}; SubAccId: {1}; RoleId: {2} ErrCode: {3}; ErrMsg: {4}", 
                        criteria.CustId,
                        criteria.SubAccId,
                        criteria.Roleid,
                        errCode, 
                        errMsg) , LogType.Info);
                }
            }
            catch (Exception ex)
            {
                LoggingHandler.Log(ex);
            }
        }
    }
}