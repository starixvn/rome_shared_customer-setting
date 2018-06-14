namespace Fanex.CustomerSetting.Module.Casino.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using CasinoLibrary.Model.UserException;
    using CasinoLibrary.SBAgent;
    using Core;
    using Core.Models;
    using Core.Web;
    using Criteria;
    using Entities;
    using Interface;
    using Logging.Extensions.Web;
    using Repositories.Impl;
    using Repositories.Interface;
    using ViewModels;

    public class CasinoServiceBaseImpl : CasinoServiceBase
    {
        protected const int Super = 4;
        protected const int Master = 3;
        protected const int Agent = 2;
        protected const int Member = 1;

        protected readonly CasinoRepository casinoRepository;
        protected DataTable _tblCustMultipleStakeLevel;

        public CasinoServiceBaseImpl(CasinoRepository casinoRepository = null)
        {
            this.casinoRepository = casinoRepository ?? new CasinoRepositoryImpl();
        }

        public CasinoCustomer GetCustInfoIBC(int custid, int uplineId)
            => casinoRepository.GetCustInfoIBC(custid, uplineId);

        public CasinoCustomer GetCustSettingCS(CasinoCustomer custIBCModel)
        {
            var cust = new Customer();
            var dt = custIBCModel.GeneralInfo.CasinoSync || (custIBCModel.GeneralInfo.IBCCustID != 0 && custIBCModel.GeneralInfo.RoleId != Super) ? cust.GetCustSetting(custIBCModel.GeneralInfo.IBCCustID) : new DataTable();
            DataRow row = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
                custIBCModel.GeneralInfo.IsEnablePT = Utils.ToBoolean(row["IsEnablePT"]);
                custIBCModel.GeneralInfo.TotalWinLoss = custIBCModel.GeneralInfo.CasinoSync ? Utils.ToDouble(row["ActualMaxTransfer"]) : 0;
                custIBCModel.GeneralInfo.MaxWin = Utils.ToDouble(row["MaxWin"]);
                custIBCModel.GeneralInfo.MaxLose = Utils.ToDouble(row["MaxLose"]);

                custIBCModel.GeneralInfo.MaxTransferCredit = Utils.ToDouble(row["MaxTransferCredit"]);

                custIBCModel.GeneralInfo.IsMaxWin = Utils.ToBoolean(row["IsMaxWin"]);
                custIBCModel.GeneralInfo.IsMaxLose = Utils.ToBoolean(row["IsMaxLose"]);
                custIBCModel.GeneralInfo.PTByGameType = Utils.ToBoolean(row["PTByGameType"]);
                custIBCModel.GeneralInfo.PTByGameKind = Utils.ToBoolean(row["PTByGameKind"]);
                custIBCModel.GeneralInfo.RoleId = Utils.ToInt32(row["RoleId"]);
                custIBCModel.GeneralInfo.UpperCustID = Utils.ToInt32(row["UpperCustId"]);
                custIBCModel.GeneralInfo.IsAutoReset = Utils.ToBoolean(row["IsAutoReset"]);

                if (custIBCModel.GeneralInfo.RoleId == Master)
                {
                    custIBCModel.GeneralInfo.IsDisableAutoMPT = Utils.ToBoolean(row["DisableAutoMPT"]);
                }

                custIBCModel.GeneralInfo.NoCSData = false;
            }

            return custIBCModel;
        }

        protected bool UpdateCSStatus(
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

        protected static string Lang(string original)
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

        protected string SelectedStakeLevels(List<StakeLevel> stakes)
        {
            var selectedLevels = string.Empty;

            stakes.ForEach(item =>
            {
                selectedLevels += item.IsChecked ? item.StakeLevelID + "," : string.Empty;
            });

            return selectedLevels.EndsWith(",") ? selectedLevels.Substring(0, selectedLevels.Length - 1) : selectedLevels;
        }

        protected DataTable GameKindsDataTable()
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

        protected DataTable GameTypesDataTable()
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

        protected DataTable GameTypesStakesDataTable()
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

        protected List<StakeLevel> GetStakeLevels(
            int custId, 
            int gameTypeID, 
            int exchangeId = 0, 
            bool synced = true, 
            bool isSuper = false, 
            string site = null)
        {
            var stakeLevels = new List<StakeLevel>();
            var dt = custId != 0 ? GetCustStakeLevel(gameTypeID) : new BasicInfo() { SiteName = site }.GetGameTypeStakeLevel(gameTypeID, exchangeId);
            
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

        protected DataTable GetCustStakeLevel(int gameTypeID)
        {
            DataRow[] result = null;
            result = this._tblCustMultipleStakeLevel.Select("GameTypeID = " + gameTypeID);
            return result == null || result.Length == 0 ? this._tblCustMultipleStakeLevel.Clone() : result.CopyToDataTable();
        }

        protected Result CheckAllowedDownlines(int custId, byte custRole)
            => casinoRepository.ExecuteNonQuery(new AllowedDownlinesCriteria(custId, custRole));

        protected Result BuildMessages(
            DataTable resultKind,
            DataTable resultType,
            DataTable resultStake,
            CasinoSettingModel model)
        {
            var msg = string.Empty;

            foreach (DataRow item in resultKind.Rows)
            {
                msg += item.Field<string>("ErrMsg") + "<br />";
            }

            if (model.IsPtByGameType)
            {
                foreach (DataRow item in resultType.Rows)
                {
                    msg += item.Field<string>("ErrMsg") + "<br />";
                }
            }
            else
            {
                foreach (DataRow item in resultStake.Rows)
                {
                    msg += item.Field<string>("ErrMsg") + "<br />";
                }
            }

            msg = msg.EndsWith("<br />") ? msg.Substring(0, msg.LastIndexOf("<br />")) : msg;

            return new Result(msg == string.Empty || (resultKind.Rows.Count == 0 && resultType.Rows.Count == 0 && resultStake.Rows.Count == 0) ? 0 : 1, msg);
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
            => casinoRepository.DoUpdateCSStatusWithoutSync(
                custid, 
                syncCS, 
                disabledCS, 
                userId, 
                ip, 
                subAccId, 
                systemId, 
                lang, 
                out errMsg);
    }
}
