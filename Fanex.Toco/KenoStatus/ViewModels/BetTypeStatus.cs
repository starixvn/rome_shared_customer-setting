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

    /// <summary>
    /// Enable Status by Bet Type
    /// </summary>
    public class BetTypeStatus
    {
        public int BetTypeID { get; set; }
        public string BetTypeName { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDisabledUpline { get; set; }
        public bool IsDisabledDownlines { get; set; }
        public string BetTypeName { get; set; }
    }
}
