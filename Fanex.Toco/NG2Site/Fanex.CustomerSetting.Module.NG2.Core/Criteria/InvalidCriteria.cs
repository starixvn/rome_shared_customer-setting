namespace Fanex.CustomerSetting.Module.NG2.Core.Criteria
{
    using Data.Repository;

    public class InvalidCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return string.Empty;
        }

        public override bool IsValid()
        {
            return false;
        }
    }
}