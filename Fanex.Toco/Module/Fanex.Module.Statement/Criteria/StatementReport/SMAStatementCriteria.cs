// --------------------------------------------------------------------------
// <copyright file="SMAStatementCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Criteria.StatementReport
{
    using System;
    using Fanex.Module.Statement.Constants;
    using Fanex.Module.Statement.Repositories;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// SMA Statement Criteria.
    /// </summary>
    public class SMAStatementCriteria : CriteriaBase
    {
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public int CustId { get; set; }

        [SpParam(Ignored = true)]
        public int RoleId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public DateTime BalanceUpDay { get; set; }

        public override string GetSettingKey()
        {
            return "Statement_Report_" + RoleIdConstants.RoleName(RoleId);
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
