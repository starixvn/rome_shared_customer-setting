// <copyright file="ServiceBase.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/07">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Services
{
    using System;
    using System.Collections.Generic;
    using Fanex.Caching;
    using Fanex.Common.Helper;
    using Fanex.CustomerSetting.Module.Core.Repository;
    using Fanex.CustomerSetting.Module.Core.Services.Interfaces;
    using Fanex.NAP.Core.Web.Context;
    using Fanex.NAP.Core.Web.Session;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// The service base.
    /// </summary>
    public class ServiceBase : IServiceBase
    {
        protected readonly IDynamicRepository _DynamicRepository;
        protected readonly ICache _CacheManager;
        protected readonly BaseRepository _Repository;
        protected readonly ICurrentContext _Context;
        protected readonly ILoggingHandler _LogginHandler;

        public ServiceBase()
            : this(null, null, null, null, null, null)
        {
        }

        public ServiceBase(
            IDynamicRepository repository,
            ICache cache,
            BaseRepository baseRepository,
            ICurrentContext currentContext,
            ICurrentSession currentSession,
            ILoggingHandler loggingHandler)
        {
            this._DynamicRepository = repository ?? new DynamicRepository();
            this._CacheManager = cache ?? CacheService.CreateInstance();
            this._Repository = baseRepository ?? new BaseRepository();
            this._Context = currentContext ?? new CurrentContext();
            this._LogginHandler = loggingHandler ?? new LoggingHandler();
        }

        protected IDynamicRepository Repository
        {
            get { return this._DynamicRepository; }
        }

        /// <summary>
        /// Get Report Data with Criteria.
        /// </summary>
        /// <typeparam name="T">Type of Object return.</typeparam>
        /// <param name="criteria">Criteria to Query DB.</param>
        /// <returns>Enumerable of Type T.</returns>
        public virtual IEnumerable<T> GetListData<T>(ICriteria criteria)
        {
            CheckArgumentIsNotNull(criteria);

            try
            {
                return criteria.IsValid() ? _DynamicRepository.Fetch<T>(criteria) : new List<T>();
            }
            catch (Exception exception)
            {
                HandleException(exception);
                return null;
            }
        }

        internal static void CheckArgumentIsNotNull(ICriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
        }

        protected void HandleException(Exception exception)
        {
            _LogginHandler.Log(exception);
        }
    }
}