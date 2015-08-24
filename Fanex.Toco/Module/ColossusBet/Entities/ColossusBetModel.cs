namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Entities
{
    /// <summary>
    /// The BetSetting entity.
    /// </summary>
    public class ColossusBetModel
    {
        public CustomerInformation CustomerInformationPartial { get; set; }

        public Status StatusPartial { get; set; }

        public Comission ComissionPartial { get; set; }

        public CashInShare CashInSharePartial { get; set; }
    }
}