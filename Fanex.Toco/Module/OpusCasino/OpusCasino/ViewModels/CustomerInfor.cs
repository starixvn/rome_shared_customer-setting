using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.CustomerSetting.OpusCasino.ViewModels
{
    public class CustomerInfor
    {
        public int CustID { get; set; }
        public string Username { get; set; }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }
        public bool IsEnable { get; set; }
        public bool IsDownline { get; set; }

        public CustomerInfor()
        {
            this.CustID = 1373389;
            this.Username = "testuser";
            this.NickName = "testcs";
            this.FirstName = "David";
            this.LastName = "Tran";
            this.Phone = "564645654";
            this.MobilePhone = "56658645465";
            this.Fax = "6589456";
            this.IsEnable = true;
            this.IsDownline = false;
        }
    }
}