// ************************************************************************************
// <copyright file="KenoEnableStatus.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="ethan.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.KenoStatus.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Keno Enable Status Object.
    /// </summary>
    public class KenoEnableStatus
    {
        public KenoEnableStatus()
        {
            this.BetTypeStatusList = new List<BetTypeStatus>();
        }

        public List<BetTypeStatus> BetTypeStatusList { get; set; }

        public int CustID { get; set; }

        public string UserName { get; set; }

        public int RoleID { get; set; }

        public int UplineID { get; set; }

        public int UserID { get; set; }

        public int SubAccID { get; set; }

        public string Language { get; set; }

        public bool HasDownline { get; set; }
    }
}