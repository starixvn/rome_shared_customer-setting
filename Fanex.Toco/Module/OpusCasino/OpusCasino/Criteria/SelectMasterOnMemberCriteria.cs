// ************************************************************************************
// <copyright file="SelectMasterOnMemberCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// SelectMasterOnMemberCriteria Class.
    /// </summary>
    public class SelectMasterOnMemberCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerSelMEditCustomerOpusCS";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}