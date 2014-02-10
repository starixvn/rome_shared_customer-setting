using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.CustomerSetting.OpusCasino.ViewModels
{
    public class StakeLevel
    {
        public int GameTypeId { get; set; }
        public string GameTypeName { get; set; }
        public string StakeLevelName { get; set; }
        public int StakeLevelId { get; set; }
        public bool IsChecked { get; set; }
    }
}