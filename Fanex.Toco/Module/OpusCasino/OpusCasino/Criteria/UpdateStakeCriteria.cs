// -----------------------------------------------------------------------
// <copyright file="UpdateStakeCriteria.cs" company="Nexcel Solution">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.OpusCasino.Criteria
{
    using Fanex.NAP.Framework.Data;

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