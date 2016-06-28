// --------------------------------------------------------------------------
// <copyright file="MasterPositionTaking.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities
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

        public bool IsDataChanged()
        {
            return this.IsUpdateS3 || this.IsUpdateUS3 || this.IsUpdateM3 || this.IsUpdateN3 || this.IsUpdateMComm;
        }
    }
}