// ************************************************************************************
// <copyright file="OpusSetting.cs" company="Nexcel Solutions Vietnam">
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

    /// <summary>
    /// OpusSetting Class.
    /// </summary>
    public class OpusSetting
    {
        public OpusSetting()
        {
            this.CustomerInformation = new CustomerInfo();
        
            this.BetLimitSetting = new BetSetting();

            this.GameSetting = new GameSetting();
        }

        public CustomerInfo CustomerInformation { get; set; }

        public BetSetting BetLimitSetting { get; set; }

        public GameSetting GameSetting { get; set; }
    }
}
