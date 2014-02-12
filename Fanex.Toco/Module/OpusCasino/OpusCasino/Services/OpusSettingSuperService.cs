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
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Fanex.Common;
    using Fanex.CustomerSetting.OpusCasino.Criteria;
    using Fanex.CustomerSetting.OpusCasino.Entities;
    using Fanex.CustomerSetting.OpusCasino.ViewModels;
    using Fanex.Data;

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
            DataSet dataSet = GetDataSetting(custId);

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                var customerTable = dataSet.Tables[0];
                var gameTypeTable = dataSet.Tables[1];

                foreach (DataRow row in customerTable.Rows)
                {
                    superOpusModel.PositionTaking = new PositionTakingSuper
                    {
                        L4 = Converter.To<double>(row["v163_spositiontaking_1401_max"]),
                        S4 = Converter.To<double>(row["v163_spositiontaking_1401"]),
                        N4 = Converter.To<double>(row["v163_spositiontaking_1401_min"]),
                        Comm4 = Converter.To<double>(row["v163_sdiscount_1401"]),
                        Comm4Max = Converter.To<double>(row["v163_sdiscount_14101_max"])
                    };

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

                    superOpusModel.BetLimitSetting = new BetSetting
                    {
                        MaxTransfer = Converter.To<double>(row["MaxTransferOpusCasino"]),
                        MaxTransferAllowed = Converter.To<double>(row["MaxTransferOpusCasino_max"]),
                        IsLimited = true,
                        IsDailyAutoResetLimit = Converter.To<bool>(row["isAutoResetOpusCasino"]),
                    };
                }

                GetGameSetting(superOpusModel, gameTypeTable);
            }

            return superOpusModel;
        }        

        /// <summary>
        /// Update Super setting.
        /// </summary>
        /// <param name="opusSetting">OpusSettingSuper object.</param>
        /// <returns>Result object.</returns>
        public Result UpdateSuper(OpusSettingSuper opusSetting)
        {
            Result result = new Result();

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
               IsAutoReset = betSetting.IsDailyAutoResetLimit,
               ////ErrCode = result.ErrorCode,
               ////ErrMsg = result.ErrorMessage
            };

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                Hashtable hash = db.GetOuputParams();
                ////result.ErrorCode = Converter.To<int>(hash["errcode"]);
                ////result.ErrorMessage = Converter.ToString2(hash["errmsg"]); 
            }

            if (opusSetting.GameSetting.IsUpdateGameSetting)
            {
                UpdateStake(opusSetting);
            }

            return result;
        }

        /// <summary>
        /// Update Stake.
        /// </summary>
        /// <param name="opusSetting">OpusSettingSuper object.</param>
        /// <returns>The list result.</returns>
        public List<Result> UpdateStake(OpusSettingSuper opusSetting)
        {
            List<Result> resultList = new List<Result>();
            CustomerInfo customerInfo = opusSetting.CustomerInformation;

            foreach (GameType gameType in opusSetting.GameSetting.GameTypeList)
            {
                if (gameType.IsUpdateGameType)
                {
                    string stakeIdList = string.Empty;
                    foreach (StakeLevel stake in gameType.StakeLevelList)
                    {
                        if (stake.IsChecked)
                        {
                            stakeIdList = stakeIdList + stake.StakeLevelId + ',';
                        }
                    }

                    UpdateStakeCriteria criteria = new UpdateStakeCriteria
                    {
                        CustId = customerInfo.CustId,
                        UserId = customerInfo.UserId,
                        GameTypeId = gameType.GameTypeID,
                        StakeLevelList = stakeIdList
                    };

                    using (var db = new ObjectDb(criteria.GetSettingKey()))
                    {
                        db.ExecuteNonQuery(criteria.GetSpParams());
                        Hashtable hash = db.GetOuputParams();
                        resultList.Add(new Result
                        {
                            ErrorCode = Converter.To<int>(hash["errcode"]),
                            ErrorMessage = Converter.ToString2(hash["errmsg"])
                        });
                    }
                }
            }

            return resultList;
        }

        #region Private methods

        /// <summary>
        /// Get Data setting.
        /// </summary>
        /// <param name="custId">Customer Id.</param>
        /// <returns>Data set object.</returns>
        private static DataSet GetDataSetting(int custId)
        {
            SelectSuperCriteria superCriteria = new SelectSuperCriteria
            {
                CustId = custId
            };

            DataSet dataSet = new DataSet();
            using (var db = new ObjectDb(superCriteria.GetSettingKey()))
            {
                var paramsOuput = superCriteria.GetSpParams();
                dataSet = db.ExecuteDataSet(paramsOuput);
            }

            return dataSet;
        }

        /// <summary>
        /// Get Game Setting.
        /// </summary>
        /// <param name="superOpusModel">SettingSuper object.</param>
        /// <param name="gameTypeTable">Data table.</param>
        private static void GetGameSetting(OpusSettingSuper superOpusModel, DataTable gameTypeTable)
        {
            List<StakeLevel> listStakeTmp = new List<StakeLevel>();
            foreach (DataRow row in gameTypeTable.Rows)
            {
                listStakeTmp.Add(new StakeLevel
                {
                    GameTypeId = Converter.To<int>(row["GameTypeID"]),
                    GameTypeName = Converter.ToString2(row["GameTypeName"]),
                    StakeLevelName = Converter.ToString2(row["StakeLevelName"]),
                    StakeLevelId = Converter.To<int>(row["StakeLevelID"]),
                    IsChecked = Converter.To<bool>(row["isChecked"])
                });
            }

            var gameTypeList = listStakeTmp.Select(item => new { item.GameTypeId, item.GameTypeName }).Distinct().ToList();

            foreach (var gameType in gameTypeList)
            {
                superOpusModel.GameSetting.GameTypeList.Add(new GameType
                {
                    GameTypeID = gameType.GameTypeId,
                    GameTypeName = gameType.GameTypeName,
                    StakeLevelList = listStakeTmp.Select(x => x).Where(y => y.GameTypeId.Equals(gameType.GameTypeId)).ToList()
                });
            }
        }

        #endregion       
    }
}
