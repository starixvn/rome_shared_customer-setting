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
    using System.Data;
    using CasinoLibrary.SBAgent;
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Repositories.Impl;
    using Fanex.CustomerSetting.Module.Casino.ViewModels;
    using Fanex.CustomerSetting.Module.Core;
    
    /// <summary>
    /// TODO: Update summary.
    /// </summary>    
    public class CasinoManager
    {
        private const int Super = 4;
        private const int Master = 3;
        private const int Agent = 2;
        private const int Member = 1;

        private static CasinoManager instance = new CasinoManager();
        private readonly CasinoDao casino = new CasinoDao();

        public static CasinoManager Instance
        {
            get
            {
                return CasinoManager.instance ?? new CasinoManager();
            }
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
            CasinoManager.Instance.GetCustSettingCS(ref casinoSettingUpline);

            if (casinoSettingUpline.RoleId - custInfo.RoleId != 1 || casinoSettingUpline.RoleId != 4 || custInfo.RoleId != 3)
            {
                throw new Exception("Invalid custId and uplineId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.PTByGameType;
            
            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();
                
                // Commission
                gameGroup.Mco = synced ? Utils.ToDecimal(row["MAgentRollingRate"]).Format(4) : Utils.ToDecimal(row["UplineMAgentRollingRate"]).Format(4);
                gameGroup.Sco = synced ? Utils.ToDecimal(row["UplineSMAgentRollingRate"]).Format(4) : Utils.ToDecimal(row["SMAgentRollingRate"]).Format(4);

                // Game Group Info
                gameGroup.GameGroupID = Utils.ToInt32(row["GameKind"]);
                gameGroup.GameGroupName = Utils.ToString(row[Lang("GameKindName")]);

                // Super preset PT
                gameGroup.S4 = synced ? Utils.ToDecimal(row["UplineMaxPositionTaking"]).Format(2) : Utils.ToDecimal(row["MaxPositionTaking"]).Format(2);
                gameGroup.S3 = synced ? Utils.ToDecimal(row["SPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineSPositionTaking"]).Format(2);
                
                // Super Auto PT
                gameGroup.Is3 = Utils.ToBoolean(row["IsAutoPositionTaking"]);
                gameGroup.Us3 = Utils.ToDecimal(row["AutoPositionTaking"]).Format(2);

                // Master Max PT
                gameGroup.M3 = synced ? Utils.ToDecimal(row["MPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineMPositionTaking"]).Format(2);
                
                // Master Min PT
                gameGroup.N3 = synced ? Utils.ToDecimal(row["MinPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineMinPositionTaking"]).Format(2);
                                
                // Assign game types
                var gameTypes = this.GetGameTypeMaster(id, gameGroup.GameGroupID, synced);
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
            CasinoManager.Instance.GetCustSettingCS(ref casinoSettingUpline);

            if (casinoSettingUpline.RoleId - custInfo.RoleId != 1 || casinoSettingUpline.RoleId != 3 || custInfo.RoleId != 2)
            {
                throw new Exception("Invalid custId and uplineId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();

                // Commission
                gameGroup.Aco = synced ? Utils.ToDecimal(row["AgentRollingRate"]) : Utils.ToDecimal(row["UplineAgentRollingRate"]);
                gameGroup.Mco = synced ? Utils.ToDecimal(row["MAgentRollingRate"]) : Utils.ToDecimal(row["UplineMAgentRollingRate"]);

                // Game Group Info
                gameGroup.GameGroupID = Utils.ToInt32(row["GameKind"]);
                gameGroup.GameGroupName = Utils.ToString(row[Lang("GameKindName")]);

                // Master preset PT
                gameGroup.M3 = synced ? Utils.ToDecimal(row["MPositionTaking"]) : Utils.ToDecimal(row["UplineMPositionTaking"]);
                gameGroup.M2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineAPositionTaking"]);
                                
                // Agent Max PT
                gameGroup.A2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineAPositionTaking"]);

                // Agent Min PT
                gameGroup.N2 = synced ? Utils.ToDecimal(row["MinPositionTaking"]) : Utils.ToDecimal(row["UplineMinPositionTaking"]);

                // Assign game types
                var gameTypes = this.GetGameTypeAgent(id, gameGroup.GameGroupID, synced);
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
            CasinoManager.Instance.GetCustSettingCS(ref casinoSettingUpline);

            if (casinoSettingUpline.RoleId - custInfo.RoleId != 1 || casinoSettingUpline.RoleId != 3 || custInfo.RoleId != 2)
            {
                throw new Exception("Invalid custId and uplineId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();

                // Commission
                gameGroup.Aco = synced ? Utils.ToDecimal(row["AgentRollingRate"]) : Utils.ToDecimal(row["UplineAgentRollingRate"]);
                gameGroup.Mco = synced ? Utils.ToDecimal(row["MAgentRollingRate"]) : Utils.ToDecimal(row["UplineMAgentRollingRate"]);

                // Game Group Info
                gameGroup.GameGroupID = Utils.ToInt32(row["GameKind"]);
                gameGroup.GameGroupName = Utils.ToString(row[Lang("GameKindName")]);

                // Master preset PT
                gameGroup.M3 = synced ? Utils.ToDecimal(row["MPositionTaking"]) : Utils.ToDecimal(row["UplineMPositionTaking"]);
                gameGroup.M2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineAPositionTaking"]);

                // Agent Max PT
                gameGroup.A2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineAPositionTaking"]);

                // Agent Min PT
                gameGroup.N2 = synced ? Utils.ToDecimal(row["MinPositionTaking"]) : Utils.ToDecimal(row["UplineMinPositionTaking"]);

                // Assign game types
                var gameTypes = this.GetGameTypeAgent(id, gameGroup.GameGroupID, synced);
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
            CasinoManager.Instance.GetCustSettingCS(ref casinoSettingUpline);

            if (casinoSettingUpline.RoleId - custInfo.RoleId != 1 || casinoSettingUpline.RoleId != 2 || custInfo.RoleId != 1)
            {
                throw new Exception("Invalid custId and uplineId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();

                // Commission
                gameGroup.co = synced ? Utils.ToDecimal(row["MemberRollingRate"]) : Utils.ToDecimal(row["UplineMemberRollingRate"]);
                gameGroup.Aco = synced ? Utils.ToDecimal(row["AgentRollingRate"]) : Utils.ToDecimal(row["UplineAgentRollingRate"]);

                // Game type info
                gameGroup.GameGroupID = Utils.ToInt32(row["GameKindID"]);
                gameGroup.GameGroupName = Utils.ToString(row[Lang("GameKindName")]);

                // Agent preset PT
                gameGroup.M2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineMPositionTaking"]);
                gameGroup.M1 = synced ? Utils.ToDecimal(row["PositionTaking"]) : Utils.ToDecimal(row["UplinePositionTaking"]);

                // Assign game types
                var gameTypes = this.GetGameTypeAgent(id, gameGroup.GameGroupID, synced);
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
            var synced = custInfo.CasinoSync;
            var id = synced ? custId : uplineId;

            var cust = new Customer();
            var dt = cust.GetCustGameKind(id);

            var casinoSettingUpline = new CasinoCustomer();

            // Gets upline settings
            casinoSettingUpline.IBCCustID = uplineId;
            CasinoManager.Instance.GetCustSettingCS(ref casinoSettingUpline);

            if (casinoSettingUpline.RoleId - custInfo.RoleId != 1 || casinoSettingUpline.RoleId != 2 || custInfo.RoleId != 1)
            {
                throw new Exception("Invalid custId and uplineId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();

                // Commission
                gameGroup.co = synced ? Utils.ToDecimal(row["MemberRollingRate"]) : Utils.ToDecimal(row["UplineMemberRollingRate"]);
                gameGroup.Aco = synced ? Utils.ToDecimal(row["AgentRollingRate"]) : Utils.ToDecimal(row["UplineAgentRollingRate"]);

                // Game type info
                gameGroup.GameGroupID = Utils.ToInt32(row["GameKindID"]);
                gameGroup.GameGroupName = Utils.ToString(row[Lang("GameKindName")]);

                // Agent preset PT
                gameGroup.M2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineMPositionTaking"]);
                gameGroup.M1 = synced ? Utils.ToDecimal(row["PositionTaking"]) : Utils.ToDecimal(row["UplinePositionTaking"]);

                // Assign game types
                var gameTypes = this.GetGameTypeAgent(id, gameGroup.GameGroupID, synced);
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
            var synced = custInfo.CasinoSync;
            var id = synced ? custId : uplineId;

            var cust = new Customer();
            var dt = cust.GetCustGameKind(id);

            var casinoSettingUpline = new CasinoCustomer();

            // Gets upline settings
            casinoSettingUpline.IBCCustID = uplineId;
            CasinoManager.Instance.GetCustSettingCS(ref casinoSettingUpline);

            if (casinoSettingUpline.RoleId - custInfo.RoleId != 1 || casinoSettingUpline.RoleId != 2 || custInfo.RoleId != 1)
            {
                throw new Exception("Invalid custId and uplineId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();

                // Commission
                gameGroup.co = synced ? Utils.ToDecimal(row["MemberRollingRate"]) : Utils.ToDecimal(row["UplineMemberRollingRate"]);
                gameGroup.Aco = synced ? Utils.ToDecimal(row["AgentRollingRate"]) : Utils.ToDecimal(row["UplineAgentRollingRate"]);

                // Game type info
                gameGroup.GameGroupID = Utils.ToInt32(row["GameKindID"]);
                gameGroup.GameGroupName = Utils.ToString(row[Lang("GameKindName")]);

                // Agent preset PT
                gameGroup.M2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineMPositionTaking"]);
                gameGroup.M1 = synced ? Utils.ToDecimal(row["PositionTaking"]) : Utils.ToDecimal(row["UplinePositionTaking"]);

                // Assign game types
                var gameTypes = this.GetGameTypeAgent(id, gameGroup.GameGroupID, synced);
                gameGroup.GameTypes = gameTypes;

                gameGroups.Add(gameGroup);
            }

            gameModel.GameGroups = gameGroups;

            return gameModel;
        }

        public CasinoCustomer GetCustInfoIBC(int custid, int uplineId)
        {            
            return this.casino.GetCustInfoIBC(custid, uplineId);
        }

        public void GetCustSettingCS(ref CasinoCustomer custIBC)
        {
            var cust = new Customer();
            var dt = cust.GetCustSetting(custIBC.IBCCustID);
            DataRow row = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
                custIBC.IsEnablePT = Utils.ToBoolean(row["IsEnablePT"]);
                custIBC.TotalWinLoss = Utils.ToDouble(row["ActualMaxTransfer"]);
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

        public Result UpdateMaster(int custId, int uplineId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel settingModel, CasinoCustomer customerModel)
        {
            var errMsg = string.Empty;
            var customer = new Customer();
            var messages = string.Empty;
            var settingsInserted = true;
            var statusUpdated = true;
            var maxTransferUpdated = false;
            var casinoPTResult = new Result();
            var enablePTUpdated = false;

            if (customerModel.UplineRoleId != 4 || customerModel.RoleId != 3)
            {
                return new Result(1, "Invalid customer information");
            }

            // Check whether casino is synced or not to insert customer settings for direct downline only
            if (!customerModel.CasinoSync)
            {
                settingsInserted = customer.InsertCustSetting(
                    "01", 
                    customerModel.IBCCustID, 
                    customerModel.UserName, 
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
                    customerModel.IsAutoReset);
            }

            // Update Status for direct downline only
            if ((settingsInserted && !customerModel.CasinoSync) || customerModel.CasinoSync)
            {                
                statusUpdated = this.UpdateCSStatus(
                    custId, 
                    true, 
                    customerModel.CasinoDisabled, 
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
                enablePTUpdated = customer.UpdateCustEnablePT(custId, true);

                maxTransferUpdated = customer.UpdateMaxTransfer(
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

        private static string Lang(string original)
        {
            return original + "_En";
        }
         
        private bool UpdateCSStatus(int custid, bool syncCS, bool disabledCS, bool isUpdDownline, int userId, string ip, int subAccId, string lang, string username, string subUsername, out string errMsg)
        {
            var status = this.DoUpdateCSStatusWithoutSync(custid, syncCS, disabledCS, isUpdDownline, userId, ip, subAccId, lang, out errMsg);
            
            if (!status)
            {
                return false;
            }

            var cust = new Customer();
            return cust.UpdateCustEnableCasino(custid, !disabledCS, isUpdDownline, ip, custid, subAccId == 0 ? username : subUsername, 2);
        }
       
        private bool DoUpdateCSStatusWithoutSync(int custid, bool syncCS, bool disabledCS, bool isUpdDownline, int userId, string ip, int subAccId, string lang, out string errMsg)
        {
            return this.casino.DoUpdateCSStatusWithoutSync(custid, syncCS, disabledCS, isUpdDownline, userId, ip, subAccId, lang, out errMsg);
        }

        private Result UpdateMasterSettings(int custId, int roleId, int uplineId, int uplineRoleId, string ip, int modifiedId, string modifiedBy, int fromSystem, CasinoSettingModel model)
        {
            var customer = new Customer();
            var msg = string.Empty;
            var gameKindTable = (uplineRoleId == Super || uplineRoleId == Master) && roleId == Member ? 
                this.GameKindsDataTableMember() : this.GameKindsDataTable();
            var gameTypeTable = (uplineRoleId == Super || uplineRoleId == Master) && roleId == Member ? 
                this.GameTypesDataTableMember() : this.GameTypesDataTable();
            var gameStakeTable = this.GameTypesStakesDataTable();

            foreach (var gameGroup in model.GameGroups)
            {
                var gameGroupID = gameGroup.GameGroupID;

                if (uplineRoleId - roleId != 1)
                {
                    // Assign data for indirect downline
                    gameKindTable.Rows.Add(
                        new object[] 
                            {
                                custId,
                                gameGroupID,
                                !(uplineRoleId == Super || uplineRoleId == Master) || gameGroup.Is3 == true ? null : (object)gameGroup.S3,
                                !(uplineRoleId == Master || uplineRoleId == Agent) ? null : (object)gameGroup.M3,
                                gameGroup.Is3 == true ? 1 : 0,
                                !gameGroup.Is3 == true ? null : (object)gameGroup.Us3,
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
                                    !(uplineRoleId == Super || uplineRoleId == Master) || gameType.Is3 == true ? null : (object)gameType.S3,
                                    !(uplineRoleId == Master || uplineRoleId == Agent) ? null : (object)gameType.M3,
                                    gameType.Is3 == true ? 1 : 0,
                                    !gameType.Is3 == true ? null : (object)gameType.Us3,
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
                else
                {
                    // Assign data for direct downline
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
            }

            var resultKind = new DataTable();
            var resultType = new DataTable();
            var resultStake = new DataTable();

            if (uplineRoleId - roleId == 1)
            {
                // Update direct downline
                resultKind = customer.UpdateCustGameKind(gameKindTable);

                if (model.IsPtByGameType)
                {
                    resultType = customer.UpdateCustGameType(gameTypeTable);
                }
                else
                {
                    resultStake = customer.UpdateCustGameTypeStakeLevel(gameStakeTable);
                }
            }
            else
            {
                // Update indirect downline
                resultKind = customer.UpdateCustGameKind_EditMember(gameKindTable);

                if (model.IsPtByGameType)
                {
                    resultType = customer.UpdateCustGameType_EditMember(gameTypeTable);
                }
                else
                {
                    resultStake = customer.UpdateCustGameTypeStakeLevel(gameStakeTable);
                }
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

        private List<GameType> GetGameTypeMaster(int custId, int gameKindID, bool synced)
        {
            var gameTypes = new List<GameType>();
            var cust = new Customer();
            var dt = cust.GetCustGameType(custId, gameKindID);

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                gameType.Mco = synced ? Utils.ToDecimal(row["MAgentRollingRate"]).Format(4) : Utils.ToDecimal(row["UplineMAgentRollingRate"]).Format(4);
                gameType.Sco = synced ? Utils.ToDecimal(row["UplineSMAgentRollingRate"]).Format(4) : Utils.ToDecimal(row["SMAgentRollingRate"]).Format(4);

                // Game type Info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Super preset PT
                gameType.S4 = synced ? Utils.ToDecimal(row["UplineMaxPositionTaking"]).Format(2) : Utils.ToDecimal(row["MaxPositionTaking"]).Format(2);
                gameType.S3 = synced ? Utils.ToDecimal(row["SPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineSPositionTaking"]).Format(2);

                // Super Auto PT
                gameType.Is3 = Utils.ToBoolean(row["IsAutoPositionTaking"]);
                gameType.Us3 = Utils.ToDecimal(row["AutoPositionTaking"]).Format(2);

                // Master Max PT
                gameType.M3 = synced ? Utils.ToDecimal(row["MPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineMPositionTaking"]).Format(2);

                // Master Min PT
                gameType.N3 = synced ? Utils.ToDecimal(row["MinPositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineMinPositionTaking"]).Format(2);

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID);
                gameType.BettingLimits = stakes;

                gameTypes.Add(gameType);
            }

            return gameTypes;
        }

        private List<GameType> GetGameTypeAgent(int custId, int gameKindID, bool synced)
        {
            var gameTypes = new List<GameType>();
            var cust = new Customer();
            var dt = cust.GetCustGameType(custId, gameKindID);

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                // Commission
                gameType.Aco = synced ? Utils.ToDecimal(row["AgentRollingRate"]) : Utils.ToDecimal(row["UplineAgentRollingRate"]);
                gameType.Mco = synced ? Utils.ToDecimal(row["MAgentRollingRate"]) : Utils.ToDecimal(row["UplineMAgentRollingRate"]);

                // Game type info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Master preset PT
                gameType.M3 = synced ? Utils.ToDecimal(row["MPositionTaking"]) : Utils.ToDecimal(row["UplineMPositionTaking"]);
                gameType.M2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineAPositionTaking"]);

                // Agent Max PT
                gameType.A2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineAPositionTaking"]);

                // Agent Min PT
                gameType.N2 = synced ? Utils.ToDecimal(row["MinPositionTaking"]) : Utils.ToDecimal(row["UplineMinPositionTaking"]);

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID);
                gameType.BettingLimits = stakes;

                gameTypes.Add(gameType);
            }

            return gameTypes;
        }

        private List<GameType> GetGameTypeAgentBySuper(int custId, int gameKindID, bool synced)
        {
            var gameTypes = new List<GameType>();
            var cust = new Customer();
            var dt = cust.GetCustGameType(custId, gameKindID);

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                // Commission
                gameType.Aco = synced ? Utils.ToDecimal(row["AgentRollingRate"]) : Utils.ToDecimal(row["UplineAgentRollingRate"]);
                gameType.Mco = synced ? Utils.ToDecimal(row["MAgentRollingRate"]) : Utils.ToDecimal(row["UplineMAgentRollingRate"]);

                // Game type info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Master preset PT
                gameType.M3 = synced ? Utils.ToDecimal(row["MPositionTaking"]) : Utils.ToDecimal(row["UplineMPositionTaking"]);
                gameType.M2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineAPositionTaking"]);

                // Agent Max PT
                gameType.A2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineAPositionTaking"]);

                // Agent Min PT
                gameType.N2 = synced ? Utils.ToDecimal(row["MinPositionTaking"]) : Utils.ToDecimal(row["UplineMinPositionTaking"]);

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID);
                gameType.BettingLimits = stakes;

                gameTypes.Add(gameType);
            }

            return gameTypes;
        }

        private List<GameType> GetGameTypeMember(int custId, int gameKindID, bool synced)
        {
            var gameTypes = new List<GameType>();
            var cust = new Customer();
            var dt = cust.GetCustGameType(custId, gameKindID);

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                // Commission
                gameType.co = synced ? Utils.ToDecimal(row["MemberRollingRate"]) : Utils.ToDecimal(row["UplineMemberRollingRate"]);
                gameType.Aco = synced ? Utils.ToDecimal(row["AgentRollingRate"]) : Utils.ToDecimal(row["UplineAgentRollingRate"]);                

                // Game type info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Agent preset PT
                gameType.M2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineMPositionTaking"]);
                gameType.M1 = synced ? Utils.ToDecimal(row["PositionTaking"]) : Utils.ToDecimal(row["UplinePositionTaking"]);

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID);
                gameType.BettingLimits = stakes;

                gameTypes.Add(gameType);
            }

            return gameTypes;
        }

        private List<GameType> GetGameTypeMemberBySuper(int custId, int gameKindID, bool synced)
        {
            var gameTypes = new List<GameType>();
            var cust = new Customer();
            var dt = cust.GetCustGameType(custId, gameKindID);

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                // Commission
                gameType.co = synced ? Utils.ToDecimal(row["MemberRollingRate"]) : Utils.ToDecimal(row["UplineMemberRollingRate"]);
                gameType.Aco = synced ? Utils.ToDecimal(row["AgentRollingRate"]) : Utils.ToDecimal(row["UplineAgentRollingRate"]);

                // Game type info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Agent preset PT
                gameType.M2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineMPositionTaking"]);
                gameType.M1 = synced ? Utils.ToDecimal(row["PositionTaking"]) : Utils.ToDecimal(row["UplinePositionTaking"]);

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID);
                gameType.BettingLimits = stakes;

                gameTypes.Add(gameType);
            }

            return gameTypes;
        }

        private List<GameType> GetGameTypeMemberByMaster(int custId, int gameKindID, bool synced)
        {
            var gameTypes = new List<GameType>();
            var cust = new Customer();
            var dt = cust.GetCustGameType(custId, gameKindID);

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                // Commission
                gameType.co = synced ? Utils.ToDecimal(row["MemberRollingRate"]) : Utils.ToDecimal(row["UplineMemberRollingRate"]);
                gameType.Aco = synced ? Utils.ToDecimal(row["AgentRollingRate"]) : Utils.ToDecimal(row["UplineAgentRollingRate"]);

                // Game type info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Agent preset PT
                gameType.M2 = synced ? Utils.ToDecimal(row["APositionTaking"]) : Utils.ToDecimal(row["UplineMPositionTaking"]);
                gameType.M1 = synced ? Utils.ToDecimal(row["PositionTaking"]) : Utils.ToDecimal(row["UplinePositionTaking"]);

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID);
                gameType.BettingLimits = stakes;

                gameTypes.Add(gameType);
            }

            return gameTypes;
        }

        private List<StakeLevel> GetStakeLevels(int custId, int gameTypeID)
        {
            var stakeLevels = new List<StakeLevel>();
            var cust = new Customer();
            var dt = cust.GetCustStakeLevel(custId, gameTypeID);

            foreach (DataRow row in dt.Rows)
            {
                var stake = new StakeLevel
                {
                    StakeLevelID = Utils.ToInt32(row["StakeLevelID"]),
                    StakeLevelName = Utils.ToString(row["StakeLevelName"]),
                    IsChecked = Utils.ToBoolean(row["IsChecked"]),
                };

                stakeLevels.Add(stake);
            }

            return stakeLevels;
        }
    }
}