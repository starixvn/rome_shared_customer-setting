namespace Fanex.CustomerSetting.Module.Casino.Services.Impl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
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

    public class SuperServices : CasinoServiceBase
    {
        public CasinoSettingModel SuperCasinoModel(int custId, int uplineId)
        {
            var gameModel = new CasinoSettingModel();
            var gameGroups = new List<GameGroup>();
            var custInfo = this.GetCustInfoIBC(custId, uplineId);
            decimal maxPT = Utils.ToDecimal(ConfigurationManager.AppSettings["MaxPT"]).Format();
            decimal maxComm = Utils.ToDecimal(ConfigurationManager.AppSettings["MaxComm"]).Format(4);
            if (custInfo.GeneralInfo.IsDeposit)
            {
                ArrayList depositSetting = new BasicInfo().GetCasinoSetting("02");
                maxPT = Utils.ToDecimal(depositSetting[3]).Format(2);
                maxComm = Utils.ToDecimal(depositSetting[4]).Format(2);
            }

            var synced = custInfo.GeneralInfo.CasinoSync;
            var id = synced ? custId : 0;
            var cust = new Customer();
            var dt = synced ? cust.GetCustGameKind(custId) : new BasicInfo().GetGameKind(custInfo.GeneralInfo.CasinoNo);

            if (custInfo.GeneralInfo.RoleId != Super)
            {
                throw new Exception("Invalid custId");
            }

            this.GetCustSettingCS(ref custInfo);
            gameModel.IsPtByGameType = custInfo.GeneralInfo.PTByGameType;

            foreach (DataRow row in dt.Rows)
            {
                var gameGroup = new GameGroup();

                // Game Group Info
                gameGroup.GameGroupID = !synced ? Utils.ToInt32(row["CodeID"]) : Utils.ToInt32(row["GameKind"]);
                gameGroup.GameGroupName = !synced ? Utils.ToString(row[Lang("CodeName")]) : Utils.ToString(row["GameKindName"]);

                // Super preset PT
                gameGroup.S4 = maxPT;
                gameGroup.S3 = !synced ? 0 : Utils.ToDecimal(row["MaxPositionTaking"]).Format();

                // Master Min PT
                gameGroup.N4 = gameGroup.S3;
                gameGroup.N3 = !synced ? 0 : Utils.ToDecimal(row["MinPositionTaking"]).Format();

                // Commission
                if (gameGroup.GameGroupID.Equals(136))
                {
                    gameGroup.Sco = 0;
                    gameGroup.Mco = 0;
                }
                else
                {
                    gameGroup.Sco = maxComm;
                    gameGroup.Mco = !synced ? 0 : Utils.ToDecimal(row["SMAgentRollingRate"]).Format(4);
                }

                // Assign game types
                var gameTypes = this.GetGameTypeSuper(id, maxPT, maxComm, gameGroup.GameGroupID, synced, ref gameGroup, custInfo.GeneralInfo.Currency, gameModel.IsPtByGameType, custInfo.GeneralInfo.SiteName);
                gameGroup.GameTypes = gameTypes;

                gameGroups.Add(gameGroup);
            }

            gameModel.GameGroups = gameGroups;

            return gameModel;
        }

        public Result UpdateSuper(
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
            var allowedDownlinesResult = CheckAllowedDownlines(custId, 4);
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
                var custInfo = this.GetCustInfoIBC(custId, uplineId);

                if (!custInfo.GeneralInfo.CasinoSync)
                {
                    settingsInserted = cust.InsertCustSetting(
                        "01",
                        custId,
                        custName,
                        customerModel.UserName2,
                        customerModel.FirstName,
                        customerModel.LastName,
                        custInfo.UserPwd,
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
                        custInfo.SIBCCustID,
                        custId,
                        custInfo.AIBCCustID,
                        customerModel.PTByGameType,
                        customerModel.RoleId.ToString(),
                        DateTime.Now,
                        ip,
                        modifiedId,
                        modifiedBy,
                        fromSystem,
                        customerModel.IsAutoReset,
                        false,
                        IsEnabledJackpot(custId),
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
                    modifiedId,
                    ip,
                    AppSession.SubAccountId,
                    systemId,
                    AppSession.Language,
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
            casinoPTResult = this.UpdateSuperSettings(
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
                ICasinoMemberWinLimitService service = new CasinoMemberWinLimitService();
                result = service.UpdateMemberWinLimit(custId, modifiedId, AppSession.SubAccountId, customerModel.MemberWinLimit);
            }

            return result;
        }

        private List<GameType> GetGameTypeSuper(
            int custId, 
            decimal maxPT,
            decimal comm, 
            int gameKindID, 
            bool synced,
            ref GameGroup gameGroup, 
            int exchangeId = 0, 
            bool isPTByGameType = false, 
            string site = null)
        {
            var cust = new Customer();
            var gameTypes = new List<GameType>();
            var dt = synced ? cust.GetCustGameType(custId, gameKindID) : new BasicInfo().GetGameType(gameKindID, custId, exchangeId);
            List<decimal?> s4s = new List<decimal?>(), n4s = new List<decimal?>(), scos = new List<decimal?>();
            this._tblCustMultipleStakeLevel = custId != 0 ? cust.GetCustMultipleStakeLevel(custId, gameKindID) : null;
            foreach (DataRow row in dt.Rows)
            {
                var gameType = new GameType();

                gameType.Sco = comm;
                gameType.Mco = !synced ? 0 : Utils.ToDecimal(row["SMAgentRollingRate"]).Format(4);

                // Game type Info
                gameType.GameTypeID = Utils.ToInt32(row["GameTypeID"]);
                gameType.GameTypeName = Utils.ToString(row[Lang("GameTypeName")]);

                // Super preset PT
                gameType.S4 = maxPT;
                gameType.S3 = !synced ? 0 : Utils.ToDecimal(row["MaxPositionTaking"]).Format();

                // Master Min PT
                gameType.N4 = gameType.S3;
                gameType.N3 = !synced ? 0 : Utils.ToDecimal(row["MinPositionTaking"]).Format();

                // Betting limits
                var stakes = this.GetStakeLevels(custId, gameType.GameTypeID, exchangeId, synced, true, site);
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

        private Result UpdateSuperSettings(
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
    }
}