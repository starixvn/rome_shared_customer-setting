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
        public List<GameSetting> GameSettingList { get; set; }

        public OpusSetting()
        {
            this.CustomerInformation = new CustomerInfo();

            this.PositionTaking = new PositionTaking();

            this.BetLimitSetting = new BetSetting();

            this.GameSettingList = new List<GameSetting>();
            List<StakeLevel> stakeLevelList = new List<StakeLevel>();

            for (int i = 0; i < 6; i++)
            {
                stakeLevelList.Add(new StakeLevel{
                    ID = i,
                    Value = "10-50",
                    IsChecked = true
                });

            }
            for (int i = 0; i < 8; i++)
            {
                this.GameSettingList.Add(new GameSetting
                {
                    GameTypeID = i,
                    GameTypeName = "Baccarat",
                    BettingLimitList = stakeLevelList,
                    StakeChoosen = stakeLevelList[0]
                });
            }
        }
    }
}
