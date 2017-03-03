namespace Fanex.Module.RNGCasino.Helpers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Text.RegularExpressions;
    using CustomerSetting.Module.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.CustomerSetting.Module.Core.Extensions;
    using Models;
    using Resources;

    public class BetListHelper
    {
        public static readonly Regex IsNumericRegex = new Regex("^(" +
            /*Hex*/ @"0x[0-9a-f]+" + "|" +
            /*Bin*/ @"0b[01]+" + "|" +
            /*Oct*/ @"0[0-7]+" + "|" +
            /*Dec*/ @"((?!0)|[-+])(\d*\.)?\d+(e\d+)?" +
                                                    ")$");

        public static bool IsNumeric(string value)
        {
            return IsNumericRegex.IsMatch(value);
        }

        public static bool IsUseRefNoInsteadOfTransId(ITicket ticket)
        {
            return ticket.SportTypeId == SportContants.RNG_RNGGame ||
                ticket.SportTypeId == SportContants.RNG_MiniGame ||
                ticket.SportTypeId == SportContants.RNG_Mobile;
        }

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed. Suppression is OK here.")]
        public static string GetCommonHeaderRows(int siteId)
        {
            var result = new StringBuilder();
            result.Append("<tr>");
            result.Append("<th><div class=\"width30 height34 table-cell\">" + BetListResources.No + "</div></th>");
            result.Append("<th><div class=\"width120 height34 table-cell\">" + BetListResources.Trans_Time + "</div></th>");
            result.Append("<th><div class=\"column-choice height34 table-cell\">" + BetListResources.Choice + "</div></th>");
            result.Append("<th><div class=\"width60 height34 table-cell\">" + BetListResources.Odds + "</div></th>");
            result.Append("<th><div class=\"width50 height34 table-cell\">" + BetListResources.Stake + "</div></th>");
            result.Append("<th><div class=\"column-winloss height34 table-cell\">" + BetListResources.Win_Loss + "</div></th>");
            result.Append("<th><div class=\"width100 height34 table-cell\">" + BetListResources.Status + "</div></th>");
            result.Append("<th><div class=\"width60 height34 table-cell\">" + BetListResources.Super + "<br />" + BetListResources.PT + "/" + BetListResources.Comm + "</div></th>");
            result.Append("<th><div class=\"width60 height34 table-cell\">" + BetListResources.Master + "<br />" + BetListResources.PT + "/" + BetListResources.Comm + "</div></th>");
            result.Append("<th><div class=\"width60 height34 table-cell\">" + BetListResources.Agent + "<br />" + BetListResources.PT + "/" + BetListResources.Comm + "</div></th>");
            result.Append("</tr>");
            return result.ToString();
        }

        public string IsNegative(decimal input)
        {
            return input < 0 ? "negative" : "positive";
        }

        public string PercentFormat(decimal input)
        {
            var isInputInterger = (input * 100) % 1 == 0;
            return isInputInterger ?
                Math.Round(input * 100, 0).ToString() + BetListResources.Symbol_Percent :
                Math.Round(input * 100, 2).ToString() + BetListResources.Symbol_Percent;
        }

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        public string StatusRefNo(TicketDetail ticketDetail)
        {
            if (IsFishingWorld(ticketDetail.Ticket.SportTypeId))
            {
                return GetFishingWorldStatus(ticketDetail);
            }

            BetListHelper betlistHelper = new BetListHelper();
            string status = ticketDetail.Ticket.Status.ToLower();
            string statusLabel = string.Empty;
            bool isWonStatus = string.Equals(status, "won", StringComparison.OrdinalIgnoreCase);
            bool isLoseStatus = string.Equals(status, "lose", StringComparison.OrdinalIgnoreCase);
            bool isDrawStatus = string.Equals(status, "draw", StringComparison.OrdinalIgnoreCase);
            var isRunningStatus = string.Equals(status, "running", StringComparison.OrdinalIgnoreCase);

            if (isRunningStatus && !string.IsNullOrEmpty(ticketDetail.Ticket.BetCheck))
            {
                var viewBrokenGame = "ViewRNGCasinoBrokenGame(" +
                    ticketDetail.Ticket.MatchId + "," +
                    (string.IsNullOrEmpty(ticketDetail.Ticket.Race) ? "0" : ticketDetail.Ticket.Race) + "," +
                    ticketDetail.Ticket.BetTypeId + "," +
                    ticketDetail.Ticket.SportTypeId + "," +
                    "'" + (ticketDetail.Ticket.BetCheck ?? string.Empty) + "'," +
                    "'" + ticketDetail.Ticket.WinlostDate.ToShortFormatedDate() + "')";
                statusLabel = "<div id=\"rngcasino_brokengame_" + ticketDetail.Ticket.TransId + "\" onclick=\"" + viewBrokenGame 
                    + "\" class=\"viewbrokengame-link\">" + status.ToUpper() + "</div>";
            }
            else
            {
                statusLabel = "<div class=\"bl_status c\">" + status.ToUpper() + "</div>";
            }

            string resultLink = string.Empty;
            if (isWonStatus || isLoseStatus || isDrawStatus)
            {
                resultLink = string.IsNullOrEmpty(ticketDetail.Ticket.BetCheck) ?
                    string.Empty :
                    "ViewRNGCasinoResult(" +
                        ticketDetail.Ticket.MatchId + "," +
                        (string.IsNullOrEmpty(ticketDetail.Ticket.Race) ? "0" : ticketDetail.Ticket.Race) + "," +
                        ticketDetail.Ticket.BetTypeId + "," +
                        ticketDetail.Ticket.SportTypeId + "," +
                        "'" + (ticketDetail.Ticket.BetCheck ?? string.Empty) + "'," +
                        "\'" + ticketDetail.Ticket.WinlostDate.ToShortFormatedDate() + "\'" + "," +
                        ticketDetail.Ticket.CustId + ")";
            }

            var resultLink1 = string.IsNullOrEmpty(resultLink) ?
                string.Empty :
                "<br />" +
                "<a id=\"result_" + ticketDetail.Ticket.TransId + "\" onclick=\"" + resultLink + "\" class=\"betlist-result\">" + BetListResources.Result + "</a>" +
                "<br />";
            return statusLabel + resultLink1;
        }

        public bool IsShowOddsWithChoice(int betTypeId)
        {
            return betTypeId == BetTypeContants.MixParlay || betTypeId == BetTypeContants.SystemParlay;
        }

        public bool IsFishingWorld(int sportType)
        {
            return sportType == SportContants.FishingWorld;
        }

        public string GetFishingWorldStatus(TicketDetail ticketDetail)
        {
            var statusLabel = string.Empty;
            var status = ticketDetail.Ticket.Status.ToLower();
            bool isWonStatus = string.Equals(status, "won", StringComparison.OrdinalIgnoreCase);
            bool isLoseStatus = string.Equals(status, "lose", StringComparison.OrdinalIgnoreCase);
            bool isDrawStatus = string.Equals(status, "draw", StringComparison.OrdinalIgnoreCase);

            statusLabel = "<div class=\"bl_status c\">" + status.ToUpper() + "</div>";

            var resultLink = string.Empty;
            if (isWonStatus || isLoseStatus || isDrawStatus)
            {
                resultLink = string.IsNullOrEmpty(ticketDetail.Ticket.BetCheck) ?
                     string.Empty :
                     "ViewFishingWorldResult(" + ticketDetail.Ticket.CustId + ", '" + ticketDetail.Ticket.BetCheck + "')";
            }

            var resultLink1 = string.IsNullOrEmpty(resultLink) ?
                 string.Empty :
                 "<br />" +
                 "<a id=\"result_" + ticketDetail.Ticket.TransId + "\" onclick=\"" + resultLink + "\">" + BetListResources.Result + "</a>" +
                 "<br />";
            return statusLabel + resultLink1;
        }
    }
}
