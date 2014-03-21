// ************************************************************************************
// <copyright file="BetTypeStatus.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="ethan.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.KenoStatus.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class KenoEnableStatus
    {
        public List<BetTypeStatus> BetTypeStatusList { get; set; }
        
        public int CustID { get; set; }

        public string UserName { get; set; }

        public int RoleID { get; set; }

        public int UplineID { get; set; }

        public int UserID { get; set; }

        public int SubAccID { get; set; }

        public string Language { get; set; }
        
        public bool HasDownline { get; set; }
        
        public KenoEnableStatus()
        {
            this.BetTypeStatusList = new List<BetTypeStatus>();
        }
    }
}
