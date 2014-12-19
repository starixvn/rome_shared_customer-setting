// --------------------------------------------------------------------------
// <copyright file="MemberStatementCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// Statement Report Criteria.
    /// </summary>
    public class StatementReportCriteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int CustId { get; set; }

        public DateTime BalanceUpDay { get; set; }

        public DateTime MoveBetDay { get; set; }
    }
}
