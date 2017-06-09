using Fanex.NAP.Framework.Data;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria
{
    internal class SelectNG2ProductStatusCriteria : SelectProductStatusCriteria
    {
        public override string GetSettingKey()
        {
            return "Acc_CustSetting_DisableGameStatusSelect";
        }
    }
}
