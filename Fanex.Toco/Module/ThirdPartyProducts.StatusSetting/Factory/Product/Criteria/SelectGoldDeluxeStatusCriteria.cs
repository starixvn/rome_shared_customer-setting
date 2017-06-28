using Fanex.NAP.Framework.Data;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria
{
    class SelectGoldDeluxeStatusCriteria : CriteriaBase
    {
        public SelectGoldDeluxeStatusCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerSelStatusGoldDeluxe";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
