namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Entities
{
    /// <summary>
    /// The BetSetting entity.
    /// </summary>
    public class Comission
    {
        public double CommisionValue { get; set; }

        public double MaxCommisionValue { get; set; }

        public bool IsCommisionUpdated { get; set; }
    }
}