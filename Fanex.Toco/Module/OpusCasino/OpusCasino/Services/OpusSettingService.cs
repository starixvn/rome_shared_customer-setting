// -----------------------------------------------------------------------
// <copyright file="OpusSettingService.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------


namespace Fanex.CustomerSetting.Module.Racebook.Services
{
    using System.Collections;
    using Fanex.CustomerSetting.OpusCasino.ViewModels;
    using Fanex.CustomerSetting.OpusCasino.Criteria;
    using System.Data;
    using Fanex.Data;
    using Fanex.Common;
    using System.Linq;
    using System.Collections.Generic;
    using Fanex.CustomerSetting.OpusCasino.Entities;
    using Fanex.CustomerSetting.Module.Core.Repository;

    public class OpusSettingService : IOpusSettingService
    {
        public OpusSetting SelectCustomerInfo(int custId, int uplineRole, int downlineRole, int actionId)
        {
            OpusSetting opusModel = new OpusSetting();
            string whatActionName = uplineRole.ToString() + "_" + actionId.ToString() + "_" + downlineRole.ToString();
            switch (whatActionName)
            {
                case "0_0_4":
                    opusModel = GetSuperSetting(custId);
                    break;
                case "4_0_3":
                    opusModel = GetMasterSetting(custId);
                    break;
                case "3_0_2":
                    opusModel = GetAgentSetting(custId);
                    break;
                case "2_0_1":
                    opusModel = GetMemberSetting(custId);
                    break;
            }

            return opusModel;
        }

        private OpusSetting GetSuperSetting(int custId)
        {
            OpusSetting superOpusModel = new OpusSetting();
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

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                var customerTable = dataSet.Tables[0];
                var gameTypeTable = dataSet.Tables[1];

                foreach (DataRow row in customerTable.Rows)
                {
                    superOpusModel.PositionTaking = new PositionTaking
                    {
                        S4Max = Converter.To<double>(row["v163_spositiontaking_1401_max"]),
                        L4 = Converter.To<double>(row["v163_spositiontaking_1401"]),
                        N4 = Converter.To<double>(row["v163_spositiontaking_1401_min"]),
                        Comm4 = Converter.To<double>(row["v163_sdiscount_1401"]),
                        Comm4Max = Converter.To<double>(row["v163_sdiscount_14101_max"]),
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

                List<StakeLevel> listStakeTmp = new List<StakeLevel>();
                foreach (DataRow row in gameTypeTable.Rows)
                {
                    listStakeTmp.Add(new StakeLevel
                    {
                        GameTypeId = Converter.To<int>(row["GameTypeID"]),
                        GameTypeName = Converter.ToString2(row["GameTypeName"]),
                        StakeLevelName = Converter.ToString2(row["StakeLevelName"]),
                        IsChecked = Converter.To<bool>(row["isChecked"])
                    });
                }


                var gameTypeList = listStakeTmp.Select(item => new { item.GameTypeId, item.GameTypeName }).Distinct().ToList();

                foreach (var gameType in gameTypeList)
                {
                    superOpusModel.GameSettingList.Add(new GameSetting
                    {
                        GameTypeID = gameType.GameTypeId,
                        GameTypeName = gameType.GameTypeName,
                        BettingLimitList = listStakeTmp.Select(x => x).Where(y => y.GameTypeId.Equals(gameType.GameTypeId)).ToList()
                    });
                }
            }
            return superOpusModel;
        }

        private OpusSetting GetMasterSetting(int custId)
        {
            OpusSetting masterOpusModel = new OpusSetting();

            SelectMasterCriteria masterCriteria = new SelectMasterCriteria
            {
                CustId = custId
            };

            DataSet dataSet = new DataSet();
            using (var db = new ObjectDb(masterCriteria.GetSettingKey()))
            {
                var paramsOuput = masterCriteria.GetSpParams();
                dataSet = db.ExecuteDataSet(paramsOuput);
            }

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                var customerTable = dataSet.Tables[0];
                var gameTypeTable = dataSet.Tables[1];

                foreach (DataRow row in customerTable.Rows)
                {
                    masterOpusModel.PositionTaking = new PositionTaking
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

                    masterOpusModel.BetLimitSetting = new BetSetting
                    {
                        MaxTransfer = Converter.To<double>(row["MaxTransferOpusCasino"]),
                        MaxTransferAllowed = Converter.To<double>(row["MaxTransferOpusCasino_max"]),
                        IsLimited = true,
                        IsDailyAutoResetLimit = Converter.To<bool>(row["isAutoResetOpusCasino"]),
                    };
                }

                List<StakeLevel> listStakeTmp = new List<StakeLevel>();
                foreach (DataRow row in gameTypeTable.Rows)
                {
                    listStakeTmp.Add(new StakeLevel
                    {
                        GameTypeId = Converter.To<int>(row["GameTypeID"]),
                        GameTypeName = Converter.ToString2(row["GameTypeName"]),
                        StakeLevelName = Converter.ToString2(row["StakeLevelName"]),
                        IsChecked = Converter.To<bool>(row["isChecked"])
                    });
                }


                var gameTypeList = listStakeTmp.Select(item => new { item.GameTypeId, item.GameTypeName }).Distinct().ToList();

                foreach (var gameType in gameTypeList)
                {
                    masterOpusModel.GameSettingList.Add(new GameSetting
                    {
                        GameTypeID = gameType.GameTypeId,
                        GameTypeName = gameType.GameTypeName,
                        BettingLimitList = listStakeTmp.Select(x => x).Where(y => y.GameTypeId.Equals(gameType.GameTypeId)).ToList()
                    });
                }
            }

            return masterOpusModel;
        }

