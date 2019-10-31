// --------------------------------------------------------------------------
// <copyright file="GetCustomerInformationByUsername.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Criteria
{
    using CustomerSetting.Module.Core.Web;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Date Range Report Criteria.
    /// </summary>
    public class GetCustomerInformationByUsername : CriteriaBase
    {
        public string Username { get; set; }

        public int UserId
            => AppSession.CurrentSession.UserId;

        public override string GetSettingKey()
            => "Statement_Report_GetCustomerInformationByUsername";

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(Username) && UserId > 0;
    }
}
