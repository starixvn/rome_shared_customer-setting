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
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Date Range Report Criteria.
    /// </summary>
    public class DateRangeReportCriteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        [SpParam(Ignored = true)]
        public DateTime BalanceUpDay { get; set; }

        [SpParam(Ignored = true)]
        public DateTime MoveBetDay { get; set; }
    }
}
