using Fanex.NAP.Framework.Data;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria
{
    internal class UpdateCasinoStatusCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public bool DisabledCS { get; set; }

        public const bool SyncCS = true;

        public int SystemId { get; set; }

        public bool EnableDownline { get; set; }

        public string Language { get; set; }

        public string LoginIP { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_UpdCSStatus";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
