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

    public class MasterServicesImpl : CasinoServiceBaseImpl, MasterServices
    {
        public CasinoSettingModel MasterCasinoModel(int custId, int uplineId)
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

            if (custInfo.GeneralInfo.RoleId != Master)
            {
                throw new Exception("Invalid custId");
            }

            gameModel.IsPtByGameType = casinoSettingUpline.GeneralInfo.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();

                // Commission
                gameGroup.Mco = Utils.ToDecimal(row["MAgentRollingRate"]).Format(4); 
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
                gameGroup.S3 = Utils.ToDecimal(row["SPositionTaking"]).Format(2); 

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

        public Result UpdateMaster(
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
            var allowedDownlinesResult = CheckAllowedDownlines(custId, 3);
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

                // Enable PT/Update max transfer for direct downline only
                if (statusUpdated)
                {
                    enablePTUpdated = cust.UpdateCustEnablePT(custId, true);
                }
            }

            // Update PT/Betting limit settings
            casinoPTResult = this.UpdateMasterSettings(
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

        private Result UpdateMasterSettings(
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
                                !(roleId == Master || roleId == Agent) || gameGroup.Im2== true ? null : (object)gameGroup.M3,
                                !(roleId == Agent || roleId == Member) ? null : (object)gameGroup.N3,
                                gameGroup.Is3 == true ? 1 : 0,
                                !gameGroup.Is3 == true ? null : (object)gameGroup.Us3,
                                0,
                                null,
                                !(roleId == Super || roleId == Master || roleId == Agent) ? null : (object)gameGroup.N3,
                                0,
                                roleId != Super ? null : (object)gameGroup.Mco, //// Mco instead of Sco coz use in View.
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
                                !(roleId == Master || roleId == Agent)  || gameType.Im2 == true? null : (object)gameType.M3,
                                !(roleId == Agent || roleId == Member) ? null : (object)gameType.N3,
                                gameType.Is3 == true ? 1 : 0,
                                !gameType.Is3 == true ? null : (object)gameType.Us3,
                                 0,
                                 null,
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

        private List<GameType> GetGameTypeMaster(
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
            List<decimal?> scos = new List<decimal?>(), s4s = new List<decimal?>(),
                us3s = new List<decimal?>(), m3s = new List<decimal?>(), n3s = new List<decimal?>();

            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                gameType.Mco = Utils.ToDecimal(row["MAgentRollingRate"]).Format(4); 
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
                gameType.S3 = Utils.ToDecimal(row["SPositionTaking"]).Format(2); 

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
    }
}