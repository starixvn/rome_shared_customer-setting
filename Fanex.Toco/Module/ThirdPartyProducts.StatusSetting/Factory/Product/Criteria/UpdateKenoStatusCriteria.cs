using Fanex.NAP.Framework.Data;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria
{
    internal class UpdateKenoStatusCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int Bettype { get; set; }

        public bool Disable { get; set; }

        public bool DisableDownlines { get; set; }

        public int SystemId { get; set; }

        public int SubAccId { get; set; }

        public int UserId { get; set; }

        public int UplineId { get; set; }

        public string Language { get; set; }

        public string Ip { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerUpdDisableKeno";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
