// --------------------------------------------------------------------------
// <copyright file="SuperPositionTaking.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Entities
{
    public class SuperPositionTaking : PositionTakingBase
    {
        public bool IsUpdatePT { get; set; }

        public bool IsUpdateS4 { get; set; }

        public bool IsUpdateN4 { get; set; }

        public bool IsUpdateSCO { get; set; }
    }
}