// --------------------------------------------------------------------------
// <copyright file="CheckInternalByUsernameCriteria.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Date Range Report Criteria.
    /// </summary>
    public class CheckInternalByUsernameCriteria : CriteriaBase
    {
        public string Username { get; set; }

        public bool IsInternal { get; set; }

        public override string GetSettingKey()
        {
            return "Statement_Report_CheckInternalByUsername";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
