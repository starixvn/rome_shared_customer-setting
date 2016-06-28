// --------------------------------------------------------------------------
// <copyright file="StatementReportCriteria.cs" company="">
// Copyright (c). All rights reserved.
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
    public class StatementReportCriteria : DateRangeReportCriteria
    {
        public int CustId { get; set; }

        [SpParam(Ignored = true)]
        public string Username { get; set; }
    }
}
