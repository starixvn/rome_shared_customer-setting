// --------------------------------------------------------------------------
// <copyright file="DateRangeReportCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Criteria.StatementReport
{
    using System;
    using System.Data;
    using Fanex.Caching;
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.Module.Statement.Repositories.StatementReport;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Date Range Report Criteria.
    /// </summary>
    public class DateRangeReportCriteria : CriteriaBase
    {
        private const string BalanceUpDayKey = "__balanceUpDayKey";
        private static ICache _cache;
        private DataTable _balanceUpDay;
        private IStatementReportRepository _report;
        
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public DateTime BalanceUpDay 
        {
            get
            {
                return Utils.ToDateTime(GetBalanceUpDay().Rows[0]["BalanceUpDay"]);
            }
        }

        public DateTime MoveBetsDay 
        {
            get
            {
                return Utils.ToDateTime(GetBalanceUpDay().Rows[0]["MoveBetsDay"]);
            }
        }

        public override bool  IsValid()
        {
            return this.FromDate.Year >= 1753 && this.FromDate.Year <= 9999
                && this.ToDate.Year >= 1753 && this.ToDate.Year <= 9999
                && this.ToDate >= this.FromDate;
        }

        /// <summary>
        /// Gets balance up day.
        /// </summary>
        /// <returns>DataTable object.</returns>
        private DataTable GetBalanceUpDay()
        {
            _report = new StatementReportRepository();

            if (_cache == null)
            {
                _cache = CacheService.CreateInstance(CacheType.MemoryCache);
            }

            _balanceUpDay = _cache.Get<DataTable>(BalanceUpDayKey);

            if (_balanceUpDay == null)
            {
                _balanceUpDay = _report.GetBalanceUpDay();
                _cache.Set<DataTable>(BalanceUpDayKey, _balanceUpDay, DateTime.Now.AddMinutes(5));

                return _balanceUpDay;
            }

            return _balanceUpDay;
        }
    }
}
