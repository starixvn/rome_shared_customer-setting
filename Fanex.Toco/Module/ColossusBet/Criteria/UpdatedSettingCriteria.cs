namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Criteria
{
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.NAP.Framework.Data;

    public class UpdatedSettingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public bool IsUpdateCommission { get; set; }

        public bool IsUpdateCashInShare { get; set; }

        public bool IsUpdateDisable { get; set; }

        public decimal V204Commission_18001 { get; set; }

        public decimal V204CashInShare_18002 { get; set; }

        public bool EnableDownlines { get; set; }

        public bool Disable { get; set; }

        public string IP { get; set; }

        public int UserID { get; set; }

        public string Language { get; set; }
        
        public override string GetSettingKey()
        {
            return Constants.GetUpdatedColossusBetCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return this.CustId > 0;
        }
    }
}