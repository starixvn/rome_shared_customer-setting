// -----------------------------------------------------------------------
// <copyright file="CustomerInfo.cs" company="Nexcel Co.Ltd">
// TODO: Defines left panel model info.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.LeftPanel.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// TODO: Model for left panel.
    /// </summary>
    public class CustomerModel
    {
        public CustomerModel()
        {
            this.BalanceInfo = new BalanceInfo();
            this.BetList = new BetList();
            this.CustomerList = new CustomerList();
            this.DownlineList = new DownlineList();
            this.Login = new Login();
            this.OnlineList = new OnlineList();
            this.PersonalInfo = new PersonalInfo();
            this.Setting = new Setting();
            this.StatementList = new StatementList();
        }

        public PersonalInfo PersonalInfo { get; set; }

        public CustomerList CustomerList { get; set; }

        public StatementList StatementList { get; set; }

        public BetList BetList { get; set; }

        public BalanceInfo BalanceInfo { get; set; }

        public DownlineList DownlineList { get; set; }

        public Setting Setting { get; set; }

        public Login Login { get; set; }

        public OnlineList OnlineList { get; set; }
    }
    
    public class PersonalInfo : DownlineList
    {
        public PersonalInfo() : base()
        {            
        }
                
        public string Level { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Fax { get; set; }
    }

    public class CustomerList
    {
        public CustomerList()
        {
            this.Customers = new DataTable();
        }

        public DataTable Customers { get; set; }
    }

    public class BetList
    {
        public BetList()
        {
            this.Filters = new List<Dictionary<string, bool>>();
            this.Bets = new List<BetItem>();
        }

        public List<Dictionary<string, bool>> Filters { get; set; }

        public List<BetItem> Bets { get; set; }
    }

    public class BetItem
    {
        public int BetId { get; set; }

        public DateTime TransTime { get; set; }

        public string Choice { get; set; }

        public decimal Odds { get; set; }

        public decimal Stake { get; set; }

        public decimal WinLoss { get; set; }

        public string Status { get; set; }

        public decimal SuperPtComm { get; set; }

        public decimal MasterPtComm { get; set; }

        public decimal AgentPtComm { get; set; }
    }

    public class Statement
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Remark { get; set; }

        public decimal Amount { get; set; }

        public decimal Total { get; set; }
    }

    public class StatementList
    {
        public StatementList()
        {
            this.Statements = new List<Statement>();
        }

        public List<Statement> Statements { get; set; }
    }

    public class BalanceInfo
    {
        public BalanceInfo()
        {
            this.Balances = new List<Dictionary<string, decimal>>();
            this.Statuses = new List<Dictionary<string, Status>>();
        }

        public string Currency { get; set; }

        public List<Dictionary<string, decimal>> Balances { get; set; }

        public List<Dictionary<string, Status>> Statuses { get; set; }
    }

    public class Status
    {
        public int TotalActive { get; set; }

        public int TotalClosed { get; set; }

        public int TotalSuspended { get; set; }
    }

    public class DownlineList : BasicInfo
    {
        public DownlineList()
        {
            this.Downlines = new List<Dictionary<int, Dictionary<string, string>>>();
        }

        public List<Dictionary<int, Dictionary<string, string>>> Downlines { get; set; }
    }

    public class Setting : BasicInfo
    {
        public Setting()
        {
            this.EnableItems = new List<Dictionary<string, bool>>();
            this.EditItems = new List<Dictionary<string, string>>();
        }
        
        public List<Dictionary<string, bool>> EnableItems { get; set; }

        public List<Dictionary<string, string>> EditItems { get; set; }
    }

    public class Login : OnlineInfo
    {
        public DateTime CreatedDate { get; set; }

        public DateTime LastTrans { get; set; }
    }

    public class OnlineList
    {
        public OnlineList()
        {
            this.Onlines = new List<OnlineInfo>();
        }

        public List<OnlineInfo> Onlines { get; set; }
    }

    public class OnlineInfo : BasicInfo
    {
        public string IP { get; set; }

        public DateTime LoginTime { get; set; }
    }

    public class BasicInfo
    {
        public int CustId { get; set; }

        public int RoleId { get; set; }

        public int UplineRoleId { get; set; }
    }
}