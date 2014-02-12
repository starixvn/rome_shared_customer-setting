// ************************************************************************************
// <copyright file="UpdateStakeCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// UpdateStakeCriteria Class.
    /// </summary>
    public class UpdateStakeCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int UserId { get; set; }

        public int GameTypeId { get; set; }

        public string StakeLevelList { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerUpdOpusCS_StakeLevel";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}