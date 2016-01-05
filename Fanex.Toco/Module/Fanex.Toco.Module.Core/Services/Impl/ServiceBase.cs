namespace Fanex.CustomerSetting.Module.Core.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Caching;
    using Fanex.Common.Helper;
    using Fanex.CustomerSetting.Module.Core.Repository;
    using Fanex.CustomerSetting.Module.Core.Services.Interface;
    using Fanex.NAP.Core.Web.Context;
    using Fanex.NAP.Core.Web.Session;
    using Fanex.NAP.Framework.Data;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class ServiceBase : IServiceBase
    {
        protected readonly IDynamicRepository DynamicRepository;

        protected readonly ICache _cacheManager;

        protected readonly BaseRepository _repository;

        protected readonly ICurrentContext _context;

        protected readonly ICurrentSession _session;

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
    }
}