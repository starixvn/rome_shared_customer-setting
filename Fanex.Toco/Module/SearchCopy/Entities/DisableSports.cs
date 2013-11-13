// --------------------------------------------------------------------------
// <copyright file="DisableSports.cs" company="Nexcel Solutions Vietnam">
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
    public class DisableSports
    {
        public bool DisableSportsbook { get; set; }

        public bool DisableSportsbookMinPT { get; set; }

        public bool DisableRacing { get; set; }

        public bool DisableRacingMinPT { get; set; }

        public bool DisableCasino { get; set; }

        public bool DisableVirtualSports { get; set; }

        public bool DisableBingo { get; set; }

        public bool DisableLiveCasino { get; set; }
    }
}
