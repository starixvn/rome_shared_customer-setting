// --------------------------------------------------------------------------
// <copyright file="AgentPositionTaking.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities
{
    /// <summary>
    /// The AgentPositionTaking entity.
    /// </summary>
    public class AgentPositionTaking : PositionTakingBase
    {
        public bool IsUpdateA2 { get; set; }

        public bool IsUpdateM2 { get; set; }

        public bool IsUpdateUM2 { get; set; }

        public bool IsUpdateN2 { get; set; }

        public bool IsUpdateACO { get; set; }

        public bool IsUpdateAllMembers { get; set; }

        public double NewMemberPT { get; set; }

        public bool IsDataChanged()
        {
            return this.IsUpdateA2 || this.IsUpdateM2 || this.IsUpdateUM2 || this.IsUpdateN2 || this.IsUpdateACO;
        }
    }
}