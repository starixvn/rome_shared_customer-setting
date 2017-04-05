using Fanex.NAP.Framework.Data;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria
{
    internal class SelectNG2ProductStatusCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int ProductId { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustSetting_DisableGameStatusSelect";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
