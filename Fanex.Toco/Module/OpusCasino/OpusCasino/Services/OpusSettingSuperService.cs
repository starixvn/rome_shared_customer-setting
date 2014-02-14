// ************************************************************************************
// <copyright file="OpusSettingSuperService.cs" company="Nexcel Solutions Vietnam">
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
    public class OpusSettingSuperService : OpusSettingService, IOpusSettingSuperService
    {
        /// <summary>
        /// Get super setting.
        /// </summary>
        /// <param name="custId">Customer Id.</param>
        /// <returns>Supper Setting object.</returns>
        public OpusSettingSuper GetSuperSetting(int custId)
        {
            OpusSettingSuper superOpusModel = new OpusSettingSuper();
            SelectSuperCriteria criteria = new SelectSuperCriteria
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
                    this.MapCustomerInfo(ref superOpusModel, row, custId);
                    this.MapPositionTaking(ref superOpusModel, row);
                    this.MapBetLimitSetting(ref superOpusModel, row);
                }

                superOpusModel.GameSetting = this.GetGameSetting(gameTypeTable);
            }

            return superOpusModel;
        }

        /// <summary>
        /// Update Super setting.
        /// </summary>
        /// <param name="opusSetting">OpusSettingSuper object.</param>
        /// <returns>Result object.</returns>
        public List<Result> UpdateSuper(OpusSettingSuper opusSetting)
        {
            List<Result> resultList = new List<Result>();
            try
            {
                CustomerInfo customerInfo = opusSetting.CustomerInformation;
                PositionTakingSuper pt = opusSetting.PositionTaking;
                BetSetting betSetting = opusSetting.BetLimitSetting;
                List<Result> resultsTmp = new List<Result>();

                if (opusSetting.GameSetting.IsUpdateGameSetting)
                {
                    resultList = this.UpdateStake(opusSetting.GameSetting, opusSetting.CustomerInformation);
                }

                if (customerInfo.IsUpdateStatus || pt.IsUpdatePT || betSetting.IsUpdateBetSetting)
                {
                    resultsTmp = UpdateSuperOpusAll(opusSetting);

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

        #region Private methods
        /// <summary>
        /// Map customer information.
        /// </summary>
        /// <param name="superOpusModel">Super opus model.</param>
        /// <param name="row">DataRow row.</param>
        /// <param name="custId">Customer Id.</param>
        private void MapCustomerInfo(ref OpusSettingSuper superOpusModel, DataRow row, int custId)
        {
            superOpusModel.CustomerInformation = new CustomerInfo
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
        /// <param name="superOpusModel">Super Opus Model.</param>
        /// <param name="row">DataRow row.</param>
        private void MapPositionTaking(ref OpusSettingSuper superOpusModel, DataRow row)
        {
            superOpusModel.PositionTaking = new PositionTakingSuper
            {
                L4 = Converter.To<double>(row["v163_spositiontaking_1401_max"]),
                S4 = Converter.To<double>(row["v163_spositiontaking_1401"]),
                N4 = Converter.To<double>(row["v163_spositiontaking_1401_min"]),
                Comm4 = Converter.To<double>(row["v163_sdiscount_1401"]),
                Comm4Max = Converter.To<double>(row["v163_sdiscount_14101_max"])
            };
        }

        /// <summary>
        /// Map Bet Limit Setting.
        /// </summary>
        /// <param name="superOpusModel">Super opus model.</param>
        /// <param name="row">DataRow row.</param>
        private void MapBetLimitSetting(ref OpusSettingSuper superOpusModel, DataRow row)
        {
            superOpusModel.BetLimitSetting = new BetSetting
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
        private List<Result> UpdateSuperOpusAll(OpusSettingSuper opusSetting)
        {
            string errorCode = string.Empty;
            string errorMsg = string.Empty;
            CustomerInfo customerInfo = opusSetting.CustomerInformation;
            PositionTakingSuper pt = opusSetting.PositionTaking;
            BetSetting betSetting = opusSetting.BetLimitSetting;

            UpdateSuperCriteria criteria = new UpdateSuperCriteria
            {
                CustId = customerInfo.CustId,
                UserId = customerInfo.UserId,
                SubAccId = customerInfo.SubAccId,
                Ip = customerInfo.Ip,
                Language = customerInfo.Language,
                V163UpdateCustomerStatus_1401 = customerInfo.IsUpdateStatus,
                Disable = customerInfo.IsDisable,
                DisableDownLines = customerInfo.IsDisableDownlines,
                V163UpdatePositionTaking_1401 = opusSetting.PositionTaking.IsUpdateS4,
                V163SPositionTaking_1401 = opusSetting.PositionTaking.S4,
                V163UpdateMinPositionTaking_1401 = opusSetting.PositionTaking.IsUpdateN4,
                V163SPositionTaking_1401_Min = opusSetting.PositionTaking.N4,
                V163UpdateCommission_1401 = opusSetting.PositionTaking.IsUpdateCom4,
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
        #endregion
    }
}
