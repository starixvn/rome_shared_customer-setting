// --------------------------------------------------------------------------
// <copyright file="ReportService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Services
{
    using Fanex.Module.Statement.Criteria.StatementReport;

    /// <summary>
    /// Report Service.
    /// </summary>
    public class ReportService
    {
        public const string BetTrans14SPSuffix = "_14";
        public const string WarehouseSPSuffix = "_WH";

        /// <summary>
        /// Gets the store mapping suffix.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>
        /// The store mapping suffix.
        /// </returns>
        public static string GetStoreMappingSuffix(DateRangeReportCriteria criteria)
        {
            if (criteria.FromDate > criteria.MoveBetDay)
            {
                return string.Empty;
            }

            if (criteria.FromDate >= criteria.BalanceUpDay)
            {
                return BetTrans14SPSuffix;
            }

            return WarehouseSPSuffix;
        }
    }
}
