using System.Threading;

namespace Fanex.CustomerSetting.Module.Core.Services.Impl
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Common;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.RemotingRef;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public static class CacheRef
    {
        private static IRemotingRefService _remotingRefService = new RemotingRefService();
        
        public static string GetBettype(object bettypeId)
        {
            return GetBettype(bettypeId, null);
        }

        public static string GetBettype(object bettypeId, object betID)
        {
            // resourceId remove first character
            string resourceId = string.Empty;
            if (betID != null && betID.ToString().Length > 1)
            {
                resourceId = betID.ToString().Substring(1);
            }

            return _remotingRefService.GetBetTypeName(bettypeId.ToString(), Thread.CurrentThread.CurrentCulture.Name, string.Empty, resourceId);
        }

        public static string GetSportName(object sportID)
        {
            return _remotingRefService.GetSportName(Converter.ToString2(sportID), Thread.CurrentThread.CurrentCulture.Name, string.Empty);
        }

        public static string GetTeamName(object teamID)
        {
            return _remotingRefService.GetTeamName(Converter.ToString2(teamID), Thread.CurrentThread.CurrentCulture.Name, string.Empty);
        }

        public static string GetLeagueName(object leagueID)
        {
            return _remotingRefService.GetLeagueName(Converter.ToString2(leagueID), Thread.CurrentThread.CurrentCulture.Name, string.Empty);
        }

        public static string GetHorseTeamName(object codeID)
        {
            return _remotingRefService.GetHorseTeamName(Converter.ToString2(codeID), Thread.CurrentThread.CurrentCulture.Name);
        }

        public static string GetOddsTypeName(object oddsType)
        {
            return _remotingRefService.GetOddsTypeName(Converter.ToString2(oddsType), Thread.CurrentThread.CurrentCulture.Name);
        }

        public static string GetCombinationName(string betteam)
        {
            string name = string.Empty;
            switch (betteam)
            {
                case "110":
                    name = "Trixie";
                    break;
                case "1110":
                    name = "Yankee";
                    break;
                case "11110":
                    name = "Canadian";
                    break;
                case "111110":
                    name = "Heinz";
                    break;
                case "1111110":
                    name = "Super Heinz";
                    break;
                case "11111110":
                    name = "Goliath";
                    break;
            }

            return name;
        }

        public static string GetRefResourceByRefIdAndResourceId(string refId, string resourceId, string defaultValue)
        {
            return _remotingRefService.GetResourceDataByRefIdAndResourceId(refId, resourceId, Thread.CurrentThread.CurrentCulture.Name, defaultValue);
        }

        public static string GetRouletteBetItem(int itemId)
        {
            return _remotingRefService.GetRouletteBetItem(Converter.ToString2(itemId), Thread.CurrentThread.CurrentCulture.Name);
        }

        public static string GetBettypeNameSportingSolution(object betypeId, string resourceId)
        {
            string bettypeName = string.Empty;
            bettypeName = _remotingRefService.GetBetTypeName(Converter.ToString2(betypeId), Thread.CurrentThread.CurrentCulture.Name, string.Empty, resourceId);
            return bettypeName;
        }

        public static string GetCasinoBetItem(object betStatusId)
        {
            return _remotingRefService.GetCasinoBetItem(betStatusId.ToString(), Thread.CurrentThread.CurrentCulture.Name, string.Empty);
        }

        public static string GetRouletteBetItem(object itemId)
        {
            return _remotingRefService.GetRouletteBetItem(itemId.ToString(), Thread.CurrentThread.CurrentCulture.Name, string.Empty);
        }
    }
}
