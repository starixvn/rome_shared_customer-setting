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
    using System.Linq;
    using Fanex.Common;
    using Fanex.CustomerSetting.OpusCasino.Criteria;
    using Fanex.CustomerSetting.OpusCasino.ViewModels;
    using Fanex.Data;

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
                        L3 = Converter.To<double>(row["v163mpositiontaking_1401"]),
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
    }
}
