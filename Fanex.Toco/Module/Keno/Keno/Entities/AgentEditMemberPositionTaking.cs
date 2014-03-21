// --------------------------------------------------------------------------
// <copyright file="AgentEditMemberPositionTaking.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Entities
{
    /// <summary>
    /// The AgentEditMemberPositionTaking entity.
    /// </summary>
    public class AgentEditMemberPositionTaking : PositionTakingBase
    {
        public bool IsUpdatePT { get; set; }

        public bool IsUpdateA1 { get; set; }

        public bool IsUpdateComm { get; set; }
    }
}