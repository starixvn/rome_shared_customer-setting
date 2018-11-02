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
    using Entities;
    using Interface;
    using Logging.Extensions.Web;
    using ViewModels;

    public class MemberServicesImpl : CasinoServiceBaseImpl, MemberServices
    {
        public CasinoSettingModel MemberCasinoModel(int custId, int uplineId)
        {
            var gameModel = new CasinoSettingModel();
            var gameGroups = new List<GameGroup>();
            var custInfo = this.GetCustInfoIBC(custId, uplineId);
            var synced = custInfo.GeneralInfo.CasinoSync;
            var id = synced ? custId : uplineId;
            var cust = new Customer();
            var dt = cust.GetCustGameKind(id);

            var casinoSettingUpline = new CasinoCustomer();

            // Gets upline settings
            casinoSettingUpline.GeneralInfo.IBCCustID = uplineId;
            casinoSettingUpline.GeneralInfo.UplineRoleId = custInfo.GeneralInfo.RoleId + 1;
            casinoSettingUpline = this.GetCustSettingCS(casinoSettingUpline);

            if (custInfo.GeneralInfo.RoleId != 1)
            {
                throw new Exception("Invalid custId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.GeneralInfo.PTByGameType;

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
            casinoSettingUpline.GeneralInfo.IBCCustID = uplineId;
            casinoSettingUpline.GeneralInfo.UplineRoleId = custInfo.GeneralInfo.RoleId + 3;
            casinoSettingUpline = this.GetCustSettingCS(casinoSettingUpline);

            if (custInfo.GeneralInfo.RoleId != Member)
            {
                throw new Exception("Invalid custId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.GeneralInfo.PTByGameType;

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
            var dt = cust.GetCustGameKind_EditMember(custId);
            var dtmaster = cust.GetCustGameKind(custId);

            var casinoSettingUpline = new CasinoCustomer();

            // Gets upline settings
            casinoSettingUpline.GeneralInfo.IBCCustID = uplineId;
            casinoSettingUpline.GeneralInfo.UplineRoleId = custInfo.GeneralInfo.RoleId + 2;
            casinoSettingUpline = this.GetCustSettingCS(casinoSettingUpline);

            if (custInfo.GeneralInfo.RoleId != Member)
            {
                throw new Exception("Invalid custId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.GeneralInfo.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();
                var isAutoOnMember = false;

                foreach (DataRow dtrow in dtmaster.Rows)
                {
                    if (row["GameKind"].Equals(dtrow["GameKind"]))
                    {
                        isAutoOnMember = Utils.ToBoolean(dtrow["UplineIsAutoMPositionTaking"]);
                        break;
                    }
                }

                // Game type info
                gameGroup.GameGroupID = Utils.ToInt32(row["GameKind"]);
                gameGroup.GameGroupName = Utils.ToString(row[Lang("GameKindName")]);

                // Super preset PT
                gameGroup.S4 = Utils.ToDecimal(row["MasterMaxPositionTaking"]).Format(2);
                gameGroup.S3 = Utils.ToDecimal(row["SPositionTaking"]).Format(2);

                // Agent preset PT
                gameGroup.M3 = Utils.ToDecimal(row["MPositionTaking"]).Format(2);
                gameGroup.M2 = Utils.ToDecimal(row["APositionTaking"]).Format(2);
                var min = Utils.ToDecimal(row["MasterMinPositionTaking"]).Format(2) - Utils.ToDecimal(row["APositionTaking"]).Format(2);
                gameGroup.N2 = min < 0 ? 0 : min;

                // Master Auto PT
                gameGroup.Im1 = Utils.ToBoolean(row["IsAutoMPositionTaking"]);
                gameGroup.Im2 = isAutoOnMember;
                gameGroup.Um1 = Utils.ToDecimal(row["AutoMPositionTaking"]).Format(2);

                // Assign game types
                var gameTypes = this.GetGameTypeMemberByMaster(custId, gameGroup.GameGroupID, ref gameGroup, gameModel.IsPtByGameType);
                gameGroup.GameTypes = gameTypes;

                gameGroups.Add(gameGroup);
            }

            gameModel.GameGroups = gameGroups;

            return gameModel;
        }

        public Result UpdateMember(UpdateCasinoCommand command)
        {
            var errMsg = string.Empty;
            var messages = string.Empty;
            var settingsInserted = true;
            var updateStatusResult = new Result();
            var updateSettingResult = new Result();
            var cust = new Customer();

            try
            {
                if (!command.CustomerModel.CasinoSync)
                {
                    try
                    {
                        var customerInfo = this.GetCustInfoIBC(command.CustId, command.UplineId);
                        settingsInserted = cust.InsertCustSetting(
                            CreditCasinoNo,
                            command.CustId,
                            command.CustName,
                            command.CustomerModel.UserName2,
                            command.CustomerModel.FirstName,
                            command.CustomerModel.LastName,
                            customerInfo.UserPwd,
                            command.CustomerModel.Phone,
                            command.CustomerModel.MobilePhone,
                            command.CustomerModel.Currency,
                            command.CustomerModel.Fax,
                            command.CustomerModel.Closed,
                            command.CustomerModel.Suspend,
                            command.CustomerModel.IsEnablePT,
                            (decimal)command.CustomerModel.MaxWin,
                            (decimal)command.CustomerModel.MaxLose,
                            true,
                            (decimal)command.CustomerModel.MaxTransferCredit,
                            customerInfo.SIBCCustID,
                            customerInfo.MIBCCustID,
                            customerInfo.AIBCCustID,
                            command.CustomerModel.PTByGameType,
                            command.CustomerModel.RoleId.ToString(),
                            DateTime.Now,
                            command.IP,
                            command.ModifiedId,
                            command.ModifiedBy,
                            command.FromSystem,
                            command.CustomerModel.IsAutoReset,
                            false,
                            false,
                            command.CustomerModel.IsDisableAutoMPT);
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.Handle(ex, TraceEventType.Warning);
                        settingsInserted = false;
                    }
                }
            }
            catch (ErrorException casinoEx)
            {
                ExceptionHandler.Handle(new Exception(casinoEx.Message), TraceEventType.Warning);
            }
            catch (Exception)
            {
                // Bypass this exception because this customer had setting but update status failed, before.
            }

            // Update Status for direct downline only
            if ((settingsInserted && !command.CustomerModel.CasinoSync) || command.CustomerModel.CasinoSync)
            {
                updateStatusResult = UpdateCSStatus(
                    command.CustId,
                    true,
                    command.CustomerModel.UplineDisabledCasino ? true : command.CustomerModel.CasinoDisabled,
                    command.ModifiedId,
                    command.IP,
                    command.SubAccId,
                    command.SystemId,
                    AppSession.Language,
                    command.CustomerModel.UserName,
                    command.SubAccName);

                if (updateStatusResult.IsSuccess)
                {
                    cust.UpdateCustEnablePT(command.CustId, true);
                }
            }

            // Update PT/Betting limit settings
            updateSettingResult = this.UpdateMemberSettings(
                command.CustId,
                command.CustomerModel.RoleId,
                command.IP,
                command.ModifiedId,
                command.ModifiedBy,
                command.FromSystem,
                command.SettingModel);

            // Gets error messages
            messages += string.IsNullOrWhiteSpace(errMsg) && updateSettingResult.IsSuccess ? string.Empty : !string.IsNullOrWhiteSpace(errMsg) ? errMsg + "<br />" + updateSettingResult.ErrorMessage : updateSettingResult.ErrorMessage;

            var msgs = messages.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var retMsg = string.Empty;

            foreach (var item in msgs)
            {
                retMsg += string.Format("{0}{1}", item, ", ");
            }

            var result = new Result(retMsg == string.Empty || retMsg == "<br />" ? 0 : 1, retMsg.EndsWith(", ") ? retMsg.Substring(0, retMsg.Length - 2) : retMsg);

            if (result.IsSuccess)
            {
                MemberWinLimitService service = new MemberWinLimitServiceImpl();
                result = service.UpdateMemberWinLimit(command.CustId, command.ModifiedId, command.SubAccId, command.CustomerModel.MemberWinLimit);
            }

            return result;
        }

        public Result UpdateMemberBySuper(UpdateCasinoCommand command)
        {
            var errMsg = string.Empty;
            var messages = string.Empty;
            var casinoPTResult = new Result();

            // Update PT/Betting limit settings
            casinoPTResult = this.UpdateMemberSettingsBySuper(
                command.CustId,
                command.CustomerModel.RoleId,
                command.CustomerModel.UplineRoleId,
                command.IP,
                command.ModifiedId,
                command.ModifiedBy,
                command.FromSystem,
                command.SettingModel);

            // Gets error messages
            messages += string.IsNullOrWhiteSpace(errMsg) && casinoPTResult.IsSuccess ? string.Empty : !string.IsNullOrWhiteSpace(errMsg) ? errMsg + "<br />" + casinoPTResult.ErrorMessage : casinoPTResult.ErrorMessage;

            var msgs = messages.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var retMsg = string.Empty;

            foreach (var item in msgs)
            {
                retMsg += string.Format("{0}{1}", item, ", ");
            }

            var result = new Result(retMsg == string.Empty || retMsg == "<br />" ? 0 : 1, retMsg.EndsWith(", ") ? retMsg.Substring(0, retMsg.Length - 2) : retMsg);

            return result;
        }

        public Result UpdateMemberByMaster(UpdateCasinoCommand command)
        {
            var errMsg = string.Empty;
            var messages = string.Empty;
            var casinoPTResult = new Result();

            // Update PT/Betting limit settings
            casinoPTResult = this.UpdateMemberSettingsByMaster(
                command.CustId,
                command.CustomerModel.RoleId,
                command.CustomerModel.UplineRoleId,
                command.IP,
                command.ModifiedId,
                command.ModifiedBy,
                command.FromSystem,
                command.SettingModel);

            // Gets error messages
            messages += string.IsNullOrWhiteSpace(errMsg) && casinoPTResult.IsSuccess ? string.Empty : !string.IsNullOrWhiteSpace(errMsg) ? errMsg + "<br />" + casinoPTResult.ErrorMessage : casinoPTResult.ErrorMessage;

            var msgs = messages.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var retMsg = string.Empty;

            foreach (var item in msgs)
            {
                retMsg += string.Format("{0}{1}", item, ", ");
            }

            var result = new Result(retMsg == string.Empty || retMsg == "<br />" ? 0 : 1, retMsg.EndsWith(", ") ? retMsg.Substring(0, retMsg.Length - 2) : retMsg);

            return result;
        }

        public Result UpdateMemberSettings(
            int custId, 
            int roleId, 
            string ip, 
            int modifiedId, 
            string modifiedBy, 
            int fromSystem, 
            CasinoSettingModel model)
        {
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

            var resultKind = cust.UpdateCustGameKind(gameKindTable);

            var resultType = new DataTable();
            var resultStake = new DataTable();

            if (model.IsPtByGameType)
            {
                resultType = cust.UpdateCustGameType(gameTypeTable);
            }
            else
            {
                resultStake = cust.UpdateCustGameTypeStakeLevel(gameStakeTable);
            }

            return BuildMessages(resultKind, resultType, resultStake, model);
        }

        private Result UpdateMemberSettingsBySuper(
            int custId, 
            int roleId, 
            int uplineRoleId, 
            string ip, 
            int modifiedId, 
            string modifiedBy, 
            int fromSystem, 
            CasinoSettingModel model)
        {
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
                                !(uplineRoleId == Super || uplineRoleId == Master) || gameType.Is1 == true || gameType.Is3 == true ? null : (object)gameType.S3,
                                !(uplineRoleId == Master || uplineRoleId == Agent) ? null : (object)gameType.M3,
                                gameType.Is1 == true ? 1 : 0,
                                !gameType.Is1 == true ? null : (object)gameType.Us3,
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

            var resultKind = cust.UpdateCustGameKind_EditMember(gameKindTable);

            var resultType = new DataTable();

            if (model.IsPtByGameType)
            {
                resultType = cust.UpdateCustGameType_EditMember(gameTypeTable);
            }

            // var resultStake = cust.UpdateCustGameTypeStakeLevel(gameStakeTable); Hotfix 93258

            return BuildMessages(resultKind, resultType, new DataTable(), model);
        }

        private Result UpdateMemberSettingsByMaster(
            int custId, 
            int roleId, 
            int uplineRoleId, 
            string ip, 
            int modifiedId, 
            string modifiedBy, 
            int fromSystem, 
            CasinoSettingModel model)
        {
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
                                !(uplineRoleId == Master || uplineRoleId == Agent) || gameGroup.Im1 == true || gameGroup.Im2 == true? null : (object)gameGroup.M3,
                                0,
                                null,
                                 gameGroup.Im1 == true ? 1 : 0,
                                !gameGroup.Im1 == true ? null : (object)gameGroup.Um1,
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
                                !(uplineRoleId == Master || uplineRoleId == Agent) || gameType.Im1 == true || gameType.Im2 == true? null : (object)gameType.M3,
                                0,
                                null,
                                 gameType.Im1 == true ? 1 : 0,
                                !gameType.Im1 == true ? null : (object)gameType.Um1,
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

            var resultKind = cust.UpdateCustGameKind_EditMember(gameKindTable);

            var resultType = new DataTable();

            if (model.IsPtByGameType)
            {
                resultType = cust.UpdateCustGameType_EditMember(gameTypeTable);
            }

            // var resultStake = cust.UpdateCustGameTypeStakeLevel(gameStakeTable); Hotfix 93258

            return BuildMessages(resultKind, resultType, new DataTable(), model);
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
                    new DataColumn("IsAutoMPositionTaking"),
                    new DataColumn("AutoMPositionTaking"),
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
                    new DataColumn("IsAutoMPositionTaking"),
                    new DataColumn("AutoMPositionTaking"),
                    new DataColumn("RoleId"),
                    new DataColumn("FromIP"),
                    new DataColumn("ModifyBy"),
                    new DataColumn("ModifyUserName"),
                    new DataColumn("FromSystem")
                });

            return dt;
        }

        private List<GameType> GetGameTypeMember(
            int custId, 
            int gameKindID, 
            bool synced, 
            ref GameGroup gameGroup, 
            bool isPTByGameType = false)
        {
            var cust = new Customer();
            var gameTypes = new List<GameType>();
            var dt = cust.GetCustGameType(custId, gameKindID);
            this._tblCustMultipleStakeLevel = custId != 0 ? cust.GetCustMultipleStakeLevel(custId, gameKindID) : null;
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

        private List<GameType> GetGameTypeMemberBySuper(
            int custId, 
            int gameKindID, 
            ref GameGroup gameGroup, 
            bool isPTByGameType = false)
        {
            var cust = new Customer();
            var gameTypes = new List<GameType>();
            var dt = cust.GetCustGameType_EditMember(custId, gameKindID);
            var dtsuper = cust.GetCustGameType(custId, gameKindID);
            this._tblCustMultipleStakeLevel = custId != 0 ? cust.GetCustMultipleStakeLevel(custId, gameKindID) : null;
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

        private List<GameType> GetGameTypeMemberByMaster(
            int custId, 
            int gameKindID, 
            ref GameGroup gameGroup, 
            bool isPTByGameType = false)
        {
            var cust = new Customer();
            var gameTypes = new List<GameType>();
            var dtMaster = cust.GetCustGameType(custId, gameKindID);
            var dt = cust.GetCustGameType_EditMember(custId, gameKindID);
            this._tblCustMultipleStakeLevel = custId != 0 ? cust.GetCustMultipleStakeLevel(custId, gameKindID) : null;
            List<decimal?> s4s = new List<decimal?>(), m3s = new List<decimal?>(), n2s = new List<decimal?>();

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();
                var isAutoOnMember = false;

                foreach (DataRow dtrow in dtMaster.Rows)
                {
                    if (row["GameTypeID"].Equals(dtrow["GameTypeID"]))
                    {
                        isAutoOnMember = Utils.ToBoolean(dtrow["IsAutoMPositionTaking"]);
                        break;
                    }
                }

                // Game type info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Super preset PT
                gameType.S4 = Utils.ToDecimal(row["MasterMaxPositionTaking"]).Format(2);
                gameType.S3 = Utils.ToDecimal(row["SPositionTaking"]).Format(2);

                // Agent preset PT
                gameType.M3 = Utils.ToDecimal(row["MPositionTaking"]).Format(2);
                gameType.M2 = Utils.ToDecimal(row["APositionTaking"]).Format(2);
                var min = Utils.ToDecimal(row["MasterMinPositionTaking"]).Format(2) - Utils.ToDecimal(row["APositionTaking"]).Format(2);
                gameType.N2 = min < 0 ? 0 : min;

                // Master Auto PT
                gameType.Im1 = Utils.ToBoolean(row["IsAutoMPositionTaking"]);
                gameType.Im2 = true;
                gameType.Um1 = Utils.ToDecimal(row["AutoMPositionTaking"]).Format(2);

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
    }
}