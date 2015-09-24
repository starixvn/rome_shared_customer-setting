namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Criteria
{
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// GetAgentSettingCriteria class.
    /// </summary>
    public class GetAgentSettingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int Mrecommend { get; set; }

        public override string GetSettingKey()
        {
            return Constants.GetAgentSettingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return this.CustId > 0;
        }
    }
}