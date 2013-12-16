// --------------------------------------------------------------------------
// <copyright file="HideSports.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.11.04">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.SearchCopy.Entities
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class HideSports
    {
        public bool HideSportsbook { get; set; }

        public bool HideSportsbookMinPT { get; set; }

        public bool HideRacing { get; set; }

        public bool HideRacingMinPT { get; set; }

        public bool HideCasino { get; set; }

        public bool HideVirtualSports { get; set; }

        public bool HideBingo { get; set; }

        public bool HideLiveCasino { get; set; }
    }
}
