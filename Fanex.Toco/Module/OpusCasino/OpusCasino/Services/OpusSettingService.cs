// ************************************************************************************
// <copyright file="OpusSettingService.cs" company="Nexcel Solutions Vietnam">
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
    using Fanex.CustomerSetting.OpusCasino.ViewModels;
    using Fanex.Data;
    using Fanex.OpusCasino;

    /// <summary>
    /// OpusSettingService Class.
    /// </summary>
    public abstract class OpusSettingService : IOpusSettingService
    {
        /// <summary>
        /// Get Data setting.
        /// </summary>
        /// <param name="criteria">Criteria to get data.</param>
        /// <returns>Data set object.</returns>
        public DataSet GetDataSetting(dynamic criteria)
        {
            DataSet dataSet = new DataSet();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                var paramsOuput = criteria.GetSpParams();
                dataSet = db.ExecuteDataSet(paramsOuput);
            }

            return dataSet;
        }

        /// <summary>
        /// Get Game Setting.
        /// </summary>
        /// <param name="gameTypeTable">Data table.</param>
        /// <returns>GameSetting object.</returns>
        public GameSetting GetGameSetting(DataTable gameTypeTable)
        {
            GameSetting gameSetting = new GameSetting();
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
                gameSetting.GameTypeList.Add(new GameType
                {
                    GameTypeID = gameType.GameTypeId,
                    GameTypeName = gameType.GameTypeName,
                    StakeLevelList = listStakeTmp.Select(x => x).Where(y => y.GameTypeId.Equals(gameType.GameTypeId)).ToList()
                });
            }

            return gameSetting;
        }

        /// <summary>
        /// Update Stake.
        /// </summary>
        /// <param name="gameSetting">GameSetting object.</param>
        /// <param name="customerInfo">CustomerInfo object.</param>
        /// <returns>The list result.</returns>
        public List<Result> UpdateStake(GameSetting gameSetting, CustomerInfo customerInfo)
        {
            List<Result> resultList = new List<Result>();

            foreach (GameType gameType in gameSetting.GameTypeList)
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

        ////public OpusSetting SelectCustomerInfo(int custId, int uplineRole, int downlineRole, int actionId)
        ////{            
        ////    OpusSetting opusModel = new OpusSetting();
        ////    string whatActionName = uplineRole.ToString() + "_" + actionId.ToString() + "_" + downlineRole.ToString();
        ////    switch (whatActionName)
        ////    {
        ////        case "0_0_4":
        ////            opusModel = GetSuperSetting(custId);
        ////            break;
        ////        case "4_0_3":
        ////            opusModel = GetMasterSetting(custId);
        ////            break;
        ////        case "3_0_2":
        ////            opusModel = GetAgentSetting(custId);
        ////            break;
        ////        case "2_0_1":
        ////            opusModel = GetMemberSetting(custId);
        ////            break;
        ////    }

        ////    return opusModel;
        ////}

        ////private OpusSetting GetMasterSetting(int custId)
        ////{
        ////    OpusSetting masterOpusModel = new OpusSetting();

        ////    SelectMasterCriteria masterCriteria = new SelectMasterCriteria
        ////    {
        ////        CustId = custId
        ////    };

        ////    DataSet dataSet = new DataSet();
        ////    using (var db = new ObjectDb(masterCriteria.GetSettingKey()))
        ////    {
        ////        var paramsOuput = masterCriteria.GetSpParams();
        ////        dataSet = db.ExecuteDataSet(paramsOuput);
        ////    }

        ////    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
        ////    {
        ////        var customerTable = dataSet.Tables[0];
        ////        var gameTypeTable = dataSet.Tables[1];

        ////        foreach (DataRow row in customerTable.Rows)
        ////        {
        ////            masterOpusModel.PositionTaking = new PositionTaking
        ////            {
        ////                S4 = Converter.To<double>(row["v163spositiontaking_1401_max"]),
        ////                S3 = Converter.To<double>(row["v163spositiontaking_1401"]),
        ////                US3 = Converter.To<double>(row["auto_v163spositiontaking_1401"]),
        ////                IS3 = Converter.To<bool>(row["isauto_v163spositiontaking_1401"]),
        ////                L3 = Converter.To<double>(row["v163mpositiontaking_1401"]),
        ////                N3 = Converter.To<double>(row["v163spositiontaking_1401_min"]),
        ////                Comm4 = Converter.To<double>(row["v163dicount_1401_max"]),
        ////                Comm3 = Converter.To<double>(row["v163discount_1401"]),
        ////            };

        ////            masterOpusModel.CustomerInformation = new CustomerInfo
        ////            {
        ////                CustId = custId,
        ////                RoleId = 4,
        ////                Username = Converter.ToString2(row["username"]),
        ////                NickName = Converter.ToString2(row["username2"]),
        ////                FirstName = Converter.ToString2(row["firstname"]),
        ////                LastName = Converter.ToString2(row["lastname"]),
        ////                Phone = Converter.ToString2(row["phone"]),
        ////                MobilePhone = Converter.ToString2(row["mobilephone"]),
        ////                Fax = Converter.ToString2(row["fax"]),
        ////                IsDisable = Converter.ToBool(row["DisableOpusCasino"]),
        ////                IsDisableDownlines = Converter.ToBool(row["DisableOpusCasinoDownlines"])
        ////            };

        ////            masterOpusModel.BetLimitSetting = new BetSetting
        ////            {
        ////                MaxTransfer = Converter.To<double>(row["MaxTransferOpusCasino"]),
        ////                MaxTransferAllowed = Converter.To<double>(row["MaxTransferOpusCasino_max"]),
        ////                IsLimited = true,
        ////                IsDailyAutoResetLimit = Converter.To<bool>(row["isAutoResetOpusCasino"]),
        ////            };
        ////        }

        ////        List<StakeLevel> listStakeTmp = new List<StakeLevel>();
        ////        foreach (DataRow row in gameTypeTable.Rows)
        ////        {
        ////            listStakeTmp.Add(new StakeLevel
        ////            {
        ////                GameTypeId = Converter.To<int>(row["GameTypeID"]),
        ////                GameTypeName = Converter.ToString2(row["GameTypeName"]),
        ////                StakeLevelName = Converter.ToString2(row["StakeLevelName"]),
        ////                IsChecked = Converter.To<bool>(row["isChecked"])
        ////            });
        ////        }

        ////        var gameTypeList = listStakeTmp.Select(item => new { item.GameTypeId, item.GameTypeName }).Distinct().ToList();

        ////        foreach (var gameType in gameTypeList)
        ////        {
        ////            masterOpusModel.GameSetting.GameTypeList.Add(new GameType
        ////            {
        ////                GameTypeID = gameType.GameTypeId,
        ////                GameTypeName = gameType.GameTypeName,
        ////                StakeLevelList = listStakeTmp.Select(x => x).Where(y => y.GameTypeId.Equals(gameType.GameTypeId)).ToList()
        ////            });
        ////        }
        ////    }

        ////    return masterOpusModel;
        ////}

        ////private OpusSetting GetAgentSetting(int custId)
        ////{
        ////    OpusSetting agentOpusModel = new OpusSetting();

        ////    SelectAgentCriteria agentCriteria = new SelectAgentCriteria
        ////    {
        ////        CustId = custId
        ////    };

        ////    DataSet dataSet = new DataSet();
        ////    using (var db = new ObjectDb(agentCriteria.GetSettingKey()))
        ////    {
        ////        var paramsOuput = agentCriteria.GetSpParams();
        ////        dataSet = db.ExecuteDataSet(paramsOuput);
        ////    }

        ////    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
        ////    {
        ////        var customerTable = dataSet.Tables[0];
        ////        var gameTypeTable = dataSet.Tables[1];

        ////        foreach (DataRow row in customerTable.Rows)
        ////        {
        ////            agentOpusModel.PositionTaking = new PositionTaking
        ////            {
        ////                M3 = Converter.To<double>(row["v163_mpositiontaking_1401_max"]),
        ////                M2 = Converter.To<double>(row["v163mpositiontaking_1401"]),
        ////                L2 = Converter.To<double>(row["v163apositiontaking_1401"]),
        ////                N2 = Converter.To<double>(row["v163apositiontaking_1401_min"]),
        ////                UM2 = Converter.To<double>(row["auto_v163mpositiontaking_1401"]),
        ////                IM1 = Converter.To<bool>(row["isauto_v163mpositiontaking_1401"]),
        ////                Comm3 = Converter.To<double>(row["v163discount_1401_max"]),
        ////                Comm2 = Converter.To<double>(row["v163discount_1401"]),
        ////            };

        ////            agentOpusModel.CustomerInformation = new CustomerInfo
        ////            {
        ////                CustId = custId,
        ////                RoleId = 4,
        ////                Username = Converter.ToString2(row["username"]),
        ////                NickName = Converter.ToString2(row["username2"]),
        ////                FirstName = Converter.ToString2(row["firstname"]),
        ////                LastName = Converter.ToString2(row["lastname"]),
        ////                Phone = Converter.ToString2(row["phone"]),
        ////                MobilePhone = Converter.ToString2(row["mobilephone"]),
        ////                Fax = Converter.ToString2(row["fax"]),
        ////                IsDisable = Converter.ToBool(row["DisableOpusCasino"]),
        ////                IsDisableDownlines = Converter.ToBool(row["DisableDownlinesCSOpus"])
        ////            };

        ////            agentOpusModel.BetLimitSetting = new BetSetting
        ////            {
        ////                MaxTransfer = Converter.To<double>(row["MaxTransferOpusCasino"]),
        ////                MaxTransferAllowed = Converter.To<double>(row["MaxTransferOpusCasino_max"]),
        ////                IsLimited = true,
        ////                IsDailyAutoResetLimit = Converter.To<bool>(row["isAutoResetOpusCasino"])
        ////            };
        ////        }

        ////        List<StakeLevel> listStakeTmp = new List<StakeLevel>();
        ////        foreach (DataRow row in gameTypeTable.Rows)
        ////        {
        ////            listStakeTmp.Add(new StakeLevel
        ////            {
        ////                GameTypeId = Converter.To<int>(row["GameTypeID"]),
        ////                GameTypeName = Converter.ToString2(row["GameTypeName"]),
        ////                StakeLevelName = Converter.ToString2(row["StakeLevelName"]),
        ////                IsChecked = Converter.To<bool>(row["isChecked"])
        ////            });
        ////        }

        ////        var gameTypeList = listStakeTmp.Select(item => new { item.GameTypeId, item.GameTypeName }).Distinct().ToList();

        ////        foreach (var gameType in gameTypeList)
        ////        {
        ////            agentOpusModel.GameSetting.GameTypeList.Add(new GameType
        ////            {
        ////                GameTypeID = gameType.GameTypeId,
        ////                GameTypeName = gameType.GameTypeName,
        ////                StakeLevelList = listStakeTmp.Select(x => x).Where(y => y.GameTypeId.Equals(gameType.GameTypeId)).ToList()
        ////            });
        ////        }
        ////    }

        ////    return agentOpusModel;
        ////}

        ////private OpusSetting GetMemberSetting(int custId)
        ////{
        ////    OpusSetting memberOpusModel = new OpusSetting();

        ////    SelectMasterCriteria masterCriteria = new SelectMasterCriteria
        ////    {
        ////        CustId = custId
        ////    };

        ////    DataSet dataSet = new DataSet();
        ////    using (var db = new ObjectDb(masterCriteria.GetSettingKey()))
        ////    {
        ////        var paramsOuput = masterCriteria.GetSpParams();
        ////        dataSet = db.ExecuteDataSet(paramsOuput);
        ////    }

        ////    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
        ////    {
        ////        var customerTable = dataSet.Tables[0];
        ////        var gameTypeTable = dataSet.Tables[1];

        ////        foreach (DataRow row in customerTable.Rows)
        ////        {
        ////            memberOpusModel.PositionTaking = new PositionTaking
        ////            {
        ////                S4 = Converter.To<double>(row["v163spositiontaking_1401_max"]),
        ////                S3 = Converter.To<double>(row["v163spositiontaking_1401"]),
        ////                US3 = Converter.To<double>(row["auto_v163spositiontaking_1401"]),
        ////                IS3 = Converter.To<bool>(row["isauto_v163spositiontaking_1401"]),
        ////                L3 = Converter.To<double>(row["v163mpositiontaking_1401"]),
        ////                N3 = Converter.To<double>(row["v163spositiontaking_1401_min"]),
        ////                Comm4 = Converter.To<double>(row["v163dicount_1401_max"]),
        ////                Comm3 = Converter.To<double>(row["v163discount_1401"]),
        ////            };

        ////            memberOpusModel.CustomerInformation = new CustomerInfo
        ////            {
        ////                CustId = custId,
        ////                RoleId = 4,
        ////                Username = Converter.ToString2(row["username"]),
        ////                NickName = Converter.ToString2(row["username2"]),
        ////                FirstName = Converter.ToString2(row["firstname"]),
        ////                LastName = Converter.ToString2(row["lastname"]),
        ////                Phone = Converter.ToString2(row["phone"]),
        ////                MobilePhone = Converter.ToString2(row["mobilephone"]),
        ////                Fax = Converter.ToString2(row["fax"]),
        ////                IsDisable = Converter.ToBool(row["DisableOpusCasino"]),
        ////                IsDisableDownlines = Converter.ToBool(row["DisableOpusCasinoDownlines"])
        ////            };

        ////            memberOpusModel.BetLimitSetting = new BetSetting
        ////            {
        ////                MaxTransfer = Converter.To<double>(row["MaxTransferOpusCasino"]),
        ////                MaxTransferAllowed = Converter.To<double>(row["MaxTransferOpusCasino_max"]),
        ////                IsLimited = true,
        ////                IsDailyAutoResetLimit = Converter.To<bool>(row["isAutoResetOpusCasino"]),
        ////            };
        ////        }

        ////        List<StakeLevel> listStakeTmp = new List<StakeLevel>();
        ////        foreach (DataRow row in gameTypeTable.Rows)
        ////        {
        ////            listStakeTmp.Add(new StakeLevel
        ////            {
        ////                GameTypeId = Converter.To<int>(row["GameTypeID"]),
        ////                GameTypeName = Converter.ToString2(row["GameTypeName"]),
        ////                StakeLevelName = Converter.ToString2(row["StakeLevelName"]),
        ////                IsChecked = Converter.To<bool>(row["isChecked"])
        ////            });
        ////        }

        ////        var gameTypeList = listStakeTmp.Select(item => new { item.GameTypeId, item.GameTypeName }).Distinct().ToList();

        ////        foreach (var gameType in gameTypeList)
        ////        {
        ////            memberOpusModel.GameSetting.GameTypeList.Add(new GameType
        ////            {
        ////                GameTypeID = gameType.GameTypeId,
        ////                GameTypeName = gameType.GameTypeName,
        ////                StakeLevelList = listStakeTmp.Select(x => x).Where(y => y.GameTypeId.Equals(gameType.GameTypeId)).ToList()
        ////            });
        ////        }
        ////    }

        ////    return memberOpusModel;
        ////}

        ////private OpusSetting GetSuperOnMemberSetting(int custId)
        ////{
        ////    OpusSetting memberOpusModel = new OpusSetting();

        ////    memberOpusModel.PositionTaking = new PositionTaking
        ////    {
        ////        A2 = 0.4,
        ////        Comm2 = 0.003
        ////    };

        ////    return memberOpusModel;
        ////}

        ////public OpusSetting UpdateOpusSetting(OpusSetting opusSetting, int uplineRole,
        ////int downlineRole, int actionId, int userId, int subAccId, string ip, string language )
        ////{
        ////    throw new System.NotImplementedException();
        ////}
    }
}
