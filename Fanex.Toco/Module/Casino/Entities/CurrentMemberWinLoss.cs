namespace Fanex.CustomerSetting.Module.Casino.Entities
{
    public class CurrentMemberWinLoss
    {
        public CurrentMemberWinLoss()
        {
        }

        public CurrentMemberWinLoss(decimal winPercent, decimal currentAmount, decimal maxWinCredit, decimal lossPercent, decimal? maxLossAmount)
        {
            WinPercent = winPercent;
            MaxWinAmount = maxWinCredit;
            CurrentAmount = currentAmount;
            LossPercent = lossPercent;
            MaxLossAmount = maxLossAmount;
        }

        public decimal WinPercent { get; set; }

        public decimal? MaxWinAmount { get; set; }

        public decimal LossPercent { get; set; }

        public decimal? MaxLossAmount { get; set; }

        public decimal CurrentAmount { get; set; }
    }
}