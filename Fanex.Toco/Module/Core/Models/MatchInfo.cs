// --------------------------------------------------------------------------
// <copyright file="MatchInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/19">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Models
{
    using Fanex.CustomerSetting.Module.Core.Services;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MatchInfo
    {
        public int MatchId { get; set; }

        public string MatchCode { get; set; }

        public string EventDate { get; set; }

        public int HomeId { get; set; }

        public int AwayId { get; set; }

        public string HomeName
        {
            get
            {
                return CacheRef.GetTeamName(HomeId) ?? string.Empty;
            }
        }

        public string AwayName
        {
            get
            {
                return CacheRef.GetTeamName(AwayId) ?? string.Empty;
            }
        }
    }
}
