// --------------------------------------------------------------------------
// <copyright file="CasinoManager.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.4.11">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.Casino.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using CasinoLibrary.SBAgent;
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Repositories.Impl;
    using Fanex.CustomerSetting.Module.Casino.ViewModels;
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CasinoManager
    {
        private const int Super = 4;
        private const int Master = 3;
        private const int Agent = 2;
        private const int Member = 1;
        private const int Admin = 5;
        private decimal MaxPT = Utils.ToDecimal(ConfigurationManager.AppSettings["MaxPT"]).Format();
        private decimal MinPT = Utils.ToDecimal(ConfigurationManager.AppSettings["MinPT"]).Format();
        private decimal MaxComm = Utils.ToDecimal(ConfigurationManager.AppSettings["MaxComm"]).Format(4);
        private const string CasinoNo = "01";

        private static CasinoManager instance = new CasinoManager();
        private readonly CasinoDao _casino = new CasinoDao();

        public static CasinoManager Instance
        {
            get
            {
                return CasinoManager.instance ?? new CasinoManager();
            }
        }

        public CasinoSettingModel SuperCasinoModel(int custId, int uplineId)
        {
            var gameModel = new CasinoSettingModel();
            var gameGroups = new List<GameGroup>();
            var custInfo = this.GetCustInfoIBC(custId, uplineId);
            var synced = custInfo.CasinoSync;
            var id = synced ? custId : 0;
            var cust = new Customer();
            var dt = synced ? cust.GetCustGameKind(custId) : new BasicInfo().GetGameKind(CasinoNo);
           
            if (custInfo.RoleId != Super)
            {
                throw new Exception("Invalid custId");
            }
                        
            this.GetCustSettingCS(ref custInfo);
            gameModel.IsPtByGameType = custInfo.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();

                // Commission
                gameGroup.Sco = MaxComm;
                gameGroup.Mco = !synced ? 0 : Utils.ToDecimal(row["SMAgentRollingRate"]).Format(4);

                // Game Group Info
                gameGroup.GameGroupID = !synced ? Utils.ToInt32(row["CodeID"]) : Utils.ToInt32(row["GameKind"]);
                gameGroup.GameGroupName = !synced ? Utils.ToString(row[Lang("CodeName")]) : Utils.ToString(row["GameKindName"]);

                // Super preset PT
                gameGroup.S4 = MaxPT;
                gameGroup.S3 = !synced ? 0 : Utils.ToDecimal(row["MaxPositionTaking"]).Format();

                // Master Min PT
                gameGroup.N4 = gameGroup.S3;
                gameGroup.N3 = !synced ? 0 : Utils.ToDecimal(row["MinPositionTaking"]).Format();

                // Assign game types
                var gameTypes = this.GetGameTypeSuper(id, gameGroup.GameGroupID, synced, ref gameGroup, custInfo.Currency, gameModel.IsPtByGameType);
                gameGroup.GameTypes = gameTypes;

                gameGroups.Add(gameGroup);
            }

            gameModel.GameGroups = gameGroups;

            return gameModel;
        }

        public CasinoSettingModel MasterCasinoModel(int custId, int uplineId)
        {
            var gameModel = new CasinoSettingModel();
            var gameGroups = new List<GameGroup>();
            var custInfo = this.GetCustInfoIBC(custId, uplineId);
            var synced = custInfo.CasinoSync;
            var id = synced ? custId : uplineId;
            var cust = new Customer();
            var dt = cust.GetCustGameKind(id);

            var casinoSettingUpline = new CasinoCustomer();

            // Gets upline settings
            casinoSettingUpline.IBCCustID = uplineId;
            this.GetCustSettingCS(ref casinoSettingUpline);

            if (casinoSettingUpline.RoleId - custInfo.RoleId != 1 || casinoSettingUpline.RoleId != Super || custInfo.RoleId != Master)
            {
                throw new Exception("Invalid custId and uplineId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();

                // Commission
                gameGroup.Mco = Utils.ToDecimal(row["MAgentRollingRate"]).Format(4); // synced ? Utils.ToDecimal(row["UplineMAgentRollingRate"]).Format(4) : Utils.ToDecimal(row["MAgentRollingRate"]).Format(4);
                gameGroup.Sco = synced ? Utils.ToDecimal(row["UplineSMAgentRollingRate"]).Format(4) : Utils.ToDecimal(row["SMAgentRollingRate"]).Format(4);

                if (!synced)
                {
                    gameGroup.Mco = gameGroup.Sco;
                }

                // Game Group Info
                gameGroup.GameGroupID = Utils.ToInt32(row["GameKind"]);
                gameGroup.GameGroupName = Utils.ToString(row[Lang("GameKindName")]);

                // Super preset PT
                gameGroup.S4 = synced ? Utils.ToDecimal(row["UplineMaxPositionTaking"]).Format(2) : Utils.ToDecimal(row["MaxPositionTaking"]).Format(2);
                gameGroup.S3 = Utils.ToDecimal(row["SPositionTaking"]).Format(2); // synced ? Utils.ToDecimal(row["UplineSPositionTaking"]).Format(2) : Utils.ToDecimal(row["SPositionTaking"]).Format(2);

                // Super Auto PT
                gameGroup.Is3 = Utils.ToBoolean(row["IsAutoPositionTaking"]);
                gameGroup.Us3 = Utils.ToDecimal(row["AutoPositionTaking"]).Format(2);

                // Master Max PT
                gameGroup.M3 = synced ? Utils.ToDecimal(row["MPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineMPositionTaking"]).Format(2);

                // Master Min PT
                gameGroup.N3 = synced ? Utils.ToDecimal(row["MinPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineMinPositionTaking"]).Format(2);

                // Assign game types
                var gameTypes = this.GetGameTypeMaster(id, gameGroup.GameGroupID, synced, ref gameGroup, gameModel.IsPtByGameType);
                gameGroup.GameTypes = gameTypes;

                gameGroups.Add(gameGroup);
            }

            gameModel.GameGroups = gameGroups;

            return gameModel;
        }

        public CasinoSettingModel AgentCasinoModel(int custId, int uplineId)
        {
            var gameModel = new CasinoSettingModel();
            var gameGroups = new List<GameGroup>();
            var custInfo = this.GetCustInfoIBC(custId, uplineId);
            var synced = custInfo.CasinoSync;
            var id = synced ? custId : uplineId;
            var cust = new Customer();
            var dt = cust.GetCustGameKind(id);

            var casinoSettingUpline = new CasinoCustomer();

            // Gets upline settings
            casinoSettingUpline.IBCCustID = uplineId;
            this.GetCustSettingCS(ref casinoSettingUpline);

            if (casinoSettingUpline.RoleId - custInfo.RoleId != 1 || casinoSettingUpline.RoleId != Master || custInfo.RoleId != Agent)
            {
                throw new Exception("Invalid custId and uplineId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();

                // Commission
                gameGroup.Aco = synced ? Utils.ToDecimal(row["AgentRollingRate"]).Format(4) : Utils.ToDecimal(row["UplineAgentRollingRate"]).Format(4);
                gameGroup.Mco = synced ? Utils.ToDecimal(row["UplineMAgentRollingRate"]).Format(4) : Utils.ToDecimal(row["MAgentRollingRate"]).Format(4);

                if (!synced)
                {
                    gameGroup.Aco = gameGroup.Mco;
                }

                // Game Group Info
                gameGroup.GameGroupID = Utils.ToInt32(row["GameKind"]);
                gameGroup.GameGroupName = Utils.ToString(row[Lang("GameKindName")]);

                // Master preset PT
                gameGroup.M3 = !synced ? Utils.ToDecimal(row["MPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineMPositionTaking"]).Format(2);
                gameGroup.M2 = Utils.ToDecimal(row["MPositionTaking"]).Format(2); // !synced ? Utils.ToDecimal(row["MPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineMPositionTaking"]).Format(2);

                // Agent Max PT
                gameGroup.A2 = synced ? Utils.ToDecimal(row["APositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineAPositionTaking"]).Format(2);

                // Agent Min PT
                gameGroup.N2 = !synced ? 0 : Utils.ToDecimal(row["MinPositionTaking"]).Format(2);

                // Assign game types
                var gameTypes = this.GetGameTypeAgent(id, gameGroup.GameGroupID, synced, ref gameGroup, gameModel.IsPtByGameType);
                gameGroup.GameTypes = gameTypes;

                gameGroups.Add(gameGroup);
            }

            gameModel.GameGroups = gameGroups;

            return gameModel;
        }

        public CasinoSettingModel AgentCasinoModelBySuper(int custId, int uplineId)
        {
            var gameModel = new CasinoSettingModel();
            var gameGroups = new List<GameGroup>();
            var custInfo = this.GetCustInfoIBC(custId, uplineId);
            var synced = custInfo.CasinoSync;
            var id = synced ? custId : uplineId;
            var cust = new Customer();
            var dt = cust.GetCustGameKind(id);

            var casinoSettingUpline = new CasinoCustomer();

            // Gets upline settings
            casinoSettingUpline.IBCCustID = uplineId;
            this.GetCustSettingCS(ref casinoSettingUpline);

            if (casinoSettingUpline.RoleId - custInfo.RoleId != 2 || casinoSettingUpline.RoleId != Super || custInfo.RoleId != Agent)
            {
                throw new Exception("Invalid custId and uplineId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();

                // Game Group Info
                gameGroup.GameGroupID = Utils.ToInt32(row["GameKind"]);
                gameGroup.GameGroupName = Utils.ToString(row[Lang("GameKindName")]);

                // Master preset PT
                gameGroup.M3 = synced ? Utils.ToDecimal(row["MPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineMPositionTaking"]).Format(2);
                gameGroup.M2 = synced ? Utils.ToDecimal(row["APositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineAPositionTaking"]).Format(2);

                // Assign game types
                var gameTypes = this.GetGameTypeAgentBySuper(id, gameGroup.GameGroupID, synced);
                gameGroup.GameTypes = gameTypes;

                gameGroups.Add(gameGroup);
            }

            gameModel.GameGroups = gameGroups;

            return gameModel;
        }

        public CasinoSettingModel MemberCasinoModel(int custId, int uplineId)
        {
            var gameModel = new CasinoSettingModel();
            var gameGroups = new List<GameGroup>();
            var custInfo = this.GetCustInfoIBC(custId, uplineId);
            var synced = custInfo.CasinoSync;
            var id = synced ? custId : uplineId;
            var cust = new Customer();
            var dt = cust.GetCustGameKind(id);

            var casinoSettingUpline = new CasinoCustomer();

            // Gets upline settings
            casinoSettingUpline.IBCCustID = uplineId;
            this.GetCustSettingCS(ref casinoSettingUpline);

            if (casinoSettingUpline.RoleId - custInfo.RoleId != 1 || casinoSettingUpline.RoleId != 2 || custInfo.RoleId != 1)
            {
                throw new Exception("Invalid custId and uplineId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();

                // Commission
                gameGroup.co = synced ? Utils.ToDecimal(row["MemberRollingRate"]).Format(4) : Utils.ToDecimal(row["UplineMemberRollingRate"]).Format(4);
                gameGroup.Aco = synced ? Utils.ToDecimal(row["UplineAgentRollingRate"]).Format(4) : Utils.ToDecimal(row["AgentRollingRate"]).Format(4);

                if (!synced)
                {
                    gameGroup.co = gameGroup.Aco;
                }

                // Game type info
                gameGroup.GameGroupID = Utils.ToInt32(row["GameKind"]);
                gameGroup.GameGroupName = Utils.ToString(row[Lang("GameKindName")]);

                // Agent preset PT
                gameGroup.M2 = synced ? Utils.ToDecimal(row["UplineMaxPositionTaking"]).Format(2) : Utils.ToDecimal(row["MaxPositionTaking"]).Format(2);
                gameGroup.M1 = Utils.ToDecimal(row["APositionTaking"]).Format(2); // !synced ? Utils.ToDecimal(row["APositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineAPositionTaking"]).Format(2);
                gameGroup.N2 = synced ? Utils.ToDecimal(row["UplineMinPositionTaking"]).Format(2) : Utils.ToDecimal(row["MinPositionTaking"]).Format(2);

                // Assign game types
                var gameTypes = this.GetGameTypeMember(id, gameGroup.GameGroupID, synced, ref gameGroup, gameModel.IsPtByGameType);
                gameGroup.GameTypes = gameTypes;

                gameGroups.Add(gameGroup);
            }

            gameModel.GameGroups = gameGroups;

            return gameModel;
        }

        public CasinoSettingModel MemberCasinoModelBySuper(int custId, int uplineId)
        {
            var gameModel = new CasinoSettingModel();
            var gameGroups = new List<GameGroup>();
            var custInfo = this.GetCustInfoIBC(custId, uplineId);
            var cust = new Customer();
            var dt = cust.GetCustGameKind_EditMember(custId);
            var dtsuper = cust.GetCustGameKind(custId);

            var casinoSettingUpline = new CasinoCustomer();

            // Gets upline settings
            casinoSettingUpline.IBCCustID = uplineId;
            this.GetCustSettingCS(ref casinoSettingUpline);

            if (casinoSettingUpline.RoleId - custInfo.RoleId != 3 || casinoSettingUpline.RoleId != Super || custInfo.RoleId != Member)
            {
                throw new Exception("Invalid custId and uplineId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();
                var isAutoOnMember = false;

                foreach (DataRow dtrow in dtsuper.Rows)
                {
                    if (row["GameKind"].Equals(dtrow["GameKind"]))
                    {
                        isAutoOnMember = Utils.ToBoolean(dtrow["SuperIsAutoPositionTaking"]);
                        break;
                    }
                }

                // Game type info
                gameGroup.GameGroupID = Utils.ToInt32(row["GameKind"]);
                gameGroup.GameGroupName = Utils.ToString(row[Lang("GameKindName")]);

                // Super preset PT
                gameGroup.S4 = Utils.ToDecimal(row["SuperMaxPositionTaking"]).Format(2);
                gameGroup.S3 = Utils.ToDecimal(row["SPositionTaking"]).Format(2);
                gameGroup.N4 = Utils.ToDecimal(row["SuperMinPositionTaking"]).Format(2);

                // Super Auto PT
                gameGroup.Is1 = Utils.ToBoolean(row["IsAutoPositionTaking"]);
                gameGroup.Is3 = isAutoOnMember;
                gameGroup.Us3 = Utils.ToDecimal(row["AutoPositionTaking"]).Format(2);

                // Master preset PT
                gameGroup.M3 = Utils.ToDecimal(row["MPositionTaking"]).Format(2);

                // Agent preset PT
                gameGroup.M2 = Utils.ToDecimal(row["APositionTaking"]).Format(2);
                gameGroup.N3 = gameGroup.Is3 == true ? gameGroup.N4 - gameGroup.M3 - gameGroup.M2 : Math.Max((decimal)(gameGroup.N4 - gameGroup.M3 - gameGroup.M2), 0m);

                // Assign game types
                var gameTypes = this.GetGameTypeMemberBySuper(custId, gameGroup.GameGroupID, ref gameGroup, gameModel.IsPtByGameType);
                gameGroup.GameTypes = gameTypes;

                gameGroups.Add(gameGroup);
            }

            gameModel.GameGroups = gameGroups;

            return gameModel;
        }

        public CasinoSettingModel MemberCasinoModelByMaster(int custId, int uplineId)
        {
            var gameModel = new CasinoSettingModel();
            var gameGroups = new List<GameGroup>();
            var custInfo = this.GetCustInfoIBC(custId, uplineId);
            var cust = new Customer();
            var dt = cust.GetCustGameKind(custId);

            var casinoSettingUpline = new CasinoCustomer();

            // Gets upline settings
            casinoSettingUpline.IBCCustID = uplineId;
            this.GetCustSettingCS(ref casinoSettingUpline);

            if (casinoSettingUpline.RoleId - custInfo.RoleId != 2 || casinoSettingUpline.RoleId != Master || custInfo.RoleId != Member)
            {
                throw new Exception("Invalid custId and uplineId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();

                // Game type info
                gameGroup.GameGroupID = Utils.ToInt32(row["GameKind"]);
                gameGroup.GameGroupName = Utils.ToString(row[Lang("GameKindName")]);

                // Super preset PT
                gameGroup.S4 = Utils.ToDecimal(row["MasterMaxPositionTalking"]).Format(2);
                gameGroup.S3 = Utils.ToDecimal(row["SPositionTaking"]).Format(2);

                // Agent preset PT
                gameGroup.M3 = Utils.ToDecimal(row["MPositionTaking"]).Format(2);
                gameGroup.M2 = Utils.ToDecimal(row["APositionTaking"]).Format(2);
                var min = Utils.ToDecimal(row["MasterMinPositionTaking"]).Format(2) - Utils.ToDecimal(row["APositionTaking"]).Format(2);
                gameGroup.N2 = min < 0 ? 0 : min;

                // Assign game types
                var gameTypes = this.GetGameTypeMemberByMaster(custId, gameGroup.GameGroupID, ref gameGroup, gameModel.IsPtByGameType);
                gameGroup.GameTypes = gameTypes;

                gameGroups.Add(gameGroup);
            }

            gameModel.GameGroups = gameGroups;

            return gameModel;
        }

        public CasinoCustomer GetCustInfoIBC(int custid, int uplineId)
        {
            return this._casino.GetCustInfoIBC(custid, uplineId);
        }

        public void GetCustSettingCS(ref CasinoCustomer custIBC)
        {
            var cust = new Customer();
            var dt = custIBC.CasinoSync || (custIBC.IBCCustID != 0 && custIBC.RoleId != Super) ? cust.GetCustSetting(custIBC.IBCCustID) : new DataTable();
            DataRow row = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
                custIBC.IsEnablePT = Utils.ToBoolean(row["IsEnablePT"]);
                custIBC.TotalWinLoss = custIBC.CasinoSync ? Utils.ToDouble(row["ActualMaxTransfer"]) : 0;
                custIBC.MaxWin = Utils.ToDouble(row["MaxWin"]);
                custIBC.MaxLose = Utils.ToDouble(row["MaxLose"]);

                custIBC.MaxTransferCredit = Utils.ToDouble(row["MaxTransferCredit"]);

                custIBC.IsMaxWin = Utils.ToBoolean(row["IsMaxWin"]);
                custIBC.IsMaxLose = Utils.ToBoolean(row["IsMaxLose"]);
                custIBC.PTByGameType = Utils.ToBoolean(row["PTByGameType"]);
                custIBC.PTByGameKind = Utils.ToBoolean(row["PTByGameKind"]);
                custIBC.RoleId = Utils.ToInt32(row["RoleId"]);
                custIBC.UpperCustID = Utils.ToInt32(row["UpperCustId"]);
                custIBC.IsAutoReset = Utils.ToBoolean(row["IsAutoReset"]);

                // Get max transfer information
                if (custIBC.UpperCustID != 0)
                {
                    var dt2 = cust.GetDefaultMaxTransfer(custIBC.UpperCustID, 0, false);
                    custIBC.MaxTransferCreditLimit = Utils.ToDouble(dt2.Rows[0]["MaxLimit"]);
                    custIBC.MinTransferCreditLimit = Utils.ToDouble(dt2.Rows[0]["MinLimit"]);

                    custIBC.DefaultTransferCredit = Utils.ToDouble(dt2.Rows[0]["DefMaxTransferCredit"]);
                }

                custIBC.NoCSData = false;
            }
        }

        public Result UpdateSuper(int custId, string custName, int uplineId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel settingModel, CasinoCustomer customerModel)
        {
            var errMsg = string.Empty;
            var messages = string.Empty;
            var settingsInserted = true;
            var statusUpdated = true;
            var maxTransferUpdated = false;
            var casinoPTResult = new Result();
            var enablePTUpdated = false;
            var cust = new Customer();

            if (customerModel.UplineRoleId != 5 || customerModel.RoleId != 4)
            {
                return new Result(1, "Invalid custId and uplineId");
            }

            try
            {
                settingsInserted = cust.InsertCustSetting(
                    "01",
                    custId,
                    custName,
                    customerModel.UserName2,
                    customerModel.FirstName,
                    customerModel.LastName,
                    customerModel.UserPwd,
                    customerModel.Phone,
                    customerModel.MobilePhone,
                    customerModel.Currency,
                    customerModel.Fax,
                    customerModel.Closed,
                    customerModel.Suspend,
                    customerModel.IsEnablePT,
                    (decimal)customerModel.MaxWin,
                    (decimal)customerModel.MaxLose,
                    true,
                    (decimal)customerModel.MaxTransferCredit,
                    customerModel.SIBCCustID,
                    custId,
                    customerModel.AIBCCustID,
                    customerModel.PTByGameType,
                    customerModel.RoleId.ToString(),
                    DateTime.Now,
                    ip,
                    modifiedId,
                    modifiedBy,
                    fromSystem,
                    customerModel.IsAutoReset,
                    false,
                    false);
            }
            catch (Exception)
            {
                // Bypass this exception because this customer had setting but update status failed, before.
            }

            // Update Status for direct downline only
            if ((settingsInserted && !customerModel.CasinoSync) || customerModel.CasinoSync)
            {
                statusUpdated = this.UpdateCSStatus(
                    custId,
                    true,
                    customerModel.UplineDisabledCasino ? true : customerModel.CasinoDisabled,
                    customerModel.EnableDownline,
                    uplineId,
                    string.Empty,
                    0,
                    "en",
                    customerModel.UserName,
                    string.Empty,
                    out errMsg);
            }

            // Enable PT/Update max transfer for direct downline only
            if (statusUpdated)
            {
                enablePTUpdated = cust.UpdateCustEnablePT(custId, true);

                maxTransferUpdated = cust.UpdateMaxTransfer(
                    custId,
                    true,
                    (decimal)customerModel.MaxTransferCredit,
                    customerModel.IsAutoReset,
                    ip,
                    uplineId,
                    modifiedBy,
                    fromSystem);
            }

            // Update PT/Betting limit settings
            casinoPTResult = this.UpdateMasterSettings(
                custId,
                customerModel.RoleId,
                uplineId,
                customerModel.UplineRoleId,
                ip,
                modifiedId,
                modifiedBy,
                fromSystem,
                settingModel);

            // Gets error messages
            messages += string.IsNullOrWhiteSpace(errMsg) && casinoPTResult.IsSuccess ? string.Empty : !string.IsNullOrWhiteSpace(errMsg) ? errMsg + "<br />" + casinoPTResult.ErrorMessage : casinoPTResult.ErrorMessage;

            var msgs = messages.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var retMsg = string.Empty;

            foreach (var item in msgs)
            {
                retMsg += string.Format("{0}{1}", item, ", ");
            }

            return new Result(retMsg == string.Empty || retMsg == "<br />" ? 0 : 1, retMsg);
        }

        public Result UpdateMaster(int custId, string custName, int uplineId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel settingModel, CasinoCustomer customerModel)
        {
            var errMsg = string.Empty;
            var messages = string.Empty;
            var settingsInserted = true;
            var statusUpdated = true;
            var maxTransferUpdated = false;
            var casinoPTResult = new Result();
            var enablePTUpdated = false;
            var cust = new Customer();

            if (customerModel.UplineRoleId != 4 || customerModel.RoleId != 3)
            {
                return new Result(1, "Invalid custId and uplineId");
            }

            try
            {
                settingsInserted = cust.InsertCustSetting(
                    "01",
                    custId,
                    custName,
                    customerModel.UserName2,
                    customerModel.FirstName,
                    customerModel.LastName,
                    customerModel.UserPwd,
                    customerModel.Phone,
                    customerModel.MobilePhone,
                    customerModel.Currency,
                    customerModel.Fax,
                    customerModel.Closed,
                    customerModel.Suspend,
                    customerModel.IsEnablePT,
                    (decimal)customerModel.MaxWin,
                    (decimal)customerModel.MaxLose,
                    true,
                    (decimal)customerModel.MaxTransferCredit,
                    customerModel.SIBCCustID,
                    custId,
                    customerModel.AIBCCustID,
                    customerModel.PTByGameType,
                    customerModel.RoleId.ToString(),
                    DateTime.Now,
                    ip,
                    modifiedId,
                    modifiedBy,
                    fromSystem,
                    customerModel.IsAutoReset,
                    false,
                    false);
            }
            catch (Exception)
            {
                // Bypass this exception because this customer had setting but update status failed, before.
            }

            // Update Status for direct downline only
            if ((settingsInserted && !customerModel.CasinoSync) || customerModel.CasinoSync)
            {
                statusUpdated = this.UpdateCSStatus(
                    custId,
                    true,
                    customerModel.UplineDisabledCasino ? true : customerModel.CasinoDisabled,
                    customerModel.EnableDownline,
                    uplineId,
                    string.Empty,
                    0,
                    "en",
                    customerModel.UserName,
                    string.Empty,
                    out errMsg);
            }

            // Enable PT/Update max transfer for direct downline only
            if (statusUpdated)
            {
                enablePTUpdated = cust.UpdateCustEnablePT(custId, true);

                maxTransferUpdated = cust.UpdateMaxTransfer(
                    custId,
                    true,
                    (decimal)customerModel.MaxTransferCredit,
                    customerModel.IsAutoReset,
                    ip,
                    uplineId,
                    modifiedBy,
                    fromSystem);
            }

            // Update PT/Betting limit settings
            casinoPTResult = this.UpdateMasterSettings(
                custId,
                customerModel.RoleId,
                uplineId,
                customerModel.UplineRoleId,
                ip,
                modifiedId,
                modifiedBy,
                fromSystem,
                settingModel);

            // Gets error messages
            messages += string.IsNullOrWhiteSpace(errMsg) && casinoPTResult.IsSuccess ? string.Empty : !string.IsNullOrWhiteSpace(errMsg) ? errMsg + "<br />" + casinoPTResult.ErrorMessage : casinoPTResult.ErrorMessage;

            var msgs = messages.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var retMsg = string.Empty;

            foreach (var item in msgs)
            {
                retMsg += string.Format("{0}{1}", item, ", ");
            }

            return new Result(retMsg == string.Empty || retMsg == "<br />" ? 0 : 1, retMsg);
        }

        public Result UpdateAgent(int custId, string custName, int uplineId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel settingModel, CasinoCustomer customerModel)
        {
            var errMsg = string.Empty;
            var messages = string.Empty;
            var settingsInserted = true;
            var statusUpdated = true;
            var maxTransferUpdated = false;
            var casinoPTResult = new Result();
            var enablePTUpdated = false;
            var cust = new Customer();

            if (customerModel.UplineRoleId != 3 || customerModel.RoleId != 2)
            {
                return new Result(1, "Invalid custId and uplineId");
            }

            try
            {
                settingsInserted = cust.InsertCustSetting(
                    "01",
                    custId,
                    custName,
                    customerModel.UserName2,
                    customerModel.FirstName,
                    customerModel.LastName,
                    customerModel.UserPwd,
                    customerModel.Phone,
                    customerModel.MobilePhone,
                    customerModel.Currency,
                    customerModel.Fax,
                    customerModel.Closed,
                    customerModel.Suspend,
                    customerModel.IsEnablePT,
                    (decimal)customerModel.MaxWin,
                    (decimal)customerModel.MaxLose,
                    true,
                    (decimal)customerModel.MaxTransferCredit,
                    customerModel.SIBCCustID,
                    customerModel.MIBCCustID,
                    custId,
                    customerModel.PTByGameType,
                    customerModel.RoleId.ToString(),
                    DateTime.Now,
                    ip,
                    modifiedId,
                    modifiedBy,
                    fromSystem,
                    customerModel.IsAutoReset,
                    false,
                    false);
            }
            catch (Exception)
            {
                // Bypass this exception because this customer had setting but update status failed, before.
            }

            // Update Status for direct downline only
            if ((settingsInserted && !customerModel.CasinoSync) || customerModel.CasinoSync)
            {
                statusUpdated = this.UpdateCSStatus(
                    custId,
                    true,
                    customerModel.UplineDisabledCasino ? true : customerModel.CasinoDisabled,
                    customerModel.EnableDownline,
                    uplineId,
                    string.Empty,
                    0,
                    "en",
                    customerModel.UserName,
                    string.Empty,
                    out errMsg);
            }

            // Enable PT/Update max transfer for direct downline only
            if (statusUpdated)
            {
                enablePTUpdated = cust.UpdateCustEnablePT(custId, true);

                maxTransferUpdated = cust.UpdateMaxTransfer(
                    custId,
                    true,
                    (decimal)customerModel.MaxTransferCredit,
                    customerModel.IsAutoReset,
                    ip,
                    uplineId,
                    modifiedBy,
                    fromSystem);
            }

            // Update PT/Betting limit settings
            casinoPTResult = this.UpdateAgentSettings(
                custId,
                customerModel.RoleId,
                uplineId,
                customerModel.UplineRoleId,
                ip,
                modifiedId,
                modifiedBy,
                fromSystem,
                settingModel);

            // Gets error messages
            messages += string.IsNullOrWhiteSpace(errMsg) && casinoPTResult.IsSuccess ? string.Empty : !string.IsNullOrWhiteSpace(errMsg) ? errMsg + "<br />" + casinoPTResult.ErrorMessage : casinoPTResult.ErrorMessage;

            var msgs = messages.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var retMsg = string.Empty;

            foreach (var item in msgs)
            {
                retMsg += string.Format("{0}{1}", item, ", ");
            }

            return new Result(retMsg == string.Empty || retMsg == "<br />" ? 0 : 1, retMsg);
        }

        public Result UpdateMember(int custId, string custName, int uplineId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel settingModel, CasinoCustomer customerModel)
        {
            var errMsg = string.Empty;
            var messages = string.Empty;
            var settingsInserted = true;
            var statusUpdated = true;
            var maxTransferUpdated = false;
            var casinoPTResult = new Result();
            var enablePTUpdated = false;
            var cust = new Customer();

            if (customerModel.UplineRoleId != 2 || customerModel.RoleId != 1)
            {
                return new Result(1, "Invalid custId and uplineId");
            }

            try
            {
                settingsInserted = cust.InsertCustSetting(
                    "01",
                    custId,
                    custName,
                    customerModel.UserName2,
                    customerModel.FirstName,
                    customerModel.LastName,
                    customerModel.UserPwd,
                    customerModel.Phone,
                    customerModel.MobilePhone,
                    customerModel.Currency,
                    customerModel.Fax,
                    customerModel.Closed,
                    customerModel.Suspend,
                    customerModel.IsEnablePT,
                    (decimal)customerModel.MaxWin,
                    (decimal)customerModel.MaxLose,
                    true,
                    (decimal)customerModel.MaxTransferCredit,
                    customerModel.SIBCCustID,
                    customerModel.MIBCCustID,
                    customerModel.AIBCCustID,
                    customerModel.PTByGameType,
                    customerModel.RoleId.ToString(),
                    DateTime.Now,
                    ip,
                    modifiedId,
                    modifiedBy,
                    fromSystem,
                    customerModel.IsAutoReset,
                    false,
                    false);
            }
            catch (Exception)
            {
                // Bypass this exception because this customer had setting but update status failed, before.
            }

            // Update Status for direct downline only
            if ((settingsInserted && !customerModel.CasinoSync) || customerModel.CasinoSync)
            {
                statusUpdated = this.UpdateCSStatus(
                    custId,
                    true,
                    customerModel.UplineDisabledCasino ? true : customerModel.CasinoDisabled,
                    customerModel.EnableDownline,
                    uplineId,
                    string.Empty,
                    0,
                    "en",
                    customerModel.UserName,
                    string.Empty,
                    out errMsg);
            }

            // Enable PT/Update max transfer for direct downline only
            if (statusUpdated)
            {
                enablePTUpdated = cust.UpdateCustEnablePT(custId, true);

                maxTransferUpdated = cust.UpdateMaxTransfer(
                    custId,
                    true,
                    (decimal)customerModel.MaxTransferCredit,
                    customerModel.IsAutoReset,
                    ip,
                    uplineId,
                    modifiedBy,
                    fromSystem);
            }

            // Update PT/Betting limit settings
            casinoPTResult = this.UpdateMemberSettings(
                custId,
                customerModel.RoleId,
                uplineId,
                customerModel.UplineRoleId,
                ip,
                modifiedId,
                modifiedBy,
                fromSystem,
                settingModel);

            // Gets error messages
            messages += string.IsNullOrWhiteSpace(errMsg) && casinoPTResult.IsSuccess ? string.Empty : !string.IsNullOrWhiteSpace(errMsg) ? errMsg + "<br />" + casinoPTResult.ErrorMessage : casinoPTResult.ErrorMessage;

            var msgs = messages.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var retMsg = string.Empty;

            foreach (var item in msgs)
            {
                retMsg += string.Format("{0}{1}", item, ", ");
            }

            return new Result(retMsg == string.Empty || retMsg == "<br />" ? 0 : 1, retMsg);
        }

        public Result UpdateMemberBySuper(int custId, int uplineId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel settingModel, CasinoCustomer customerModel)
        {
            var errMsg = string.Empty;
            var messages = string.Empty;
            var casinoPTResult = new Result();
            var cust = new Customer();

            if (customerModel.UplineRoleId != Super || customerModel.RoleId != Member)
            {
                return new Result(1, "Invalid custId and uplineId");
            }

            // Update PT/Betting limit settings
            casinoPTResult = this.UpdateMemberSettingsBySuper(
                custId,
                customerModel.RoleId,
                uplineId,
                customerModel.UplineRoleId,
                ip,
                modifiedId,
                modifiedBy,
                fromSystem,
                settingModel);

            // Gets error messages
            messages += string.IsNullOrWhiteSpace(errMsg) && casinoPTResult.IsSuccess ? string.Empty : !string.IsNullOrWhiteSpace(errMsg) ? errMsg + "<br />" + casinoPTResult.ErrorMessage : casinoPTResult.ErrorMessage;

            var msgs = messages.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var retMsg = string.Empty;

            foreach (var item in msgs)
            {
                retMsg += string.Format("{0}{1}", item, ", ");
            }

            return new Result(retMsg == string.Empty || retMsg == "<br />" ? 0 : 1, retMsg);
        }

        public Result UpdateMemberByMaster(int custId, int uplineId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel settingModel, CasinoCustomer customerModel)
        {
            var errMsg = string.Empty;
            var messages = string.Empty;
            var casinoPTResult = new Result();
            var cust = new Customer();

            if (customerModel.UplineRoleId != Master || customerModel.RoleId != Member)
            {
                return new Result(1, "Invalid custId and uplineId");
            }

            // Update PT/Betting limit settings
            casinoPTResult = this.UpdateMemberSettingsByMaster(
                custId,
                customerModel.RoleId,
                uplineId,
                customerModel.UplineRoleId,
                ip,
                modifiedId,
                modifiedBy,
                fromSystem,
                settingModel);

            // Gets error messages
            messages += string.IsNullOrWhiteSpace(errMsg) && casinoPTResult.IsSuccess ? string.Empty : !string.IsNullOrWhiteSpace(errMsg) ? errMsg + "<br />" + casinoPTResult.ErrorMessage : casinoPTResult.ErrorMessage;

            var msgs = messages.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var retMsg = string.Empty;

            foreach (var item in msgs)
            {
                retMsg += string.Format("{0}{1}", item, ", ");
            }

            return new Result(retMsg == string.Empty || retMsg == "<br />" ? 0 : 1, retMsg);
        }

        public Result UpdateSuperBettingLimit(int custId, int roleId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel model, CasinoCustomer customer)
        {
            var msg = string.Empty;
            var gameStakeTable = this.GameTypesStakesDataTable();

            foreach (var gameGroup in model.GameGroups)
            {
                var gameGroupID = gameGroup.GameGroupID;

                foreach (var gameType in gameGroup.GameTypes)
                {
                    gameStakeTable.Rows.Add(
                        new object[]
                            {
                                custId,
                                gameType.GameTypeID,
                                this.SelectedStakeLevels(gameType.BettingLimits),
                                roleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });
                }
            }

            var resultStake = new DataTable();
            var cust = new Customer();

            resultStake = cust.UpdateCustGameTypeStakeLevel(gameStakeTable);
            var success = cust.UpdateMaxTransfer(custId, customer.IsMaxWin, Utils.ToDecimal(customer.MaxTransferCredit), customer.IsAutoReset, ip, modifiedId, modifiedBy, fromSystem);

            // Process error messages
            foreach (DataRow item in resultStake.Rows)
            {
                msg += item.Field<string>("ErrMsg") + "<br />";
            }

            msg = msg.EndsWith("<br />") ? msg.Substring(0, msg.LastIndexOf("<br />")) : msg;

            return new Result((msg == string.Empty || resultStake.Rows.Count == 0) && success ? 0 : 1, msg);
        }

        private static string Lang(string original)
        {
            return original + "_En";
        }

        private bool UpdateCSStatus(int custid, bool syncCS, bool disabledCS, bool isUpdDownline, int userId, string ip, int subAccId, string lang, string username, string subUsername, out string errMsg)
        {
            var cust = new Customer();
            var status = this.DoUpdateCSStatusWithoutSync(custid, syncCS, disabledCS, isUpdDownline, userId, ip, subAccId, lang, out errMsg);

            if (!status)
            {
                return false;
            }

            return cust.UpdateCustEnableCasino(custid, !disabledCS, isUpdDownline, ip, custid, subAccId == 0 ? username : subUsername, 2);
        }

        private bool DoUpdateCSStatusWithoutSync(int custid, bool syncCS, bool disabledCS, bool isUpdDownline, int userId, string ip, int subAccId, string lang, out string errMsg)
        {
            return this._casino.DoUpdateCSStatusWithoutSync(custid, syncCS, disabledCS, isUpdDownline, userId, ip, subAccId, lang, out errMsg);
        }

        private Result UpdateMasterSettings(int custId, int roleId, int uplineId, int uplineRoleId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel model)
        {
            var msg = string.Empty;
            var gameKindTable = this.GameKindsDataTable();
            var gameTypeTable = this.GameTypesDataTable();
            var gameStakeTable = this.GameTypesStakesDataTable();

            foreach (var gameGroup in model.GameGroups)
            {
                var gameGroupID = gameGroup.GameGroupID;

                gameKindTable.Rows.Add(
                    new object[]
                            {
                                custId,
                                gameGroupID,
                                null,
                                !(roleId == Super || roleId == Master) || gameGroup.Is3 == true ? null : (object)gameGroup.S3,
                                !(roleId == Master || roleId == Agent) ? null : (object)gameGroup.M3,
                                !(roleId == Agent || roleId == Member) ? null : (object)gameGroup.N3,
                                gameGroup.Is3 == true ? 1 : 0,
                                !gameGroup.Is3 == true ? null : (object)gameGroup.Us3,
                                !(roleId == Super || roleId == Master || roleId == Agent) ? null : (object)gameGroup.N3,
                                0,
                                roleId != Super ? null : (object)gameGroup.Sco,
                                roleId != Master ? null : (object)gameGroup.Mco,
                                roleId != Agent ? null : (object)gameGroup.Aco,
                                roleId != Member ? null : (object)gameGroup.co,
                                roleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });

                var gameTypes = gameGroup.GameTypes;

                foreach (var gameType in gameTypes)
                {
                    gameTypeTable.Rows.Add(
                        new object[]
                            {
                                custId,
                                gameType.GameTypeID,
                                null,
                                !(roleId == Super || roleId == Master) || gameType.Is3 == true ? null : (object)gameType.S3,
                                !(roleId == Master || roleId == Agent) ? null : (object)gameType.M3,
                                !(roleId == Agent || roleId == Member) ? null : (object)gameType.N3,
                                gameType.Is3 == true ? 1 : 0,
                                !gameType.Is3 == true ? null : (object)gameType.Us3,
                                !(roleId == Super || roleId == Master || roleId == Member) ? null : (object)gameType.N3,
                                0,
                                roleId != Super ? null : (object)gameType.Sco,
                                roleId != Master ? null : (object)gameType.Mco,
                                roleId != Agent ? null : (object)gameType.Aco,
                                roleId != Member ? null : (object)gameType.co,
                                this.SelectedStakeLevels(gameType.BettingLimits),
                                roleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });

                    gameStakeTable.Rows.Add(
                        new object[]
                            {
                                custId,
                                gameType.GameTypeID,
                                this.SelectedStakeLevels(gameType.BettingLimits),
                                roleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });
                }
            }

            var resultKind = new DataTable();
            var resultType = new DataTable();
            var resultStake = new DataTable();
            var cust = new Customer();

            resultKind = cust.UpdateCustGameKind(gameKindTable);

            if (model.IsPtByGameType)
            {
                resultType = cust.UpdateCustGameType(gameTypeTable);
            }
            else
            {
                resultStake = cust.UpdateCustGameTypeStakeLevel(gameStakeTable);
            }

            // Process error messages
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

        private Result UpdateAgentSettings(int custId, int roleId, int uplineId, int uplineRoleId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel model)
        {
            var msg = string.Empty;
            var gameKindTable = this.GameKindsDataTable();
            var gameTypeTable = this.GameTypesDataTable();
            var gameStakeTable = this.GameTypesStakesDataTable();
            var cust = new Customer();

            foreach (var gameGroup in model.GameGroups)
            {
                var gameGroupID = gameGroup.GameGroupID;

                gameKindTable.Rows.Add(
                    new object[]
                            {
                                custId,
                                gameGroupID,
                                null,
                                !(roleId == Super || roleId == Master) || gameGroup.Is1 == true ? null : (object)gameGroup.M3,
                                !(roleId == Master || roleId == Agent) ? null : (object)gameGroup.M2,
                                !(roleId == Agent || roleId == Member) ? null : (object)gameGroup.A2,
                                gameGroup.Is1 == true ? 1 : 0,
                                !gameGroup.Is1 == true ? null : (object)gameGroup.Us1,
                                !(roleId == Super || roleId == Master || roleId == Agent) ? null : (object)gameGroup.N2,
                                0,
                                roleId != Super ? null : (object)gameGroup.Sco,
                                roleId != Master ? null : (object)gameGroup.Mco,
                                roleId != Agent ? null : (object)gameGroup.Aco,
                                roleId != Member ? null : (object)gameGroup.co,
                                roleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });

                var gameTypes = gameGroup.GameTypes;

                foreach (var gameType in gameTypes)
                {
                    gameTypeTable.Rows.Add(
                        new object[]
                            {
                                custId,
                                gameType.GameTypeID,
                                null,
                                !(roleId == Super || roleId == Master) || gameType.Is1 == true ? null : (object)gameType.M3,
                                !(roleId == Master || roleId == Agent) ? null : (object)gameType.M2,
                                !(roleId == Agent || roleId == Member) ? null : (object)gameType.A2,
                                gameType.Is1 == true ? 1 : 0,
                                !gameType.Is1 == true ? null : (object)gameType.Us1,
                                !(roleId == Super || roleId == Master || roleId == Member) ? null : (object)gameType.N2,
                                0,
                                roleId != Super ? null : (object)gameType.Sco,
                                roleId != Master ? null : (object)gameType.Mco,
                                roleId != Agent ? null : (object)gameType.Aco,
                                roleId != Member ? null : (object)gameType.co,
                                this.SelectedStakeLevels(gameType.BettingLimits),
                                roleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });

                    gameStakeTable.Rows.Add(
                        new object[]
                            {
                                custId,
                                gameType.GameTypeID,
                                this.SelectedStakeLevels(gameType.BettingLimits),
                                roleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });
                }
            }

            var resultKind = new DataTable();
            var resultType = new DataTable();
            var resultStake = new DataTable();

            resultKind = cust.UpdateCustGameKind(gameKindTable);

            if (model.IsPtByGameType)
            {
                resultType = cust.UpdateCustGameType(gameTypeTable);
            }
            else
            {
                resultStake = cust.UpdateCustGameTypeStakeLevel(gameStakeTable);
            }

            // Process error messages
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

        private Result UpdateMemberSettings(int custId, int roleId, int uplineId, int uplineRoleId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel model)
        {
            var msg = string.Empty;
            var gameKindTable = this.GameKindsDataTable();
            var gameTypeTable = this.GameTypesDataTable();
            var gameStakeTable = this.GameTypesStakesDataTable();
            var cust = new Customer();

            foreach (var gameGroup in model.GameGroups)
            {
                var gameGroupID = gameGroup.GameGroupID;

                gameKindTable.Rows.Add(
                    new object[]
                            {
                                custId,
                                gameGroupID,
                                null,
                                null,
                                !(roleId == Master || roleId == Agent) ? null : (object)gameGroup.M2,
                                !(roleId == Agent || roleId == Member) ? null : (object)gameGroup.M1,
                                0,
                                null,
                                null,
                                0,
                                roleId != Super ? null : (object)gameGroup.Sco,
                                roleId != Master ? null : (object)gameGroup.Mco,
                                roleId != Agent ? null : (object)gameGroup.Aco,
                                roleId != Member ? null : (object)gameGroup.co,
                                roleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });

                var gameTypes = gameGroup.GameTypes;

                foreach (var gameType in gameTypes)
                {
                    gameTypeTable.Rows.Add(
                        new object[]
                            {
                                custId,
                                gameType.GameTypeID,
                                null,
                                null,
                                !(roleId == Master || roleId == Agent) ? null : (object)gameType.M2,
                                !(roleId == Agent || roleId == Member) ? null : (object)gameType.M1,
                                0,
                                null,
                                null,
                                0,
                                roleId != Super ? null : (object)gameType.Sco,
                                roleId != Master ? null : (object)gameType.Mco,
                                roleId != Agent ? null : (object)gameType.Aco,
                                roleId != Member ? null : (object)gameType.co,
                                this.SelectedStakeLevels(gameType.BettingLimits),
                                roleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });

                    gameStakeTable.Rows.Add(
                        new object[]
                            {
                                custId,
                                gameType.GameTypeID,
                                this.SelectedStakeLevels(gameType.BettingLimits),
                                roleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });
                }
            }

            var resultKind = new DataTable();
            var resultType = new DataTable();
            var resultStake = new DataTable();

            resultKind = cust.UpdateCustGameKind(gameKindTable);

            if (model.IsPtByGameType)
            {
                resultType = cust.UpdateCustGameType(gameTypeTable);
            }
            else
            {
                resultStake = cust.UpdateCustGameTypeStakeLevel(gameStakeTable);
            }

            // Process error messages
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

        private Result UpdateMemberSettingsBySuper(int custId, int roleId, int uplineId, int uplineRoleId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel model)
        {
            var msg = string.Empty;
            var gameKindTable = this.GameKindsDataTableMember();
            var gameTypeTable = this.GameTypesDataTableMember();
            var gameStakeTable = this.GameTypesStakesDataTable();
            var cust = new Customer();

            foreach (var gameGroup in model.GameGroups)
            {
                var gameGroupID = gameGroup.GameGroupID;

                gameKindTable.Rows.Add(
                        new object[]
                            {
                                custId,
                                gameGroupID,
                                !(uplineRoleId == Super || uplineRoleId == Master) || gameGroup.Is1 == true || gameGroup.Is3 == true ? null : (object)gameGroup.S3,
                                !(uplineRoleId == Master || uplineRoleId == Agent) ? null : (object)gameGroup.M3,
                                gameGroup.Is1 == true ? 1 : 0,
                                !gameGroup.Is1 == true ? null : (object)gameGroup.Us3,
                                uplineRoleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });

                var gameTypes = gameGroup.GameTypes;

                foreach (var gameType in gameTypes)
                {
                    gameTypeTable.Rows.Add(
                        new object[]
                            {
                                custId,
                                gameType.GameTypeID,
                                !(uplineRoleId == Super || uplineRoleId == Master) || gameType.Is1 == true || gameType.Is3 == true ? null : (object)gameType.S3,
                                !(uplineRoleId == Master || uplineRoleId == Agent) ? null : (object)gameType.M3,
                                gameType.Is1 == true ? 1 : 0,
                                !gameType.Is1 == true ? null : (object)gameType.Us3,
                                uplineRoleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });

                    gameStakeTable.Rows.Add(
                        new object[]
                            {
                                custId,
                                gameType.GameTypeID,
                                this.SelectedStakeLevels(gameType.BettingLimits),
                                roleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });
                }
            }

            var resultKind = new DataTable();
            var resultType = new DataTable();
            var resultStake = new DataTable();

            resultKind = cust.UpdateCustGameKind_EditMember(gameKindTable);

            if (model.IsPtByGameType)
            {
                resultType = cust.UpdateCustGameType_EditMember(gameTypeTable);
            }

            resultStake = cust.UpdateCustGameTypeStakeLevel(gameStakeTable);

            // Process error messages
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

        private Result UpdateMemberSettingsByMaster(int custId, int roleId, int uplineId, int uplineRoleId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel model)
        {
            var msg = string.Empty;
            var gameKindTable = this.GameKindsDataTableMember();
            var gameTypeTable = this.GameTypesDataTableMember();
            var gameStakeTable = this.GameTypesStakesDataTable();
            var cust = new Customer();

            foreach (var gameGroup in model.GameGroups)
            {
                var gameGroupID = gameGroup.GameGroupID;

                gameKindTable.Rows.Add(
                        new object[]
                            {
                                custId,
                                gameGroupID,
                                null,
                                !(uplineRoleId == Master || uplineRoleId == Agent) ? null : (object)gameGroup.M3,
                                0,
                                null,
                                uplineRoleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });

                var gameTypes = gameGroup.GameTypes;

                foreach (var gameType in gameTypes)
                {
                    gameTypeTable.Rows.Add(
                        new object[]
                            {
                                custId,
                                gameType.GameTypeID,
                                null,
                                !(uplineRoleId == Master || uplineRoleId == Agent) ? null : (object)gameType.M3,
                                0,
                                null,
                                uplineRoleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });

                    gameStakeTable.Rows.Add(
                        new object[]
                            {
                                custId,
                                gameType.GameTypeID,
                                this.SelectedStakeLevels(gameType.BettingLimits),
                                roleId,
                                ip,
                                modifiedId,
                                modifiedBy,
                                fromSystem
                            });
                }
            }

            var resultKind = new DataTable();
            var resultType = new DataTable();
            var resultStake = new DataTable();

            resultKind = cust.UpdateCustGameKind_EditMember(gameKindTable);

            if (model.IsPtByGameType)
            {
                resultType = cust.UpdateCustGameType_EditMember(gameTypeTable);
            }

            resultStake = cust.UpdateCustGameTypeStakeLevel(gameStakeTable);

            // Process error messages
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

        private string SelectedStakeLevels(List<StakeLevel> stakes)
        {
            var selectedLevels = string.Empty;

            stakes.ForEach(item =>
            {
                selectedLevels += item.IsChecked ? item.StakeLevelID + "," : string.Empty;
            });

            return selectedLevels.EndsWith(",") ? selectedLevels.Substring(0, selectedLevels.Length - 1) : selectedLevels;
        }

        private DataTable GameKindsDataTableMember()
        {
            var dt = new DataTable();
            dt.Columns.AddRange(
                new DataColumn[]
                {
                    new DataColumn("IBCCustID"),
                    new DataColumn("GameKind"),
                    new DataColumn("SPositionTaking"),
                    new DataColumn("MPositionTaking"),
                    new DataColumn("IsAutoPositionTaking"),
                    new DataColumn("AutoPositionTaking"),
                    new DataColumn("RoleId"),
                    new DataColumn("FromIP"),
                    new DataColumn("ModifyBy"),
                    new DataColumn("ModifyUserName"),
                    new DataColumn("FromSystem")
                });

            return dt;
        }

        private DataTable GameTypesDataTableMember()
        {
            var dt = new DataTable();
            dt.Columns.AddRange(
                new DataColumn[]
                {
                    new DataColumn("IBCCustID"),
                    new DataColumn("GameTypeID"),
                    new DataColumn("SPositionTaking"),
                    new DataColumn("MPositionTaking"),
                    new DataColumn("IsAutoPositionTaking"),
                    new DataColumn("AutoPositionTaking"),
                    new DataColumn("RoleId"),
                    new DataColumn("FromIP"),
                    new DataColumn("ModifyBy"),
                    new DataColumn("ModifyUserName"),
                    new DataColumn("FromSystem")
                });

            return dt;
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

        private List<GameType> GetGameTypeSuper(int custId, int gameKindID, bool synced, ref GameGroup gameGroup, int exchangeId = 0, bool isPTByGameType = false)
        {
            var cust = new Customer();
            var gameTypes = new List<GameType>();
            var dt = synced ? cust.GetCustGameType(custId, gameKindID) : new BasicInfo().GetGameType(gameKindID);
            List<decimal?> s4s = new List<decimal?>(), n4s = new List<decimal?>(), scos = new List<decimal?>();

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                gameType.Sco = MaxComm;
                gameType.Mco = !synced ? 0 : Utils.ToDecimal(row["SMAgentRollingRate"]).Format(4);

                // Game type Info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Super preset PT
                gameType.S4 = MaxPT;
                gameType.S3 = !synced ? 0 : Utils.ToDecimal(row["MaxPositionTaking"]).Format();

                // Master Min PT
                gameType.N4 = gameType.S3;
                gameType.N3 = !synced ? 0 : Utils.ToDecimal(row["MinPositionTaking"]).Format();

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID, exchangeId);
                gameType.BettingLimits = stakes;

                s4s.Add(gameType.S4);
                n4s.Add(gameType.N4);
                scos.Add(gameType.Sco);

                gameTypes.Add(gameType);
            }

            if (isPTByGameType)
            {
                gameGroup.S4 = s4s.Max();
                gameGroup.N4 = n4s.Max();
                gameGroup.Sco = scos.Max();
            }

            return gameTypes;
        }

        private List<GameType> GetGameTypeMaster(int custId, int gameKindID, bool synced, ref GameGroup gameGroup, bool isPTByGameType = false)
        {
            var cust = new Customer();
            var gameTypes = new List<GameType>();
            var dt = cust.GetCustGameType(custId, gameKindID);
            List<decimal?> scos = new List<decimal?>(), s4s = new List<decimal?>(),
                us3s = new List<decimal?>(), m3s = new List<decimal?>(), n3s = new List<decimal?>();

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                gameType.Mco = Utils.ToDecimal(row["MAgentRollingRate"]).Format(4); // synced ? Utils.ToDecimal(row["UplineMAgentRollingRate"]).Format(4) : Utils.ToDecimal(row["MAgentRollingRate"]).Format(4);
                gameType.Sco = synced ? Utils.ToDecimal(row["UplineSMAgentRollingRate"]).Format(4) : Utils.ToDecimal(row["SMAgentRollingRate"]).Format(4);

                if (!synced)
                {
                    gameType.Mco = gameType.Sco;
                }

                // Game type Info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Super preset PT
                gameType.S4 = synced ? Utils.ToDecimal(row["UplineMaxPositionTaking"]).Format(2) : Utils.ToDecimal(row["MaxPositionTaking"]).Format(2);
                gameType.S3 = Utils.ToDecimal(row["SPositionTaking"]).Format(2); // synced ? Utils.ToDecimal(row["UplineSPositionTaking"]).Format(2) : Utils.ToDecimal(row["SPositionTaking"]).Format(2);

                // Super Auto PT
                gameType.Is3 = Utils.ToBoolean(row["IsAutoPositionTaking"]);
                gameType.Us3 = Utils.ToDecimal(row["AutoPositionTaking"]).Format(2);

                // Master Max PT
                gameType.M3 = synced ? Utils.ToDecimal(row["MPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineMPositionTaking"]).Format(2);

                // Master Min PT
                gameType.N3 = !synced ? 0 : Utils.ToDecimal(row["MinPositionTaking"]).Format(2);

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID, 0, synced);
                gameType.BettingLimits = stakes;

                scos.Add(gameType.Sco);
                us3s.Add(gameType.Us3);
                m3s.Add(gameType.M3);
                n3s.Add(gameType.N3);
                s4s.Add(gameType.S4);

                gameTypes.Add(gameType);
            }

            if (isPTByGameType)
            {
                gameGroup.Sco = scos.Max();
                gameGroup.Us3 = us3s.Max();
                gameGroup.M3 = m3s.Max();
                gameGroup.N3 = n3s.Max();
                gameGroup.S4 = s4s.Max();
            }

            return gameTypes;
        }

        private List<GameType> GetGameTypeAgent(int custId, int gameKindID, bool synced, ref GameGroup gameGroup, bool isPTByGameType = false)
        {
            var cust = new Customer();
            var gameTypes = new List<GameType>();
            var dt = cust.GetCustGameType(custId, gameKindID);
            List<decimal?> mcos = new List<decimal?>(), m3s = new List<decimal?>(), a2s = new List<decimal?>(), n2s = new List<decimal?>();

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                // Game Group Info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Commission
                gameType.Aco = synced ? Utils.ToDecimal(row["AgentRollingRate"]).Format(4) : Utils.ToDecimal(row["UplineAgentRollingRate"]).Format(4);
                gameType.Mco = synced ? Utils.ToDecimal(row["UplineMAgentRollingRate"]).Format(4) : Utils.ToDecimal(row["MAgentRollingRate"]).Format(4);

                if (!synced)
                {
                    gameType.Aco = gameType.Mco;
                }

                // Master preset PT
                gameType.M3 = !synced ? Utils.ToDecimal(row["MPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineMPositionTaking"]).Format(2);
                gameType.M2 = Utils.ToDecimal(row["MPositionTaking"]).Format(2); // !synced ? Utils.ToDecimal(row["MPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineMPositionTaking"]).Format(2);

                // Agent Max PT
                gameType.A2 = synced ? Utils.ToDecimal(row["APositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineAPositionTaking"]).Format(2);

                // Agent Min PT
                gameType.N2 = !synced ? 0 : Utils.ToDecimal(row["MinPositionTaking"]).Format(2);

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID, 0, synced);
                gameType.BettingLimits = stakes;

                mcos.Add(gameType.Mco);
                m3s.Add(gameType.M3);
                a2s.Add(gameType.A2);
                n2s.Add(gameType.N2);

                gameTypes.Add(gameType);
            }

            if (isPTByGameType)
            {
                gameGroup.Mco = mcos.Max();
                gameGroup.M3 = m3s.Max();
                gameGroup.A2 = a2s.Max();
                gameGroup.N2 = n2s.Max();
            }

            return gameTypes;
        }

        private List<GameType> GetGameTypeAgentBySuper(int custId, int gameKindID, bool synced)
        {
            var cust = new Customer();
            var gameTypes = new List<GameType>();
            var dt = cust.GetCustGameType(custId, gameKindID);

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                // Game type info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Master preset PT
                gameType.M3 = synced ? Utils.ToDecimal(row["MPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineMPositionTaking"]).Format(2);
                gameType.M2 = synced ? Utils.ToDecimal(row["APositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineAPositionTaking"]).Format(2);

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID);
                gameType.BettingLimits = stakes;

                gameTypes.Add(gameType);
            }

            return gameTypes;
        }

        private List<GameType> GetGameTypeMember(int custId, int gameKindID, bool synced, ref GameGroup gameGroup, bool isPTByGameType = false)
        {
            var cust = new Customer();
            var gameTypes = new List<GameType>();
            var dt = cust.GetCustGameType(custId, gameKindID);
            List<decimal?> acos = new List<decimal?>(), m2s = new List<decimal?>(), n2s = new List<decimal?>();

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                // Commission
                gameType.co = synced ? Utils.ToDecimal(row["MemberRollingRate"]).Format(4) : Utils.ToDecimal(row["UplineMemberRollingRate"]).Format(4);
                gameType.Aco = synced ? Utils.ToDecimal(row["UplineAgentRollingRate"]).Format(4) : Utils.ToDecimal(row["AgentRollingRate"]).Format(4);

                if (!synced)
                {
                    gameType.co = gameType.Aco;
                }

                // Game type info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Agent preset PT
                gameType.M2 = synced ? Utils.ToDecimal(row["UplineMaxPositionTaking"]).Format(2) : Utils.ToDecimal(row["MaxPositionTaking"]).Format(2);
                gameType.M1 = Utils.ToDecimal(row["APositionTaking"]).Format(2); // !synced ? Utils.ToDecimal(row["APositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineAPositionTaking"]).Format(2);
                gameType.N2 = synced ? Utils.ToDecimal(row["UplineMinPositionTaking"]).Format(2) : Utils.ToDecimal(row["MinPositionTaking"]).Format(2);

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID, 0, synced);
                gameType.BettingLimits = stakes;

                acos.Add(gameType.Aco);
                m2s.Add(gameType.M2);
                n2s.Add(gameType.N2);

                gameTypes.Add(gameType);
            }

            if (isPTByGameType)
            {
                gameGroup.Aco = acos.Max();
                gameGroup.M2 = m2s.Max();
                gameGroup.N2 = n2s.Max();
            }

            return gameTypes;
        }

        private List<GameType> GetGameTypeMemberBySuper(int custId, int gameKindID, ref GameGroup gameGroup, bool isPTByGameType = false)
        {
            var cust = new Customer();
            var gameTypes = new List<GameType>();
            var dt = cust.GetCustGameType_EditMember(custId, gameKindID);
            var dtsuper = cust.GetCustGameType(custId, gameKindID);
            List<decimal?> s4s = new List<decimal?>(), us3s = new List<decimal?>(), m3s = new List<decimal?>(), n3s = new List<decimal?>();

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();
                var isAutoOnMember = false;

                foreach (DataRow dtrow in dtsuper.Rows)
                {
                    if (row["GameTypeID"].Equals(dtrow["GameTypeID"]))
                    {
                        isAutoOnMember = Utils.ToBoolean(dtrow["SuperIsAutoPositionTaking"]);
                        break;
                    }
                }

                // Game type info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Super preset PT
                gameType.S4 = Utils.ToDecimal(row["SuperMaxPositionTaking"]).Format(2);
                gameType.S3 = Utils.ToDecimal(row["SPositionTaking"]).Format(2);
                gameType.N4 = Utils.ToDecimal(row["SuperMinPositionTaking"]).Format(2);

                // Super Auto PT
                gameType.Is1 = Utils.ToBoolean(row["IsAutoPositionTaking"]);
                gameType.Is3 = isAutoOnMember;
                gameType.Us3 = Utils.ToDecimal(row["AutoPositionTaking"]).Format(2);

                // Master preset PT
                gameType.M3 = Utils.ToDecimal(row["MPositionTaking"]).Format(2);

                // Agent preset PT
                gameType.M2 = Utils.ToDecimal(row["APositionTaking"]).Format(2);
                gameType.N3 = gameType.Is3 == true ? gameType.N4 - gameType.M3 - gameType.M2 : Math.Max((decimal)(gameType.N4 - gameType.M3 - gameType.M2), 0m);

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID);
                gameType.BettingLimits = stakes;

                s4s.Add(gameType.S4);
                us3s.Add(gameType.Us3);
                m3s.Add(gameType.M3);
                n3s.Add(gameType.N3);

                gameTypes.Add(gameType);
            }

            if (isPTByGameType)
            {
                gameGroup.S4 = s4s.Max();
                gameGroup.Us3 = us3s.Max();
                gameGroup.M3 = m3s.Max();
                gameGroup.N3 = n3s.Max();
            }

            return gameTypes;
        }

        private List<GameType> GetGameTypeMemberByMaster(int custId, int gameKindID, ref GameGroup gameGroup, bool isPTByGameType = false)
        {
            var cust = new Customer();
            var gameTypes = new List<GameType>();
            var dt = cust.GetCustGameType(custId, gameKindID);
            List<decimal?> s4s = new List<decimal?>(), m3s = new List<decimal?>(), n2s = new List<decimal?>();

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                // Game type info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Super preset PT
                gameType.S4 = Utils.ToDecimal(row["MasterMaxPositionTalking"]).Format(2);
                gameType.S3 = Utils.ToDecimal(row["SPositionTaking"]).Format(2);

                // Agent preset PT
                gameType.M3 = Utils.ToDecimal(row["MPositionTaking"]).Format(2);
                gameType.M2 = Utils.ToDecimal(row["APositionTaking"]).Format(2);
                var min = Utils.ToDecimal(row["MasterMinPositionTaking"]).Format(2) - Utils.ToDecimal(row["APositionTaking"]).Format(2);
                gameType.N2 = min < 0 ? 0 : min;

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID);
                gameType.BettingLimits = stakes;

                s4s.Add(gameType.S4);
                m3s.Add(gameType.M3);
                n2s.Add(gameType.N2);

                gameTypes.Add(gameType);
            }

            if (isPTByGameType)
            {
                gameGroup.S4 = s4s.Max();
                gameGroup.M3 = m3s.Max();
                gameGroup.N2 = n2s.Max();
            }

            return gameTypes;
        }

        private List<StakeLevel> GetStakeLevels(int custId, int gameTypeID, int exchangeId = 0, bool synced = true)
        {
            var cust = new Customer();
            var stakeLevels = new List<StakeLevel>();
            var dt = custId != 0 ? cust.GetCustStakeLevel(custId, gameTypeID) : new BasicInfo().GetGameTypeStakeLevel(gameTypeID, exchangeId);

            foreach (DataRow row in dt.Rows)
            {
                var stake = new StakeLevel
                {
                    StakeLevelID = Utils.ToInt32(row["StakeLevelID"]),
                    StakeLevelName = Utils.ToString(row["StakeLevelName"]),
                    IsChecked = Utils.ToBoolean(row["IsChecked"]),
                };

                if (synced || stake.IsChecked)
                {
                    stakeLevels.Add(stake);
                }
            }

            return stakeLevels;
        }
    }
}