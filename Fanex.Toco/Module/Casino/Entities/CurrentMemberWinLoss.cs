namespace Fanex.CustomerSetting.Module.Casino.Entities
{
    public class CurrentMemberWinLoss
    {
        public CurrentMemberWinLoss()
        {
        }

        public CurrentMemberWinLoss(decimal winPercent, decimal maxWinLimit, decimal maxWinCredit, decimal lossPercent, decimal? maxLossAmount)
        {
            WinPercent = winPercent;
            MaxWinAmount = maxWinLimit == 0 ? new decimal?() : maxWinCredit;

            LossPercent = lossPercent;
            MaxLossAmount = maxLossAmount;
        }

        public decimal WinPercent { get; set; }

        public decimal? MaxWinAmount { get; set; }

        public decimal LossPercent { get; set; }

        public decimal? MaxLossAmount { get; set; }
    }
}