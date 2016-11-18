namespace Fanex.CustomerSetting.Module.NG2.Core.Services.Impl
{
    using Criteria;
    using Data;
    using Data.Repository;
    using Fanex.Caching;
    using Fanex.Common.Helper;
    using Fanex.CustomerSetting.Module.NG2.Core.Repository;
    using Fanex.CustomerSetting.Module.NG2.Core.Services.Interface;
    using Fanex.NAP.Core.Web.Context;
    using Fanex.NAP.Core.Web.Session;
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
    }
}