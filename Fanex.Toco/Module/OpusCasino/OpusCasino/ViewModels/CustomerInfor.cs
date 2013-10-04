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
    }
}