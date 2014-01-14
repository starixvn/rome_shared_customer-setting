using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.CustomerSetting.OpusCasino.ViewModels
{
    public class CustomerInfo
    {
        public int CustId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }

        public int UserId { get; set; }
        public int SubAccId { get; set; }
        public string Language { get; set; }
        public string Ip { get; set; }
        //int userId, int subAccId, string ip, string language

        public bool IsUpdateStatus { get; set; }
        public bool IsDisable { get; set; }
        public bool IsDisableDownlines { get; set; }

        //public CustomerInfo()
        //{
        //    this.CustId = 1373389;
        //    this.RoleId = 3;
        //    this.Username = "testuser";
        //    this.NickName = "testcs";
        //    this.FirstName = "David";
        //    this.LastName = "Tran";
        //    this.Phone = "564645654";
        //    this.MobilePhone = "56658645465";
        //    this.Fax = "6589456";
        //    this.IsEnable = false;
        //    this.IsDownline = true;
        //}
    }
}