namespace Fanex.CustomerSetting.Module.Casino.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using CasinoLibrary.Model.UserException;
    using CasinoLibrary.SBAgent;
    using Core;
    using Core.Models;
    using Core.Web;
    using Criteria;
    using Entities;
    using Logging.Extensions.Web;
    using Repositories.Impl;
    using ViewModels;

    public partial class CasinoManager
    {
        private const int Super = 4;
        private const int Master = 3;
        private const int Agent = 2;
        private const int Member = 1;

        private readonly CasinoRepository casinoRepository;
        private DataTable _tblCustMultipleStakeLevel;

        public CasinoManager(CasinoRepository casinoRepository = null)
        {
            this.casinoRepository = casinoRepository ?? new CasinoRepositoryImpl();
        }

        public CasinoCustomer GetCustInfoIBC(int custid, int uplineId)
        {
            return this.casinoRepository.GetCustInfoIBC(custid, uplineId);
        }

        public void GetCustSettingCS(ref CasinoCustomer custIBC)
        {
            var cust = new Customer();
            var dt = custIBC.GeneralInfo.CasinoSync || (custIBC.GeneralInfo.IBCCustID != 0 && custIBC.GeneralInfo.RoleId != Super) ? cust.GetCustSetting(custIBC.GeneralInfo.IBCCustID) : new DataTable();
            DataRow row = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
                custIBC.GeneralInfo.IsEnablePT = Utils.ToBoolean(row["IsEnablePT"]);
                custIBC.GeneralInfo.TotalWinLoss = custIBC.GeneralInfo.CasinoSync ? Utils.ToDouble(row["ActualMaxTransfer"]) : 0;
                custIBC.GeneralInfo.MaxWin = Utils.ToDouble(row["MaxWin"]);
                custIBC.GeneralInfo.MaxLose = Utils.ToDouble(row["MaxLose"]);

                custIBC.GeneralInfo.MaxTransferCredit = Utils.ToDouble(row["MaxTransferCredit"]);

                custIBC.GeneralInfo.IsMaxWin = Utils.ToBoolean(row["IsMaxWin"]);
                custIBC.GeneralInfo.IsMaxLose = Utils.ToBoolean(row["IsMaxLose"]);
                custIBC.GeneralInfo.PTByGameType = Utils.ToBoolean(row["PTByGameType"]);
                custIBC.GeneralInfo.PTByGameKind = Utils.ToBoolean(row["PTByGameKind"]);
                custIBC.GeneralInfo.RoleId = Utils.ToInt32(row["RoleId"]);
                custIBC.GeneralInfo.UpperCustID = Utils.ToInt32(row["UpperCustId"]);
                custIBC.GeneralInfo.IsAutoReset = Utils.ToBoolean(row["IsAutoReset"]);

                if (custIBC.GeneralInfo.RoleId == Master)
                {
                    custIBC.GeneralInfo.IsDisableAutoMPT = Utils.ToBoolean(row["DisableAutoMPT"]);
                }

                custIBC.GeneralInfo.NoCSData = false;
            }
        }

        public bool UpdateCSStatus(
            int custid, 
            bool syncCS, 
            bool disabledCS, 
            int userId, 
            string ip, 
            int subAccId, 
            byte systemId, 
            string lang, 
            string username, 
            string subUsername, 
            out string errMsg)
        {
            var cust = new Customer();
            var status = this.DoUpdateCSStatusWithoutSync(custid, syncCS, disabledCS, userId, ip, subAccId, systemId, lang, out errMsg);

            if (!status)
            {
                return false;
            }

            try
            {
                status = cust.UpdateCustEnableCasino(custid, !disabledCS, false, ip, custid, username, 2);
            }
            catch (ErrorException casinoEx)
            {
                ExceptionHandler.Handle(new Exception(casinoEx.Message), TraceEventType.Warning);
                errMsg = casinoEx.Message;
                status = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, TraceEventType.Information);
                errMsg = ex.Message;
                status = false;
            }

            return status;
        }

        public bool IsEnabledJackpot(int custId)
        {
            var productIds = casinoRepository.GetEnabledJackpotProducts(new Criteria.GetEnabledJackpotProductsCriteria(custId, Configurations.AppSettings.LocalTest));

            return productIds.Any();
        }

        private static string Lang(string original)
        {
            switch (AppSession.Language)
            {
                case "en-US": return original + "_En";
                case "zh-TW": return original + "_Ct"; // Traditional
                case "zh-CN": return original + "_Cs"; // Simplified
                case "ja-JP": return original + "_Jp";
                case "th-TH": return original + "_Th"; // Thai
                case "ko-KR": return original + "_Ko";
                default: return original + "_En";
            }
        }

        private bool DoUpdateCSStatusWithoutSync(
            int custid, 
            bool syncCS, 
            bool disabledCS, 
            int userId, 
            string ip, 
            int subAccId, 
            byte systemId, 
            string lang, 
            out string errMsg)
        {
            return this.casinoRepository.DoUpdateCSStatusWithoutSync(custid, syncCS, disabledCS, userId, ip, subAccId, systemId, lang, out errMsg);
        }

        private string SelectedStakeLevels(List<StakeLevel> stakes)
        {
            var selectedLevels = string.Empty;

            stakes.ForEach(item =>
            {
                selectedLevels += item.IsChecked ? item.StakeLevelID + "," : string.Empty;
            });

            return selectedLevels.EndsWith(",") ? selectedLevels.Substring(0, selectedLevels.Length - 1) : selectedLevels;
        }

        private DataTable GameKindsDataTable()
        {
            var dt = new DataTable();
            dt.Columns.AddRange(
                new DataColumn[]
                {
                    new DataColumn("IBCCustID"),
                    new DataColumn("GameKind"),
                    new DataColumn("HPositionTaking"),
                    new DataColumn("SPositionTaking"),
                    new DataColumn("MPositionTaking"),
                    new DataColumn("APositionTaking"),
                    new DataColumn("IsAutoPositionTaking"),
                    new DataColumn("AutoPositionTaking"),
                    new DataColumn("IsAutoMPositionTaking"),
                    new DataColumn("AutoMPositionTaking"),
                    new DataColumn("MinPositionTaking"),
                    new DataColumn("MaxPositionTaking"),
                    new DataColumn("SMAgentRollingRate"),
                    new DataColumn("MAgentRollingRate"),
                    new DataColumn("AgentRollingRate"),
                    new DataColumn("MemberRollingRate"),
                    new DataColumn("RoleId"),
                    new DataColumn("FromIP"),
                    new DataColumn("ModifyBy"),
                    new DataColumn("ModifyUserName"),
                    new DataColumn("FromSystem")
                });

            return dt;
        }

        private DataTable GameTypesDataTable()
        {
            var dt = new DataTable();
            dt.Columns.AddRange(
                new DataColumn[]
                {
                    new DataColumn("IBCCustID"),
                    new DataColumn("GameTypeID"),
                    new DataColumn("HPositionTaking"),
                    new DataColumn("SPositionTaking"),
                    new DataColumn("MPositionTaking"),
                    new DataColumn("APositionTaking"),
                    new DataColumn("IsAutoPositionTaking"),
                    new DataColumn("AutoPositionTaking"),
                    new DataColumn("IsAutoMPositionTaking"),
                    new DataColumn("AutoMPositionTaking"),
                    new DataColumn("MinPositionTaking"),
                    new DataColumn("MaxPositionTaking"),
                    new DataColumn("SMAgentRollingRate"),
                    new DataColumn("MAgentRollingRate"),
                    new DataColumn("AgentRollingRate"),
                    new DataColumn("MemberRollingRate"),
                    new DataColumn("StakeLevelList"),
                    new DataColumn("RoleId"),
                    new DataColumn("FromIP"),
                    new DataColumn("ModifyBy"),
                    new DataColumn("ModifyUserName"),
                    new DataColumn("FromSystem")
                });

            return dt;
        }

        private DataTable GameTypesStakesDataTable()
        {
            var dt = new DataTable();
            dt.Columns.AddRange(
                new DataColumn[]
                {
                    new DataColumn("IBCCustID"),
                    new DataColumn("GameTypeID"),
                    new DataColumn("StakeLevelList"),
                    new DataColumn("RoleId"),
                    new DataColumn("FromIP"),
                    new DataColumn("ModifyBy"),
                    new DataColumn("ModifyUserName"),
                    new DataColumn("FromSystem")
                });

            return dt;
        }

        private List<StakeLevel> GetStakeLevels(
            int custId, 
            int gameTypeID, 
            int exchangeId = 0, 
            bool synced = true, 
            bool isSuper = false, 
            string site = null)
        {
            var cust = new Customer();
            var stakeLevels = new List<StakeLevel>();
            var dt = custId != 0 ? GetCustStakeLevel(gameTypeID) : new BasicInfo() { SiteName = site }.GetGameTypeStakeLevel(gameTypeID, exchangeId);
            //var dt = custId != 0 ? cust.GetCustStakeLevel(custId, gameTypeID) : new BasicInfo() { SiteName = site }.GetGameTypeStakeLevel(gameTypeID, exchangeId);
            dt = dt == null ? new BasicInfo() { SiteName = site }.GetGameTypeStakeLevel(gameTypeID, exchangeId) : dt;

            foreach (DataRow row in dt.Rows)
            {
                bool isCheck = true;
                if (!custId.Equals(0))
                {
                    isCheck = (isSuper && !synced) ? true :
                        dt.Columns.IndexOf("IsChecked") != -1 ? Utils.ToBoolean(row["IsChecked"]) : false;
                }

                var stake = new StakeLevel
                {
                    StakeLevelID = Utils.ToInt32(row["StakeLevelID"]),
                    StakeLevelName = Utils.ToString(row["StakeLevelName"]),
                    IsChecked = isCheck
                };

                if (synced || stake.IsChecked)
                {
                    stakeLevels.Add(stake);
                }
            }

            return stakeLevels;
        }

        private DataTable GetCustStakeLevel(int gameTypeID)
        {
            DataRow[] result = null;
            result = this._tblCustMultipleStakeLevel.Select("GameTypeID = " + gameTypeID);
            return result == null || result.Length == 0 ? this._tblCustMultipleStakeLevel.Clone() : result.CopyToDataTable();
        }

        private Result CheckAllowedDownlines(int custId, byte custRole)
            => casinoRepository.ExecuteNonQuery(new AllowedDownlinesCriteria(custId, custRole));
    }
}
