namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Entities
{
    /// <summary>
    /// The BetSetting entity.
    /// </summary>
    public class Comission
    {
        public decimal CommisionValue { get; set; }

        public decimal MaxCommisionValue { get; set; }

        public bool IsCommisionUpdated { get; set; }
    }
}