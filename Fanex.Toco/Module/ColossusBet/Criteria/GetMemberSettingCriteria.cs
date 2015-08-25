namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Criteria
{
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// AgentGetMemberSettingCriteria class.
    /// </summary>
    public class GetMemberSettingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int Recommend { get; set; }

        public override string GetSettingKey()
        {
            return Constants.GetMemberSettingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return this.CustId > 0;
        }
    }
}