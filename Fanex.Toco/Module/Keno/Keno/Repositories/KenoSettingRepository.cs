// --------------------------------------------------------------------------
// <copyright file="KenoSettingRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Repositories
{
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Common.Extensions;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.Helpers;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.NAP.Framework.Exceptions;

    /// <summary>
    /// KenoSettingRepository Class.
    /// </summary>
    public class KenoSettingRepository : KenoSettingBaseRepository, IKenoSettingRepository, IKenoSettingBaseRepository
    {
        /// <summary>
        /// Gets the super setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The SuperSetting object.</returns>
        public SuperSettingViewModel GetSuperSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var superSetting = new SuperSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    superSetting.CustomerInformation = ReadCustomerInformationData(reader, Roles.Super);
                    superSetting.BetSettings = reader.NextResult() ? ReadBetLimitData(reader) : null;
                    superSetting.PositionTakings = reader.NextResult() ? ReadSuperPositionTakingData(reader) : null;
                }
            }

            return superSetting;
        }

        /// <summary>
        /// Gets the master setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The MasterSetting object.</returns>
        public MasterSettingViewModel GetMasterSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var masterSetting = new MasterSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    masterSetting.CustomerInformation = ReadCustomerInformationData(reader, Roles.Master);
                    masterSetting.BetSettings = reader.NextResult() ? ReadBetLimitData(reader) : null;
                    masterSetting.PositionTakings = reader.NextResult() ? ReadMasterPositionTakingData(reader) : null;
                }
            }

            return masterSetting;
        }

        /// <summary>
        /// Gets the agent setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The AgentSetting object.</returns>
        public AgentSettingViewModel GetAgentSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var agentSetting = new AgentSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    agentSetting.CustomerInformation = ReadCustomerInformationData(reader, Roles.Agent);
                    agentSetting.BetSettings = reader.NextResult() ? ReadBetLimitData(reader) : null;
                    agentSetting.PositionTakings = reader.NextResult() ? ReadAgentPositionTakingData(reader) : null;
                }
            }

            return agentSetting;
        }

        /// <summary>
        /// Gets the member setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The MemberSetting object.</returns>
        public AgentEditMemberSettingViewModel GetSettingInCaseOfAgentEditMember(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var memberSetting = new AgentEditMemberSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    memberSetting.CustomerInformation = ReadCustomerInformationData(reader, Roles.Member);
                    memberSetting.BetSettings = reader.NextResult() ? ReadBetLimitData(reader) : null;
                    memberSetting.PositionTakings = reader.NextResult() ? ReadMemberPositionTakingInCaseOfAgentEditMember(reader) : null;
                }
            }

            return memberSetting;
        }

        /// <summary>
        /// Gets the member setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The MemberSetting object.</returns>
        public SuperEditMemberSettingViewModel GetSettingInCaseOfSuperEditMember(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var memberSetting = new SuperEditMemberSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    memberSetting.CustomerInformation = ReadCustomerInformationData(reader, Roles.Member);
                    memberSetting.BetSettings = reader.NextResult() ? ReadBetLimitData(reader) : null;
                    memberSetting.PositionTakings = reader.NextResult() ? ReadMemberPositionTakingInCaseOfSuperEditMember(reader) : null;
                }
            }

            return memberSetting;
        }

        /// <summary>
        /// Gets the member setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The MemberSetting object.</returns>
        public MasterEditMemberSettingViewModel GetSettingInCaseOfMasterEditMember(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var memberSetting = new MasterEditMemberSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    memberSetting.CustomerInformation = ReadCustomerInformationData(reader, Roles.Member);
                    memberSetting.BetSettings = reader.NextResult() ? ReadBetLimitData(reader) : null;
                    memberSetting.PositionTakings = reader.NextResult() ? ReadMemberPositionTakingInCaseOfMasterEditMember(reader) : null;
                }
            }

            return memberSetting;
        }

        public IList<Result> UpdateBetSetting(ICriteria criteria)
        {
            return this.UpdateSetting(criteria);
        }

        public IList<Result> UpdatePositionTaking(ICriteria criteria)
        {
            return this.UpdateSetting(criteria);
        }

        /// <summary>
        /// Reads the agent information data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="role">The role of target user.</param>
        /// <returns>
        /// The CustomerInformation object.
        /// </returns>
        internal static CustomerInformation ReadCustomerInformationData(IDataReader dataReader, Roles role)
        {
            CustomerInformation customerInformation = new CustomerInformation();
            while (dataReader.Read())
            {
                customerInformation = new CustomerInformation();
                customerInformation.CustId = dataReader.HasColumn("custid") ? dataReader.ToInt("custid") : 0;
                customerInformation.Username = dataReader.HasColumn("username") ? dataReader.ToString("username") : string.Empty;
                customerInformation.FirstName = dataReader.ToString("firstname");
                customerInformation.LastName = dataReader.ToString("lastname");
                customerInformation.Phone = dataReader.ToString("phone");
                customerInformation.MobilePhone = dataReader.ToString("mobilephone");
                customerInformation.Fax = dataReader.ToString("fax");
                customerInformation.RoleId = (int)role;
            }

            return customerInformation;
        }

        /// <summary>
        /// Reads the bet limit data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of BetSetting.</returns>
        internal static IList<BetSetting> ReadBetLimitData(IDataReader dataReader)
        {
            var betSettings = new List<BetSetting>();
            while (dataReader.Read())
            {
                var bettingSetting = new BetSetting
                {
                    BetTypeId = dataReader.HasColumn("IBCBetTypeID") ? dataReader.ToInt("IBCBetTypeID") : 0,
                    MinBet = dataReader.HasColumn("MinBet") ? dataReader.ToDecimal("MinBet") : 0,
                    MaxBet = dataReader.HasColumn("MaxBet") ? dataReader.ToDecimal("MaxBet") : 0,
                    MaxPerMatch = dataReader.HasColumn("MaxPerGameType") ? dataReader.ToDecimal("MaxPerGameType") : 0,
                    IsEnabled = dataReader.HasColumn("IsDisabled") ? !dataReader.ToBool("IsDisabled") : false,
                    IsDownlinesEnabled = dataReader.HasColumn("DisableDownlinesKeno") ? !dataReader.ToBool("DisableDownlinesKeno") : false,
                    Upline = new BetSetting()
                    {
                        MinBet = dataReader.HasColumn("MinBet_Min") ? dataReader.ToDecimal("MinBet_Min") : 0,
                        MaxBet = dataReader.HasColumn("MaxBet_Max") ? dataReader.ToDecimal("MaxBet_Max") : 0,
                        MaxPerMatch = dataReader.HasColumn("MaxPerMatch_Max") ? dataReader.ToDecimal("MaxPerMatch_Max") : 0
                    }
                };

                betSettings.Add(bettingSetting);
            }

            return betSettings;
        }

        /// <summary>
        /// Reads the super position taking data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of SuperPositionTaking.</returns>
        internal static IList<dynamic> ReadSuperPositionTakingData(IDataReader dataReader)
        {
            var positionTakings = new List<dynamic>();
            while (dataReader.Read())
            {
                var bettingSetting = new SuperPositionTaking
                {
                    BetTypeId = dataReader.ToInt("BetType"),
                    S4 = dataReader.ToDouble("spositiontaking"),
                    N4 = dataReader.ToDouble("minpositiontaking"),
                    L4 = dataReader.ToDouble("MaxPositiontaking"),
                    SComm = dataReader.ToDouble("sdiscount"),
                    SCommMax = dataReader.ToDouble("maxsdiscount")
                };

                positionTakings.Add(bettingSetting);
            }

            return positionTakings;
        }

        /// <summary>
        /// Reads the master position taking data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of MasterPositionTaking.</returns>
        internal static IList<dynamic> ReadMasterPositionTakingData(IDataReader dataReader)
        {
            var positionTakings = new List<dynamic>();
            while (dataReader.Read())
            {
                var bettingSetting = new MasterPositionTaking
                {
                    BetTypeId = dataReader.ToInt("IBCBetTypeID"),
                    S4 = dataReader.ToDouble("spositiontaking_max"),
                    S3 = dataReader.ToDouble("spositiontaking"),
                    M3 = dataReader.ToDouble("mpositiontaking"),
                    N3 = dataReader.ToDouble("minpositiontaking"),
                    IS3 = dataReader.ToBool("isautopositiontaking"),
                    US3 = dataReader.ToDouble("autopositiontaking"),
                    MComm = dataReader.ToDouble("adiscount"),
                    SComm = dataReader.ToDouble("discount_max")
                };

                positionTakings.Add(bettingSetting);
            }

            return positionTakings;
        }

        /// <summary>
        /// Reads the agent position taking data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of MasterPositionTaking.</returns>
        internal static IList<dynamic> ReadAgentPositionTakingData(IDataReader dataReader)
        {
            var positionTakings = new List<dynamic>();
            while (dataReader.Read())
            {
                var bettingSetting = new AgentPositionTaking
                {
                    BetTypeId = dataReader.ToInt("BetType"),
                    IM2 = dataReader.ToBool("isauto_mpositiontaking"),
                    M2 = dataReader.ToDouble("mpositiontaking"),
                    UM2 = dataReader.ToDouble("auto_mpositiontaking"),
                    M3 = dataReader.ToDouble("mpositiontaking_max"),
                    A2 = dataReader.ToDouble("apositiontaking"),
                    N2 = dataReader.ToDouble("apositiontaking_min"),
                    AComm = dataReader.ToDouble("discount"),
                    MComm = dataReader.ToDouble("discount_max")
                };

                positionTakings.Add(bettingSetting);
            }

            return positionTakings;
        }

        /// <summary>
        /// Reads the member position taking data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of MasterPositionTaking.</returns>
        internal static IList<dynamic> ReadMemberPositionTakingInCaseOfAgentEditMember(IDataReader dataReader)
        {
            var positionTakings = new List<dynamic>();
            while (dataReader.Read())
            {
                var bettingSetting = new AgentEditMemberPositionTaking
                {
                    BetTypeId = dataReader.ToInt("BetType"),
                    A1 = dataReader.ToDouble("apositiontaking"),
                    N2 = dataReader.ToDouble("apositiontaking_min"),
                    A2 = dataReader.ToDouble("apositiontaking_max"),
                    Comm = dataReader.ToDouble("playerdiscount"),
                    AComm = dataReader.ToDouble("playerdiscount_max")
                };

                positionTakings.Add(bettingSetting);
            }

            return positionTakings;
        }

        /// <summary>
        /// Reads the member position taking data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of MasterPositionTaking.</returns>
        internal static IList<dynamic> ReadMemberPositionTakingInCaseOfSuperEditMember(IDataReader dataReader)
        {
            var positionTakings = new List<dynamic>();
            while (dataReader.Read())
            {
                var bettingSetting = new SuperEditMemberPositionTaking
                {
                    BetTypeId = dataReader.ToInt("BetType"),
                    A1 = dataReader.ToDouble("APositionTaking"),
                    M1 = dataReader.ToDouble("MPositionTaking"),
                    S1 = dataReader.ToDouble("SPositionTaking"),
                    IS1 = dataReader.ToBool("IsAuto_SPositionTaking_Mem"),
                    US1 = dataReader.ToDouble("Auto_SPositionTaking_Mem"),
                    IS3 = dataReader.ToBool("IsAuto_SPositionTaking"),
                    S1US1Min = dataReader.ToDouble("SPositionTaking_Min"),
                    S1Max = dataReader.ToDouble("SPositionTaking_Max"),
                    S4 = dataReader.ToDouble("Auto_SPositionTaking_Max")
                };

                positionTakings.Add(bettingSetting);
            }

            return positionTakings;
        }

        /// <summary>
        /// Reads the member position taking data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of MasterPositionTaking.</returns>
        internal static IList<dynamic> ReadMemberPositionTakingInCaseOfMasterEditMember(IDataReader dataReader)
        {
            var positionTakings = new List<dynamic>();
            while (dataReader.Read())
            {
                var bettingSetting = new MasterEditMemberPositionTaking
                {
                    BetTypeId = dataReader.ToInt("BetType"),
                    A1 = dataReader.ToDouble("apositiontaking"),
                    M1 = dataReader.ToDouble("mpositiontaking"),
                    IM2 = dataReader.ToBool("IsAuto_MPositionTaking"),
                    IM1 = dataReader.ToBool("IsAuto_MPositionTaking_Mem"),
                    UM1 = dataReader.ToDouble("Auto_MPositionTaking_Mem"),
                    M1Max = dataReader.ToDouble("MPositionTaking_Max"),
                    M1UM1Min = dataReader.ToDouble("MPositionTaking_Min"),
                    M3 = dataReader.ToDouble("Auto_MPositionTaking_Max"),
                };

                positionTakings.Add(bettingSetting);
            }

            return positionTakings;
        }
    }
}
