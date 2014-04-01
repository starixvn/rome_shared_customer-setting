// --------------------------------------------------------------------------
// <copyright file="BetSetting.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities
{
    /// <summary>
    /// The BetSetting entity.
    /// </summary>
    public class BetSetting
    {
        public int SportTypeId { get; set; }

        public int BetTypeId { get; set; }

        public string BetTypeName { get; set; }

        public decimal MinBet { get; set; }

        public decimal MaxBet { get; set; }

        public decimal MaxPerMatch { get; set; }

        public bool IsBetSettingUpdated { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsDownlinesEnabled { get; set; }

        public bool IsStatusUpdated { get; set; }

        public BetSetting Upline { get; set; }

        public bool IsDataChanged()
        {
            return this.IsBetSettingUpdated || this.IsStatusUpdated;
        }
    }
}