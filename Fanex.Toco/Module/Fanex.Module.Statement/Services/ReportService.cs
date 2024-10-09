// --------------------------------------------------------------------------
// <copyright file="ReportService.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Services
{
    using Fanex.Module.Statement.Criteria.StatementReport;

    public class ReportService
    {
        public const string BetTrans14SPSuffix = "_14";
        public const string WarehouseSPSuffix = "_WH";
        public const string SiteDBPrefix = "SiteDB";

        /// <summary>
        /// Gets the store mapping suffix.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>
        /// The store mapping suffix.
        /// </returns>
        public static string GetStoreMappingSuffix(DateRangeReportCriteria criteria)
        {
            if (criteria.FromDate > criteria.MoveBetsDay)
            {
                return string.Empty;
            }

            if (criteria.FromDate >= criteria.BalanceUpDay)
            {
                return BetTrans14SPSuffix;
            }

            return WarehouseSPSuffix;
        }

        public static string GetStoreMappingSiteDB(DateRangeReportCriteria criteria, bool isSiteDB, int siteDBGroupID, string settingKey)
        {
            if (criteria.FromDate > criteria.MoveBetsDay)
            {
                return isSiteDB ? $"{SiteDBPrefix}{siteDBGroupID}_{settingKey}" : settingKey;
            }

            if (criteria.FromDate >= criteria.BalanceUpDay)
            {
                return settingKey + BetTrans14SPSuffix;
            }

            return settingKey + WarehouseSPSuffix;
        }
    }
}
