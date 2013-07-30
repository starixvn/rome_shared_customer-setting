using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Entities
{
    public class ProgressiveStatus
    {
        public int CustId { get; set; }

		public string Username { get; set; }

		public int RoleId { get; set; }

        public bool DisableCasinoBonus { get; set; }

        public bool DisableCasinoJackpot { get; set; }

        public bool DisableDownlinesCasinoBonus { get; set; }

        public bool DisableUplinesCasinoBonus { get; set; }
    }
}