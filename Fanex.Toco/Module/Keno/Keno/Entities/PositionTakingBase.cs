// --------------------------------------------------------------------------
// <copyright file="PositionTakingBase.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Entities
{
    /// <summary>
    /// The PositionTakingBase entity.
    /// </summary>
    public class PositionTakingBase
    {
        public int SportTypeId { get; set; }

        public int BetTypeId { get; set; }

        public string BetTypeName { get; set; }

        public double S4 { get; set; }

        public double M4 { get; set; }

        public double A4 { get; set; }

        public double N4 { get; set; }

        public double L4 { get; set; }

        public double SCo { get; set; }

        public double SCoMax { get; set; }

        public double S3 { get; set; }

        public double M3 { get; set; }

        public double US3 { get; set; }

        public bool IS3 { get; set; }

        public double N3 { get; set; }

        public double L3 { get; set; }

        public double MCo { get; set; }

        public double M2 { get; set; }

        public double A2 { get; set; }

        public double UM2 { get; set; }

        public bool IM2 { get; set; }

        public double N2 { get; set; }

        public double L2 { get; set; }

        public double ACo { get; set; }

        public double A1 { get; set; }

        public double Co { get; set; }

        public double S1 { get; set; }

        public double US1 { get; set; }

        public bool IS1 { get; set; }

        public double M1 { get; set; }

        public double UM1 { get; set; }

        public bool IM1 { get; set; }
    }
}