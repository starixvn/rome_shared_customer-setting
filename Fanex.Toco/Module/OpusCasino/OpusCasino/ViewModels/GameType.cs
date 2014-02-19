// ************************************************************************************
// <copyright file="GameType.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

namespace Fanex.CustomerSetting.OpusCasino.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// GameType Class.
    /// </summary>
    public class GameType
    {       
        public GameType()
        {
            this.StakeLevelList = new List<StakeLevel>();
        }

        public bool IsUpdateGameType { get; set; }

        public string GameTypeName { get; set; }

        public int GameTypeID { get; set; }

        public List<StakeLevel> StakeLevelList { get; set; }

        public StakeLevel StakeChoosen { get; set; }
    }
}