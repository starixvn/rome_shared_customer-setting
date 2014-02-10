// -----------------------------------------------------------------------
// <copyright file="LicenseeInformation.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Fanex.CustomerSetting.OpusCasino.ViewModels
{
    public class OpusSetting
    {
        public CustomerInfo CustomerInformation { get; set; }
        public PositionTaking PositionTaking { get; set; }
        public BetSetting BetLimitSetting { get; set; }
        public GameSetting GameSetting { get; set; }

        public OpusSetting()
        {
            this.CustomerInformation = new CustomerInfo();

            this.PositionTaking = new PositionTaking();

            this.BetLimitSetting = new BetSetting();

            this.GameSetting = new GameSetting();
        }
    }
}
