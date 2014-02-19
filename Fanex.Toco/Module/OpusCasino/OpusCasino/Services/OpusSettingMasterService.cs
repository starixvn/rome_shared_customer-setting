// ************************************************************************************
// <copyright file="OpusSettingMasterService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="Thai.Tran">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.OpusCasino.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Common;
    using Fanex.CustomerSetting.OpusCasino.Criteria;
    using Fanex.CustomerSetting.OpusCasino.Helper;
    using Fanex.CustomerSetting.OpusCasino.ViewModels;
    using Fanex.Data;
    using Fanex.OpusCasino;

    /// <summary>
    /// OpusSettingSuperService Class.
    /// </summary>
    public class OpusSettingMasterService : OpusSettingService, IOpusSettingMasterService
    {
        /// <summary>
        /// Get Master setting.
        /// </summary>
        /// <param name="custId">Customer user of user.</param>
        /// <returns>Master Setting object.</returns>
        public OpusSettingMaster GetMasterSetting(int custId)
        {
            OpusSettingMaster masterOpusModel = new OpusSettingMaster();

            SelectMasterCriteria criteria = new SelectMasterCriteria
            {
                CustId = custId
            };

            DataSet dataSet = this.GetDataSetting(criteria);
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                var customerTable = dataSet.Tables[0];
                var gameTypeTable = dataSet.Tables[1];
                foreach (DataRow row in customerTable.Rows)
                {
                    this.MapCustomerInfo(ref masterOpusModel, row, custId);
                    this.MapPositionTaking(ref masterOpusModel, row);
                    this.MapBetLimitSetting(ref masterOpusModel, row);
                }

                masterOpusModel.GameSetting = this.GetGameSetting(gameTypeTable);
            }

            return masterOpusModel;
        }

        /// <summary>
        /// Update Master setting.
        /// </summary>
        /// <param name="opusSetting">OpusSettingSuper object.</param>
        /// <returns>Result object.</returns>
        public List<Result> UpdateMaster(OpusSettingMaster opusSetting)
        {
            List<Result> resultList = new List<Result>();
            try
            {
                CustomerInfo customerInfo = opusSetting.CustomerInformation;
                PositionTakingMaster pt = opusSetting.PositionTaking;
                BetSetting betSetting = opusSetting.BetLimitSetting;
                List<Result> resultsTmp = new List<Result>();

                if (opusSetting.GameSetting.IsUpdateGameSetting)
                {
                    resultList = this.UpdateStake(opusSetting.GameSetting, opusSetting.CustomerInformation);
                }

                if (customerInfo.IsUpdateStatus || pt.IsUpdatePT || betSetting.IsUpdateBetSetting)
                {
                    resultsTmp = UpdateMasterOpusAll(opusSetting);

                    foreach (var result in resultsTmp)
                    {
                        resultList.Add(result);
                    }
                }
            }
            catch (Exception ex)
            {
                resultList.Add(new Result
                {
                    ErrorCode = 100,
                    ErrorMessage = ex.Message
                });
            }

            return resultList;
        }

        /// <summary>
        /// Map customer information.
        /// </summary>
        /// <param name="masterOpusModel">Master opus model.</param>
        /// <param name="row">DataRow row.</param>
        /// <param name="custId">Customer Id.</param>
        private void MapCustomerInfo(ref OpusSettingMaster masterOpusModel, DataRow row, int custId)
        {
            masterOpusModel.CustomerInformation = new CustomerInfo
            {
                CustId = custId,
                RoleId = 4,
                Username = Converter.ToString2(row["username"]),
                NickName = Converter.ToString2(row["username2"]),
                FirstName = Converter.ToString2(row["firstname"]),
                LastName = Converter.ToString2(row["lastname"]),
                Phone = Converter.ToString2(row["phone"]),
                MobilePhone = Converter.ToString2(row["mobilephone"]),
                Fax = Converter.ToString2(row["fax"]),
                IsDisable = Converter.ToBool(row["DisableOpusCasino"]),
                IsDisableDownlines = Converter.ToBool(row["DisableOpusCasinoDownlines"])
            };
        }

        /// <summary>
        /// Map Position Taking.
        /// </summary>
        /// <param name="masterOpusModel">Master Opus Model.</param>
        /// <param name="row">DataRow row.</param>
        private void MapPositionTaking(ref OpusSettingMaster masterOpusModel, DataRow row)
        {
            masterOpusModel.PositionTaking = new PositionTakingMaster
                    {
                        S4 = Converter.To<double>(row["v163spositiontaking_1401_max"]),
                        S3 = Converter.To<double>(row["v163spositiontaking_1401"]),
                        US3 = Converter.To<double>(row["auto_v163spositiontaking_1401"]),
                        IS3 = Converter.To<bool>(row["isauto_v163spositiontaking_1401"]),
                        M3 = Converter.To<double>(row["v163mpositiontaking_1401"]),
                        N3 = Converter.To<double>(row["v163spositiontaking_1401_min"]),
                        Comm4 = Converter.To<double>(row["v163dicount_1401_max"]),
                        Comm3 = Converter.To<double>(row["v163discount_1401"]),
                    };
        }

        /// <summary>
        /// Map Bet Limit Setting.
        /// </summary>
        /// <param name="masterOpusModel">Master opus model.</param>
        /// <param name="row">DataRow row.</param>
        private void MapBetLimitSetting(ref OpusSettingMaster masterOpusModel, DataRow row)
        {
            masterOpusModel.BetLimitSetting = new BetSetting
            {
                MaxTransfer = Converter.To<double>(row["MaxTransferOpusCasino"]),
                MaxTransferAllowed = Converter.To<double>(row["MaxTransferOpusCasino_max"]),
                IsLimited = true,
                IsDailyAutoResetLimit = Converter.To<bool>(row["isAutoResetOpusCasino"]),
            };
        }

        /// <summary>
        /// Update Customer information, bet setting and position taking.
        /// </summary>
        /// <param name="opusSetting">Opus Setting.</param>
        /// <returns>List results.</returns>
        private List<Result> UpdateMasterOpusAll(OpusSettingMaster opusSetting)
        {
            string errorCode = string.Empty;
            string errorMsg = string.Empty;
            CustomerInfo customerInfo = opusSetting.CustomerInformation;
            PositionTakingMaster pt = opusSetting.PositionTaking;
            BetSetting betSetting = opusSetting.BetLimitSetting;

            UpdateMasterCriteria criteria = new UpdateMasterCriteria
            {
                CustId = customerInfo.CustId,
                Srecommend = opusSetting.CustomerInformation.UplineId,
                UserId = customerInfo.UserId,
                SubAccId = customerInfo.SubAccId,
                Ip = customerInfo.Ip,
                Language = customerInfo.Language,
                V163UpdateCustomerStatus_1401 = customerInfo.IsUpdateStatus,
                Disable = customerInfo.IsDisable,
                DisableDownLines = customerInfo.IsDisableDownlines,
                V163UpdatePositionTaking_1401 = pt.IsUpdateS3,
                V163spositiontaking_1401 = pt.S3,
                V163mpositiontaking_1401 = pt.M3,
                V163UpdateAutoPositiontaking_1401 = pt.IsUpdateUS3,
                Isauto_v163spositiontaking_1401 = pt.IS3,
                Auto_v163spositiontaking_1401 = pt.US3,
                V163UpdateMinPositiontaking_1401 = pt.IsUpdateN3,
                V163mpositiontaking_1401_min = pt.N3,
                Updateallmembers = pt.IsUpdateAllMember,
                Newspositiontaking = pt.NewMemberPT,
                V163UpdateCommission_1401 = opusSetting.PositionTaking.IsUpdateComm3,
                V163Discount_1401 = opusSetting.PositionTaking.Comm4,
                V163UpdateMaxTransfer_1401 = betSetting.IsUpdateBetSetting,
                MaxTransferCredit = betSetting.MaxTransfer,
                IsAutoReset = betSetting.IsDailyAutoResetLimit
            };

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                Hashtable hash = db.GetOuputParams();
                errorCode = Converter.ToString2(hash["errcode"]);
                errorMsg = Converter.ToString2(hash["errmsg"]);
            }

            return MessageHelper.ParseError(errorCode, errorMsg);
        }
    }
}
