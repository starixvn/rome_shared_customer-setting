// -----------------------------------------------------------------------
// <copyright file="CommonService.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Robert.Tran" date="2013.2.05">Created</change>
// </history>


using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Data;
using Fanex.Common.Extensions;
using Fanex.CustomerSetting.Module.Core.Criteria;
using Fanex.CustomerSetting.Module.Core.Models;
using Fanex.CustomerSetting.Module.Core.Repository;
using Fanex.CustomerSetting.Module.Core.Services.Interface;

namespace Fanex.CustomerSetting.Module.Core.Services.Impl
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class CoreService : ServiceBase, ICoreService
    {
        private readonly BaseRepository _repository;

        public CoreService()
        {
            _repository = new BaseRepository();
        }

        public IList<Bettypes> GetBettypes(GetBettypeCriteria criteria)
        {
            Func<IDataReader, Bettypes> mappingFunc = r => {
                return new Bettypes { 
                    Id = r.ToString("typeId"),
                    Name = r.ToString("BetTypeName"),
                    Type = r.ToInt("type"),
                    LiveIndicator = r.ToBool("LiveIndicator")
                };
            };

            IList<Bettypes> bettypes = _repository.Fetch<Bettypes>(criteria, mappingFunc);
            return bettypes;
        }

        public IList<Currency> GetCurrency()
        {
            Func<IDataReader, Currency> mappingFunc = r =>
            {
                return new Currency
                {
                    Id = r.ToString("exchangeid"),
                    Name = r.ToString("currency")
                };
            };

            var obj = _repository.Fetch<Currency>(new GetCurrencyCriteria(), mappingFunc);
            return obj;
        }


        public void Test()
        {
            DataTable dt = _repository.Fetch(new TestCriteria());
            int c = dt.Rows.Count;
        }


        
    }
}
