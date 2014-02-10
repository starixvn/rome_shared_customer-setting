using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.CustomerSetting.OpusCasino.ViewModels
{
    public class GameType
    {
        public bool IsUpdateGameType { get; set; }
        public string GameTypeName { get; set; }
        public int GameTypeID { get; set; }

        public List<StakeLevel> StakeLevelList { get; set; }
        public StakeLevel StakeChoosen { get; set; }
        public GameType()
        {
            this.StakeLevelList = new List<StakeLevel>();
        }
    }
}