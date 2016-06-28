// --------------------------------------------------------------------------
// <copyright file="SuperPositionTaking.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities
{
    /// <summary>
    /// The SuperPositionTaking entity.
    /// </summary>
    public class SuperPositionTaking : PositionTakingBase
    {
        public bool IsUpdateS4 { get; set; }

        public bool IsUpdateN4 { get; set; }

        public bool IsUpdateSCo { get; set; }

        public bool IsDataChanged()
        {
            return this.IsUpdateS4 || this.IsUpdateN4 || this.IsUpdateSCo;
        }
    }
}