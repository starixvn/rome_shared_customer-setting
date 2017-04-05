using Fanex.NAP.Framework.Data;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria
{
    internal class SelectProductStatusCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int ProductId { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerSelStatusByProductId";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
