// --------------------------------------------------------------------------
// <copyright file="MasterEditMemberPositionTaking.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities
{
    /// <summary>
    /// The MasterEditMemberPositionTaking entity.
    /// </summary>
    public class MasterEditMemberPositionTaking : PositionTakingBase
    {
        public bool IsUpdateM1 { get; set; }

        public bool IsUpdateUM1 { get; set; }

        public double M1UM1Min { get; set; }

        public double M1Max { get; set; }

        public bool IsDataChanged()
        {
            return this.IsUpdateM1 || this.IsUpdateUM1;
        }
    }
}