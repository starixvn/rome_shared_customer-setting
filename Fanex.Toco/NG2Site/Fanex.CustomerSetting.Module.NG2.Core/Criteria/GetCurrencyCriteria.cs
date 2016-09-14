namespace Fanex.CustomerSetting.Module.NG2.Core.Criteria
{
    using Fanex.Data.Repository;

    public class GetCurrencyCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return "Core_ExchangeCurrencySel";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
