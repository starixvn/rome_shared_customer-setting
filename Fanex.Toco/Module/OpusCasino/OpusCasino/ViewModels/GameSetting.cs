using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.CustomerSetting.OpusCasino.ViewModels
{
    public class GameSetting
    {
        public string GameTypeName { get; set; }
        public int GameTypeID { get; set; }
       
        public List<StakeLevel> BettingLimitList { get; set; }

        public GameSetting()
        {
            this.BettingLimitList = new List<StakeLevel>();
        }
    }
}