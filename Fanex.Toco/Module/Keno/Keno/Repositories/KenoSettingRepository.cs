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
                    superSetting.CustomerInformation = ReadAgentInformationData(reader);
                    superSetting.BetSettings = ReadBetLimitData(reader);
                    superSetting.PositionTakings = ReadSuperPositionTakingData(reader);
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
                    masterSetting.CustomerInformation = ReadAgentInformationData(reader);
                    masterSetting.BetSettings = ReadBetLimitData(reader);
                    masterSetting.PositionTakings = ReadMasterPositionTakingData(reader);
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
                    agentSetting.CustomerInformation = ReadAgentInformationData(reader);
                    agentSetting.BetSettings = ReadBetLimitData(reader);
                    agentSetting.PositionTakings = ReadAgentPositionTakingData(reader);
                }
            }

            return agentSetting;
        }

        /// <summary>
        /// Gets the member setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The MemberSetting object.</returns>
        public MemberSettingViewModel GetSettingInCaseOfAgentEditMember(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var memberSetting = new MemberSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    memberSetting.CustomerInformation = ReadAgentInformationData(reader);
                    memberSetting.BetSettings = ReadBetLimitData(reader);
                    memberSetting.PositionTakings = ReadMemberPositionTakingInCaseOfAgentEditMember(reader);
                }
            }

            return memberSetting;
        }

        /// <summary>
        /// Gets the member setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The MemberSetting object.</returns>
        public MemberSettingViewModel GetSettingInCaseOfSuperEditMember(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var memberSetting = new MemberSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    memberSetting.CustomerInformation = ReadAgentInformationData(reader);
                    memberSetting.BetSettings = ReadBetLimitData(reader);
                    memberSetting.PositionTakings = ReadMemberPositionTakingInCaseOfSuperEditMember(reader);
                }
            }

            return memberSetting;
        }

        /// <summary>
        /// Gets the member setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The MemberSetting object.</returns>
        public MemberSettingViewModel GetSettingInCaseOfMasterEditMember(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var memberSetting = new MemberSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    memberSetting.CustomerInformation = ReadAgentInformationData(reader);
                    memberSetting.BetSettings = ReadBetLimitData(reader);
                    memberSetting.PositionTakings = ReadMemberPositionTakingInCaseOfMasterEditMember(reader);
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
        /// <returns>The CustomerInformation object.</returns>
        internal static CustomerInformation ReadAgentInformationData(IDataReader dataReader)
        {
            CustomerInformation customerInformation = new CustomerInformation();
            while (dataReader.Read())
            {
                customerInformation = new CustomerInformation();
                customerInformation.FirstName = dataReader.ToString("firstname");
                customerInformation.LastName = dataReader.ToString("lastname");
                customerInformation.Phone = dataReader.ToString("phone");
                customerInformation.MobilePhone = dataReader.ToString("mobilephone");
                customerInformation.Fax = dataReader.ToString("fax");
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
                    BetTypeId = dataReader.ToInt("IBCBetTypeID"),
                    MinBet = dataReader.ToDecimal("MinBet"),
                    MaxBet = dataReader.ToDecimal("MaxBet"),
                    MaxPerMatch = dataReader.ToDecimal("MaxPerGameType"),
                    IsEnabled = !dataReader.ToBool("IsDisabled"),
                    IsDownlinesEnabled = !dataReader.ToBool("DisableDownlinesKeno"),
                    Upline = new BetSetting()
                    {
                        MinBet = dataReader.ToDecimal("MaxMinBet"),
                        MaxBet = !dataReader.IsDBNull("MinMaxBet") ? dataReader.ToDecimal("MinMaxBet") : 0,
                        MaxPerMatch = !dataReader.IsDBNull("MinMaxPerMatch") ? dataReader.ToDecimal("MinMaxPerMatch") : 0
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
                    BetTypeId = dataReader.ToInt("IBCBetTypeID"),
                    S4 = dataReader.ToDouble("spositiontaking"),
                    N4 = dataReader.ToDouble("minpositiontaking"),
                    L4 = 0.85,
                    SComm = dataReader.ToDouble("sdiscount"),
                    SCommMax = dataReader.ToDouble("smaxdiscount")
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
                    IS3 = dataReader.ToBool("IS3"),
                    S3 = dataReader.ToDouble("spresetpositiontaking"),
                    US3 = dataReader.ToDouble("US3"),
                    S4 = dataReader.ToDouble("spositiontaking"),
                    M3 = dataReader.ToDouble("m3"),
                    N3 = dataReader.ToDouble("n3"),
                    MComm = dataReader.ToDouble("mdiscount"),
                    SComm = dataReader.ToDouble("mmaxdiscount")
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
                    BetTypeId = dataReader.ToInt("IBCBetTypeID"),
                    IM2 = dataReader.ToBool("IM2"),
                    M2 = dataReader.ToDouble("mpresetpositiontaking"),
                    UM2 = dataReader.ToDouble("UM2"),
                    M3 = dataReader.ToDouble("m3"),
                    A2 = dataReader.ToDouble("a2"),
                    N2 = dataReader.ToDouble("aminpositiontaking"),
                    AComm = dataReader.ToDouble("adiscount"),
                    MComm = dataReader.ToDouble("amaxdiscount")
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
                var bettingSetting = new MemberPositionTaking
                {
                    BetTypeId = dataReader.ToInt("IBCBetTypeID"),
                    A1 = dataReader.ToDouble("positiontaking"),
                    N2 = dataReader.ToDouble("aminpositiontaking"),
                    L2 = dataReader.ToDouble("maxpositiontaking"),
                    A4 = dataReader.ToDouble("amaxminpositiontaking"),
                    A2 = dataReader.ToDouble("a2"),
                    Comm = dataReader.ToDouble("discount"),
                    AComm = dataReader.ToDouble("maxdiscount")
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
                var bettingSetting = new MemberPositionTaking
                {
                    BetTypeId = dataReader.ToInt("IBCBetTypeID"),
                    A1 = dataReader.ToDouble("positiontaking"),
                    L2 = dataReader.ToDouble("maxpositiontaking"),
                    A4 = dataReader.ToDouble("amaxminpositiontaking"),
                    A2 = dataReader.ToDouble("a2")
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
                var bettingSetting = new MemberPositionTaking
                {
                    BetTypeId = dataReader.ToInt("IBCBetTypeID"),
                    A1 = dataReader.ToDouble("positiontaking"),
                    L2 = dataReader.ToDouble("maxpositiontaking"),
                    A4 = dataReader.ToDouble("amaxminpositiontaking"),
                    A2 = dataReader.ToDouble("a2")
                };

                positionTakings.Add(bettingSetting);
            }

            return positionTakings;
        }
    }
}
