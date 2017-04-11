using Fanex.NAP.Framework.Data;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria
{
    internal class UpdateColossusBetStatusCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public int SystemId { get; set; }

        public bool Disable { get; set; }

        public bool EnableDownlines { get; set; }

        public string Language { get; set; }

        public string IP { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerUpdDisableColossusBet";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