        private OpusSetting GetAgentSetting(int custId)
        {
            OpusSetting agentOpusModel = new OpusSetting();

            SelectAgentCriteria agentCriteria = new SelectAgentCriteria
            {
                CustId = custId
            };

            DataSet dataSet = new DataSet();
            using (var db = new ObjectDb(agentCriteria.GetSettingKey()))
            {
                var paramsOuput = agentCriteria.GetSpParams();
                dataSet = db.ExecuteDataSet(paramsOuput);
            }

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                var customerTable = dataSet.Tables[0];
                var gameTypeTable = dataSet.Tables[1];

                foreach (DataRow row in customerTable.Rows)
                {
                    agentOpusModel.PositionTaking = new PositionTaking
                    {
                        M3 = Converter.To<double>(row["v163_mpositiontaking_1401_max"]),
                        M2 = Converter.To<double>(row["v163mpositiontaking_1401"]),
                        L2 = Converter.To<double>(row["v163apositiontaking_1401"]),
                        N2 = Converter.To<double>(row["v163apositiontaking_1401_min"]),
                        UM2 = Converter.To<double>(row["auto_v163mpositiontaking_1401"]),
                        IM1 = Converter.To<bool>(row["isauto_v163mpositiontaking_1401"]),
                        Comm3 = Converter.To<double>(row["v163discount_1401_max"]),
                        Comm2 = Converter.To<double>(row["v163discount_1401"]),
                    };

                    agentOpusModel.CustomerInformation = new CustomerInfo
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
                        IsDisableDownlines = Converter.ToBool(row["DisableDownlinesCSOpus"])
                    };

                    agentOpusModel.BetLimitSetting = new BetSetting
                    {
                        MaxTransfer = Converter.To<double>(row["MaxTransferOpusCasino"]),
                        MaxTransferAllowed = Converter.To<double>(row["MaxTransferOpusCasino_max"]),
                        IsLimited = true,
                        IsDailyAutoResetLimit = Converter.To<bool>(row["isAutoResetOpusCasino"])
                    };
                }

                List<StakeLevel> listStakeTmp = new List<StakeLevel>();
                foreach (DataRow row in gameTypeTable.Rows)
                {
                    listStakeTmp.Add(new StakeLevel
                    {
                        GameTypeId = Converter.To<int>(row["GameTypeID"]),
                        GameTypeName = Converter.ToString2(row["GameTypeName"]),
                        StakeLevelName = Converter.ToString2(row["StakeLevelName"]),
                        IsChecked = Converter.To<bool>(row["isChecked"])
                    });
                }


                var gameTypeList = listStakeTmp.Select(item => new { item.GameTypeId, item.GameTypeName }).Distinct().ToList();

                foreach (var gameType in gameTypeList)
                {
                    agentOpusModel.GameSettingList.Add(new GameSetting
                    {
                        GameTypeID = gameType.GameTypeId,
                        GameTypeName = gameType.GameTypeName,
                        BettingLimitList = listStakeTmp.Select(x => x).Where(y => y.GameTypeId.Equals(gameType.GameTypeId)).ToList()
                    });
                }
            }

