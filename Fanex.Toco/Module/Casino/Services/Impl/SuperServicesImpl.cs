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
    using Interface;
    using Logging.Extensions.Web;
    using ViewModels;

    public class SuperServicesImpl : CasinoServiceBaseImpl, SuperServices
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
                ArrayList depositSetting = new BasicInfo().GetCasinoSetting(DepositCasinoNo);
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

            custInfo = this.GetCustSettingCS(custInfo);
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

        public Result UpdateSuper(UpdateCasinoCommand command)
        {
            var allowedDownlinesResult = CheckAllowedDownlines(command.CustId, 4);
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

            try
            {
                var custInfo = this.GetCustInfoIBC(command.CustId, command.UplineId);

                if (!custInfo.GeneralInfo.CasinoSync)
                {
                    settingsInserted = cust.InsertCustSetting(
                        CreditCasinoNo,
                        command.CustId,
                        command.CustName,
                        command.CustomerModel.UserName2,
                        command.CustomerModel.FirstName,
                        command.CustomerModel.LastName,
                        custInfo.UserPwd,
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
                        custInfo.SIBCCustID,
                        command.CustId,
                        custInfo.AIBCCustID,
                        command.CustomerModel.PTByGameType,
                        command.CustomerModel.RoleId.ToString(),
                        DateTime.Now,
                        command.IP,
                        command.ModifiedId,
                        command.ModifiedBy,
                        command.FromSystem,
                        command.CustomerModel.IsAutoReset,
                        false,
                        IsEnabledJackpot(command.CustId),
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
                if (!AppSession.CurrentSession.IsBelongToSpecialGroups)
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

                if (updateStatusResult.IsSuccess)
                {
                    cust.UpdateCustEnablePT(command.CustId, true);
                }
            }

            // Update PT/Betting limit settings
            updateSettingResult = UpdateSuperSettings(
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

        /// <summary>
        /// Used for ExtNAP edit betting limit and Search and Copy on NAP
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="roleId"></param>
        /// <param name="ip"></param>
        /// <param name="modifiedId"></param>
        /// <param name="modifiedBy"></param>
        /// <param name="fromSystem"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result UpdateSuperBettingLimit(
            int custId, 
            int roleId, 
            string ip, 
            int modifiedId, 
            string modifiedBy, 
            int fromSystem, 
            CasinoSettingModel model)
        {
            var msg = string.Empty;
            var gameStakeTable = this.GameTypesStakesDataTable();

            foreach (var gameGroup in model.GameGroups)
            {
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

            var cust = new Customer();

            var resultStake = cust.UpdateCustGameTypeStakeLevel(gameStakeTable);

            // Process error messages
            foreach (DataRow item in resultStake.Rows)
            {
                msg += item.Field<string>("ErrMsg") + "<br />";
            }

            msg = msg.EndsWith("<br />") ? msg.Substring(0, msg.LastIndexOf("<br />")) : msg;

            return new Result((msg == string.Empty || resultStake.Rows.Count == 0) ? 0 : 1, msg);
        }

        public bool IsEnabledJackpot(int custId)
        {
            var productIds = casinoRepository.GetEnabledJackpotProducts(new Criteria.GetEnabledJackpotProductsCriteria(custId, Configurations.AppSettings.LocalTest));

            return productIds.Any();
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

            var resultType = new DataTable();
            var resultStake = new DataTable();
            var cust = new Customer();

            var resultKind = cust.UpdateCustGameKind(gameKindTable);

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
    }
}