// --------------------------------------------------------------------------
// <copyright file="CacheRef.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Services
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Common;
    using RemotingRef;

    /// <summary>
    /// The CacheRef class.
    /// </summary>
    public static class CacheRef
    {
        public static string GetBettype(object bettypeId)
        {
            return GetBettype(bettypeId, null, null);
        }

        public static string GetBettype(object bettypeId, object betID, string language)
        {
            if (string.IsNullOrEmpty(language))
            {
                language = "en-US";
            }

            string resourceId = betID != null && betID.ToString().Length > 1 ? betID.ToString().Substring(1) : string.Empty;
            return RefData.GetBetTypeName(bettypeId.ToString(), TWLang(language), string.Empty, resourceId);
        }

        public static string GetSportName(object sportID)
        {
            return RefData.GetSportName(Converter.ToString2(sportID), TWLang("en-US"), string.Empty);
        }

        public static string GetTeamName(object teamID)
        {
            return RefData.GetTeamName(Converter.ToString2(teamID), TWLang("en-US"), string.Empty);
        }

        public static string GetLeagueName(object leagueID)
        {
            return RefData.GetLeagueName(Converter.ToString2(leagueID), TWLang("en-US"), string.Empty);
        }

        public static string GetFncIndexName(object indexId)
        {
            return RefData.GetFncIndexName(Converter.ToString2(indexId), TWLang("en-US"));
        }

        public static string GetHorseTeamName(object codeID)
        {
            return RefData.GetHorseTeamName(Converter.ToString2(codeID), TWLang("en-US"));
        }

        public static string GetOddsTypeName(object oddsType)
        {
            return RefData.GetOddsTypeName(Converter.ToString2(oddsType), TWLang("en-US"));
        }

        /// <summary>
        /// Gets the name of the combination.
        /// </summary>
        /// <param name="betteam">The bet team.</param>
        /// <returns>The string of combination name.</returns>
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

        public static string GetLiveCasinoResult(int resultId)
        {
            return RefData.GetCasinoBetItem(Converter.ToString2(resultId), TWLang("en-US"));
        }

        public static string GetRouletteBetItem(int itemId)
        {
            return RefData.GetRouletteBetItem(Converter.ToString2(itemId), TWLang("en-US"));
        }

        public static string GetBettypeNameSportingSolution(object betypeId, string resourceId)
        {
            string bettypeName = string.Empty;
            bettypeName = RefData.GetBetTypeName(Converter.ToString2(betypeId), TWLang("en-US"), string.Empty, resourceId);
            return bettypeName;
        }

        public static string GetCasinoBetItem(object betStatusId)
        {
            return RefData.GetCasinoBetItem(betStatusId.ToString(), TWLang("en-US"), string.Empty);
        }

        public static string GetRouletteBetItem(object itemId)
        {
            return RefData.GetRouletteBetItem(itemId.ToString(), TWLang("en-US"), string.Empty);
        }

        /// <summary>
        /// Get the language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns>The result of language.</returns>
        private static string TWLang(string language)
        {
            switch (language)
            {
                case "en-US": return "en";
                case "zh-TW": return "ch";
                case "zh-CN": return "cs";
                case "ja-JP": return "jp";
                case "th-TH": return "th";
                default: return "en";
            }
        }
    }
}
