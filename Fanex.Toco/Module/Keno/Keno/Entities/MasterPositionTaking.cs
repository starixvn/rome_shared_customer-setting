// --------------------------------------------------------------------------
// <copyright file="MasterPositionTaking.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Entities
{
    /// <summary>
    /// The MasterPositionTaking entity.
    /// </summary>
    public class MasterPositionTaking : PositionTakingBase
    {
        public bool IsUpdateS3 { get; set; }

        public bool IsUpdateUS3 { get; set; }

        public bool IsUpdateM3 { get; set; }

        public bool IsUpdateN3 { get; set; }

        public bool IsUpdateMComm { get; set; }

        public bool IsUpdateAllMember { get; set; }

        public double NewMemberPT { get; set; }
    }
}