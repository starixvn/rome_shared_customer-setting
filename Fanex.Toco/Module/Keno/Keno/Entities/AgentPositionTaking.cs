// --------------------------------------------------------------------------
// <copyright file="AgentPositionTaking.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Entities
{
    /// <summary>
    /// The AgentPositionTaking entity.
    /// </summary>
    public class AgentPositionTaking
    {
        public bool IsDisabledMPT { get; set; }

        public bool IsUpdatePT { get; set; }

        public bool IsUpdateM2 { get; set; }

        public bool IsUpdateUM2 { get; set; }

        public bool IsUpdateN2 { get; set; }

        public bool IsUpdateACO { get; set; }

        public bool IsUpdateAllMember { get; set; }

        public double NewMemberPT { get; set; }
    }
}