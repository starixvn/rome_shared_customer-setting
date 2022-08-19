// --------------------------------------------------------------------------
// <copyright file="SMAStatementCriteria.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Criteria.StatementReport
{
    using Fanex.Module.Statement.Services;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// SMA Statement Criteria.
    /// </summary>
    public class SMAStatementCriteria : StatementReportCriteria
    {
        [SpParam(Ignored = true)]
        public int RoleId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
      
        [SpParam(Ignored = true)]
        public bool IsSiteDB { get; set; } = false;

        [SpParam(Ignored = true)]
        public int SiteDBGroupID { get; set; }

        [SpParam(Ignored = true)]
        public bool IsBeforeMoveBet
        {
            get
            {
                return FromDate > MoveBetsDay;
            }
        }

        public override string GetSettingKey()
        {
            return ReportService.GetStoreMappingSiteDB(this, IsSiteDB, SiteDBGroupID, "Statement_Report_GetList");
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
