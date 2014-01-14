// -----------------------------------------------------------------------
// <copyright file="AvatarSelectingCriteria.cs" company="Nexcel Solution">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.OpusCasino.Criteria
{
    using Fanex.NAP.Framework.Data;

    public class SelectMemberCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerSelEditMemberOpusCS";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}