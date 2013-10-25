// ************************************************************************************
// <copyright file="Data.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

namespace Fanex.CustomerSetting.Module.SearchCopy.Entities
{
    /// <summary>
    /// Class Data.
    /// </summary>
    public class Data
    {
        public decimal Pt { get; set; }

        public decimal MinPt { get; set; }

        public decimal MaxPt { get; set; }

        public decimal AutoPt { get; set; }

        public decimal MinAutoPt { get; set; }

        public decimal MaxAutoPt { get; set; }

        public bool IsAutoPt { get; set; }

        public decimal UplinePt { get; set; }

        public decimal MinUplinePt { get; set; }

        public decimal MaxUplinePt { get; set; }

        public decimal MasterPt { get; set; }

        public decimal AgentPt { get; set; }
        
        public bool IsUplineAutoPt { get; set; }
    }
}