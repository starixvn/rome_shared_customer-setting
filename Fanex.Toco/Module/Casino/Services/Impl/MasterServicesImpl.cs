namespace Fanex.CustomerSetting.Module.Casino.Services.Impl
{
    using CasinoLibrary.Model.UserException;
    using CasinoLibrary.SBAgent;
    using Core;
    using Core.Models;
    using Core.Web;
    using Entities;
    using Fanex.CustomerSetting.Module.Casino.Resources;
    using Interface;
    using Logging.Extensions.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using ViewModels;

    public class MasterServicesImpl : CasinoServiceBaseImpl, MasterServices
    {
        public CasinoSettingModel MasterCasinoModel(int custId, int uplineId)
        {
            var gameModel = new CasinoSettingModel();
            var gameGroups = new List<GameGroup>();

            var custInfo = this.GetCustInfoIBC(custId, uplineId);
            if (custInfo.GeneralInfo.RoleId != Master)
            {
                throw new Exception("Invalid custId");
            }

            var synced = custInfo.GeneralInfo.CasinoSync;
            var id = synced ? custId : uplineId;

            var cust = new Customer();
            var dt = cust.GetCustGameKind(id);

            var casinoSettingUpline = new CasinoCustomer();

            // Gets upline settings
            casinoSettingUpline.GeneralInfo.IBCCustID = uplineId;
            casinoSettingUpline.GeneralInfo.UplineRoleId = custInfo.GeneralInfo.RoleId + 1;
            casinoSettingUpline = this.GetCustSettingCS(casinoSettingUpline);

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

        public Result UpdateMaster(UpdateCasinoCommand command)
        {
            if (AppSession.CurrentSession.IsBelongToSpecialGroups && command.SettingModel.IsUncheckedAllStakeLevels())
            {
                return new Result(500, NGCasinoLang.WarningUncheckAllStakeLevelMsg_DirectionV3);
            }

            var allowedDownlinesResult = CheckAllowedDownlines(command.CustId, 3);
            if (!allowedDownlinesResult.IsSuccess)
            {
                return allowedDownlinesResult;
            }

            var errMsg = string.Empty;
            var messages = string.Empty;
            var settingsInserted = true;
            var updateStatusResult = new Result();
            var updateSettingResult = new Result();
            var cust = new Customer();
            CasinoCustomer customerInfo = null;

            try
            {
                if (!command.CustomerModel.CasinoSync)
                {
                    customerInfo = this.GetCustInfoIBC(command.CustId, command.UplineId);
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
                        command.CustId,
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
            if ((settingsInserted && !command.CustomerModel.CasinoSync) || command.CustomerModel.CasinoSync)
            {
                if (AppSession.CurrentSession.IsBelongToSpecialGroups)
                {
                    if (customerInfo != null)
                    {
                        updateStatusResult = UpdateCSStatus(
                            command.CustId,
                            true,
                            command.CustomerModel.UplineDisabledCasino || customerInfo.GeneralInfo.CasinoDisabled,
                            command.ModifiedId,
                            command.IP,
                            command.SubAccId,
                            command.SystemId,
                            AppSession.Language,
                            command.CustomerModel.UserName,
                            command.SubAccName);
                    }
                }
                else
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
                }

                errMsg = updateStatusResult.ErrorMessage;

                // Enable PT/Update max transfer for direct downline only
                if (updateStatusResult.IsSuccess)
                {
                    cust.UpdateCustEnablePT(command.CustId, true);
                }
            }

            // Update PT/Betting limit settings
            updateSettingResult = this.UpdateMasterSettings(
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
                result = service.UpdateMemberWinLimit(
                    command.CustId,
                    command.ModifiedId,
                    command.SubAccId,
                    command.IP,
                    AppSession.Language,
                    command.CustomerModel.MemberWinLimit);
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
            if (AppSession.CurrentSession.IsBelongToSpecialGroups && model.IsUncheckedAllStakeLevels())
            {
                return new Result(500, NGCasinoLang.WarningUncheckAllStakeLevelMsg_DirectionV3);
            }

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

            var cust = new Customer();
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