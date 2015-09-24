namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Entities
{
    /// <summary>
    /// The BetSetting entity.
    /// </summary>
    public class CashInShare
    {
        public decimal CashInShareValue { get; set; }

        public decimal MaxCashInShareValue { get; set; }

        public bool IsCashInShareUpdated { get; set; }
    }
}