            return agentOpusModel;
        }

        private OpusSetting GetMemberSetting(int custId)
        {
            OpusSetting memberOpusModel = new OpusSetting();

            SelectMasterCriteria masterCriteria = new SelectMasterCriteria
            {
                CustId = custId
            };

            DataSet dataSet = new DataSet();
            using (var db = new ObjectDb(masterCriteria.GetSettingKey()))
            {
                var paramsOuput = masterCriteria.GetSpParams();
                dataSet = db.ExecuteDataSet(paramsOuput);
            }

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                var customerTable = dataSet.Tables[0];
                var gameTypeTable = dataSet.Tables[1];

                foreach (DataRow row in customerTable.Rows)
                {
                    memberOpusModel.PositionTaking = new PositionTaking
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

                    memberOpusModel.CustomerInformation = new CustomerInfo
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

                    memberOpusModel.BetLimitSetting = new BetSetting
                    {
                        MaxTransfer = Converter.To<double>(row["MaxTransferOpusCasino"]),
                        MaxTransferAllowed = Converter.To<double>(row["MaxTransferOpusCasino_max"]),
                        IsLimited = true,
                        IsDailyAutoResetLimit = Converter.To<bool>(row["isAutoResetOpusCasino"]),
                    };
                }

                List<StakeLevel> listStakeTmp = new List<StakeLevel>();
                foreach (DataRow row in gameTypeTable.Rows)
                {
                    listStakeTmp.Add(new StakeLevel
                    {
                        GameTypeId = Converter.To<int>(row["GameTypeID"]),
                        GameTypeName = Converter.ToString2(row["GameTypeName"]),
                        StakeLevelName = Converter.ToString2(row["StakeLevelName"]),
                        IsChecked = Converter.To<bool>(row["isChecked"])
                    });
                }


                var gameTypeList = listStakeTmp.Select(item => new { item.GameTypeId, item.GameTypeName }).Distinct().ToList();

                foreach (var gameType in gameTypeList)
                {
                    memberOpusModel.GameSettingList.Add(new GameSetting
                    {
                        GameTypeID = gameType.GameTypeId,
                        GameTypeName = gameType.GameTypeName,
                        BettingLimitList = listStakeTmp.Select(x => x).Where(y => y.GameTypeId.Equals(gameType.GameTypeId)).ToList()
                    });
                }
            }

            return memberOpusModel;
        }

        //private OpusSetting GetSuperOnMemberSetting(int custId)
        //{
        //    OpusSetting memberOpusModel = new OpusSetting();

        //    memberOpusModel.PositionTaking = new PositionTaking
        //    {
        //        A2 = 0.4,
        //        Comm2 = 0.003
        //    };

        //    return memberOpusModel;
        //}


        public OpusSetting UpdateOpusSetting(OpusSetting opusSetting, int uplineRole, int downlineRole, int actionId, int userId, int subAccId, string ip, string language )
        {
            throw new System.NotImplementedException();
        }

        public Result UpdateSuper(OpusSetting opusSetting)
        {
            Result result = new Result();

            CustomerInfo customerInfo = opusSetting.CustomerInformation;
            PositionTaking pt = opusSetting.PositionTaking;
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

                V163UpdatePositionTaking_1401 = opusSetting.PositionTaking.IsUpdateL4,
                V163SPositionTaking_1401 = opusSetting.PositionTaking.L4,

                V163UpdateMinPositionTaking_1401 = opusSetting.PositionTaking.IsUpdateN4,
                V163SPositionTaking_1401_Min = opusSetting.PositionTaking.N4,

                V163UpdateCommission_1401 = opusSetting.PositionTaking.IsUpdateCom4,
                V163Discount_1401 = opusSetting.PositionTaking.Comm4,

               V163UpdateMaxTransfer_1401 = betSetting.IsUpdateBetSetting,
               MaxTransferCredit = betSetting.MaxTransfer,
               IsAutoReset = betSetting.IsDailyAutoResetLimit,
               //ErrCode = result.ErrorCode,
               //ErrMsg = result.ErrorMessage
            };

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                Hashtable hash = db.GetOuputParams();
                result.ErrorCode = Converter.To<int>(hash["errcode"]);
                result.ErrorMessage = Converter.ToString2(hash["errmsg"]); 
            }

            return result;
        }
    }
}
