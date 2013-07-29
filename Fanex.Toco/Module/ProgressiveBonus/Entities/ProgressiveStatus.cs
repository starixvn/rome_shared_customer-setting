using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Entities
{
    public class ProgressiveStatus
    {
        public int CustId { get; set; }

        public bool DisableCasinoBonus { get; set; }

        public bool DisableCasinoJackpot { get; set; }

        public bool DisableDownlinesCasinoBonus { get; set; }
    }
}