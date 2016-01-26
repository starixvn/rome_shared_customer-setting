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
            return stakeBuilder;
        }
    }
}