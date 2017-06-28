using Fanex.NAP.Framework.Data;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria.DCS;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria
{
    internal class UpdateGoldDeluxeStatusCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public bool Disable { get; set; }

        public bool EnableDownlines { get; set; }

        public int SystemId { get; set; }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerUpdDisableGoldDeluxe";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
