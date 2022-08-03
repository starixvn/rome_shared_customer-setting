// --------------------------------------------------------------------------
// <copyright file="MemberStatementCriteria.cs" company="">
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
    /// Member Statement Criteria.
    /// </summary>
    public class MemberStatementCriteria : StatementReportCriteria
    {
        [SpParam(Ignored = true)]
        public int RoleId { get; set; }

        [SpParam(Ignored = true)]
        public string Language
        {
            get
            {
                return "EN";
            }
        }

        [SpParam(Ignored = true)]
        public bool IsSiteDB { get; set; } = false;

        [SpParam(Ignored = true)]
        public int PageSize { get; set; }

        [SpParam(Ignored = true)]
        public int PageIndex { get; set; }

        public override string GetSettingKey()
        {
            return ReportService.GetStoreMappingSiteDB(this, IsSiteDB, "Statement_Report_GetList");
        }
    }
}
