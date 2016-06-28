// --------------------------------------------------------------------------
// <copyright file="SuperEditMemberPositionTaking.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities
{
    /// <summary>
    /// The SuperEditMemberPositionTaking entity.
    /// </summary>
    public class SuperEditMemberPositionTaking : PositionTakingBase
    {
        public bool IsUpdateS1 { get; set; }

        public bool IsUpdateUS1 { get; set; }

        public double S1US1Min { get; set; }

        public double S1Max { get; set; }

        public bool IsDataChanged()
        {
            return this.IsUpdateS1 || this.IsUpdateUS1;
        }
    }
}