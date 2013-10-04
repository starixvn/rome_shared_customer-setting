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
        public CustomerInfor CustomerInformation { get; set; }
        public PositionTaking PTCommSetting { get; set; }
        public BetSetting BetLimitSetting { get; set; }
        public GameSetting GameStakeSetting { get; set; }
    }
}
