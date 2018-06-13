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

    public class AgentServicesImpl : CasinoServiceBaseImpl, AgentServices
    {
        public CasinoSettingModel AgentCasinoModel(int custId, int uplineId)
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

            if (custInfo.GeneralInfo.RoleId != Agent)
            {
                throw new Exception("Invalid custId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.GeneralInfo.PTByGameType;

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
                gameGroup.M2 = Utils.ToDecimal(row["MPositionTaking"]).Format(2); 

                // Master Auto PT
                gameGroup.Im2 = Utils.ToBoolean(row["IsAutoMPositionTaking"]);
                gameGroup.Um2 = Utils.ToDecimal(row["AutoMPositionTaking"]).Format(2);

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
            var synced = custInfo.GeneralInfo.CasinoSync;
            var id = synced ? custId : uplineId;
            var cust = new Customer();
            var dt = cust.GetCustGameKind(id);

            var casinoSettingUpline = new CasinoCustomer();

            // Gets upline settings
            casinoSettingUpline.GeneralInfo.IBCCustID = uplineId;
            casinoSettingUpline.GeneralInfo.UplineRoleId = custInfo.GeneralInfo.RoleId + 2;
            casinoSettingUpline = this.GetCustSettingCS(casinoSettingUpline);

            if (custInfo.GeneralInfo.RoleId != Agent)
            {
                throw new Exception("Invalid custId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.GeneralInfo.PTByGameType;

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

        public Result UpdateAgent(
            int custId, 
            string custName, 
            int uplineId, 
            string ip, 
            int modifiedId, 
            string modifiedBy, 
            int fromSystem, 
            byte systemId, 
            CasinoSettingModel settingModel, 
            GeneralCasinoCustomer customerModel)
        {
            var allowedDownlinesResult = CheckAllowedDownlines(custId, 2);
            if (!allowedDownlinesResult.IsSuccess)
            {
                return allowedDownlinesResult;
            }

            var errMsg = string.Empty;
            var messages = string.Empty;
            var settingsInserted = true;
            var statusUpdated = true;
            var casinoPTResult = new Result();
            var enablePTUpdated = false;
            var cust = new Customer();

            try
            {
                if (!customerModel.CasinoSync)
                {
                    var customerInfo = this.GetCustInfoIBC(custId, uplineId);
                    settingsInserted = cust.InsertCustSetting(
                        "01",
                        custId,
                        custName,
                        customerModel.UserName2,
                        customerModel.FirstName,
                        customerModel.LastName,
                        customerInfo.UserPwd,
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
                        customerInfo.SIBCCustID,
                        customerInfo.MIBCCustID,
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
                        false,
                        customerModel.IsDisableAutoMPT);
                }

                // Enable PT/Update max transfer for direct downline only
                if (statusUpdated)
                {
                    enablePTUpdated = cust.UpdateCustEnablePT(custId, true);
                }
            }
            catch (ErrorException casinoEx)
            {
                ExceptionHandler.Handle(new Exception(casinoEx.Message), TraceEventType.Warning);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, TraceEventType.Warning);
                // Bypass this exception because this customer had setting but update status failed, before.
            }

            // Update Status for direct downline only
            if ((settingsInserted && !customerModel.CasinoSync) || customerModel.CasinoSync)
            {
                statusUpdated = this.UpdateCSStatus(
                    custId,
                    true,
                    customerModel.UplineDisabledCasino ? true : customerModel.CasinoDisabled,
                    uplineId,
                    string.Empty,
                    0,
                    systemId,
                    "en",
                    customerModel.UserName,
                    string.Empty,
                    out errMsg);
            }

            // Update PT/Betting limit settings
            casinoPTResult = this.UpdateAgentSettings(
                custId,
                customerModel.RoleId,
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

            var result = new Result(retMsg == string.Empty || retMsg == "<br />" ? 0 : 1, retMsg.EndsWith(", ") ? retMsg.Substring(0, retMsg.Length - 2) : retMsg);

            if (result.IsSuccess)
            {
                MemberWinLimitService service = new MemberWinLimitServiceImpl();
                result = service.UpdateMemberWinLimit(custId, modifiedId, AppSession.SubAccountId, customerModel.MemberWinLimit);
            }

            return result;
        }

        private Result UpdateAgentSettings(
            int custId, 
            int roleId,
            string ip, 
            int modifiedId, 
            string modifiedBy, 
            int fromSystem, 
            CasinoSettingModel model)
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
                                !(roleId == Super || roleId == Master) || gameGroup.Is3 == true ? null : (object)gameGroup.M3,
                                !(roleId == Master || roleId == Agent) || gameGroup.Im2 == true ?  null : (object)gameGroup.M2,
                                !(roleId == Agent || roleId == Member) ? null : (object)gameGroup.A2,
                                0,
                                null,
                                gameGroup.Im2 == true ? 1 : 0,
                                !gameGroup.Im2 == true ? null : (object)gameGroup.Um2,
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
                                !(roleId == Super || roleId == Master) || gameType.Is3 == true ? null : (object)gameType.M3,
                                !(roleId == Master || roleId == Agent) || gameType.Im2 == true ? null : (object)gameType.M2,
                                !(roleId == Agent || roleId == Member) ? null : (object)gameType.A2,
                                 0,
                                null,
                                 gameType.Im2 == true ? 1 : 0,
                                !gameType.Im2 == true ? null : (object)gameType.Um2,
                                !(roleId == Super || roleId == Master || roleId == Agent) ? null : (object)gameType.N2,
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


                    var gameStakeTableTmp =
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
                            };

                    gameStakeTable.Rows.Add(
                        gameStakeTableTmp
                      );
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

        private List<GameType> GetGameTypeAgent(
            int custId, 
            int gameKindID, 
            bool synced, 
            ref GameGroup gameGroup, 
            bool isPTByGameType = false)
        {
            var cust = new Customer();
            var gameTypes = new List<GameType>();
            var dt = cust.GetCustGameType(custId, gameKindID);
            List<decimal?> mcos = new List<decimal?>(), m3s = new List<decimal?>(), um2s = new List<decimal?>(), a2s = new List<decimal?>(), n2s = new List<decimal?>();
            this._tblCustMultipleStakeLevel = custId != 0 ? cust.GetCustMultipleStakeLevel(custId, gameKindID) : null;
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
                gameType.M2 = Utils.ToDecimal(row["MPositionTaking"]).Format(2);

                // Agent Max PT
                gameType.A2 = synced ? Utils.ToDecimal(row["APositionTaking"]).Format(2) : Utils.ToDecimal(row["UplineAPositionTaking"]).Format(2);

                // Master Auto PT
                gameType.Im2 = Utils.ToBoolean(row["IsAutoMPositionTaking"]);
                gameType.Um2 = Utils.ToDecimal(row["AutoMPositionTaking"]).Format(2);

                // Agent Min PT
                gameType.N2 = !synced ? 0 : Utils.ToDecimal(row["MinPositionTaking"]).Format(2);

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID, 0, synced);
                gameType.BettingLimits = stakes;

                mcos.Add(gameType.Mco);
                m3s.Add(gameType.M3);
                a2s.Add(gameType.A2);
                n2s.Add(gameType.N2);
                um2s.Add(gameType.Um2);

                gameTypes.Add(gameType);
            }

            if (isPTByGameType)
            {
                gameGroup.Mco = mcos.Max();
                gameGroup.M3 = m3s.Max();
                gameGroup.A2 = a2s.Max();
                gameGroup.N2 = n2s.Max();
                gameGroup.Um2 = um2s.Max();
            }

            return gameTypes;
        }

        private List<GameType> GetGameTypeAgentBySuper(int custId, int gameKindID, bool synced)
        {
            var cust = new Customer();
            var gameTypes = new List<GameType>();
            var dt = cust.GetCustGameType(custId, gameKindID);
            this._tblCustMultipleStakeLevel = custId != 0 ? cust.GetCustMultipleStakeLevel(custId, gameKindID) : null;
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
    }
}