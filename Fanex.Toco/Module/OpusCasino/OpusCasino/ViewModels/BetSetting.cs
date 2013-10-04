using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.CustomerSetting.OpusCasino.ViewModels
{
    public class BetSetting
    {
        public double MaxTransferAllowed { get; set; }

        public bool IsDailyAutoResetLimit { get; set; }

        public bool IsLimited { get; set; }

        public double MaxTransfer { get; set; }

        public bool IsValid
        {
            get;
            set; 
        }
    }
}