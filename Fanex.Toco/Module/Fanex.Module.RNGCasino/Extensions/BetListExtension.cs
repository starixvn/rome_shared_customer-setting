namespace Fanex.Module.RNGCasino.Extensions
{
    using Fanex.BetList.Core.Builder;
    using Models;
    using Resources;

    public static class BetListExtension
    {
        public static string BuildStake(this TicketDetail betDetail, ref BetListHTMLBuilder builder)
        {
            string stakeBuilder = string.Empty;
            stakeBuilder = builder.BuildStake(betDetail.Ticket);
            if (betDetail.IsShowOtExRate)
            {
                stakeBuilder +=
                    "<br />" +
                    "<span class=\"r exrate2\">" + betDetail.VirtualRate + "</span>" +
                    "<br />" +
                    "<span class=\"r exrate2\">(" + BetListResources.Ex_Rate_2 + ")</span>";
            }

            return stakeBuilder;
        }
    }
}