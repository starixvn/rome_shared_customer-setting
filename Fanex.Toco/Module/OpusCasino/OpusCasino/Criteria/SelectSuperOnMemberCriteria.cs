// ************************************************************************************
// <copyright file="SelectSuperOnMemberCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

namespace Fanex.CustomerSetting.OpusCasino.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// SelectSuperOnMemberCriteria Class.
    /// </summary>
    public class SelectSuperOnMemberCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerSelSEditCustomerOpusCS";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}