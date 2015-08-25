namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Criteria
{
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// GetSuperSettingCriteria class.
    /// </summary>
    public class UpdatedSettingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public override string GetSettingKey()
        {
            return Constants.GetSuperSettingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return this.CustId > 0;
        }
    }
}