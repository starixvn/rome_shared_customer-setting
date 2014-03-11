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
        public SuperSetting GetSuperSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var superSetting = new SuperSetting();

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
        public MasterSetting GetMasterSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var masterSetting = new MasterSetting();

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
        public AgentSetting GetAgentSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var agentSetting = new AgentSetting();

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

        public IList<Result> UpdateSuperBetSetting(ICriteria criteria)
        {
            return this.UpdateSetting(criteria);
        }

        public IList<Result> UpdateSuperPositionTaking(ICriteria criteria)
        {
            return this.UpdateSetting(criteria);
        }

        public IList<Result> UpdateMasterBetSetting(ICriteria criteria)
        {
            return this.UpdateSetting(criteria);
        }

        public IList<Result> UpdateMasterPositionTaking(ICriteria criteria)
        {
            return this.UpdateSetting(criteria);
        }

        public IList<Result> UpdateAgentBetSetting(ICriteria criteria)
        {
            return this.UpdateSetting(criteria);
        }

        public IList<Result> UpdateAgentPositionTaking(ICriteria criteria)
        {
            return this.UpdateSetting(criteria);
        }

        /// <summary>
        /// Reads the agent information data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The CustomerInformation object.</returns>
        internal CustomerInformation ReadAgentInformationData(IDataReader dataReader)
        {
            CustomerInformation customerInformation = null;
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
        internal IList<BetSetting> ReadBetLimitData(IDataReader dataReader)
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
        internal IList<SuperPositionTaking> ReadSuperPositionTakingData(IDataReader dataReader)
        {
            var positionTakings = new List<SuperPositionTaking>();
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
        internal IList<MasterPositionTaking> ReadMasterPositionTakingData(IDataReader dataReader)
        {
            var positionTakings = new List<MasterPositionTaking>();
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
        internal IList<AgentPositionTaking> ReadAgentPositionTakingData(IDataReader dataReader)
        {
            var positionTakings = new List<AgentPositionTaking>();
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
        internal IList<MemberPositionTaking> ReadAgentEditMemberPositionTakingData(IDataReader dataReader)
        {
            var positionTakings = new List<MemberPositionTaking>();
            while (dataReader.Read())
            {
                var bettingSetting = new MemberPositionTaking
                {
                    BetTypeId = dataReader.ToInt("IBCBetTypeID"),
                    A1 = dataReader.ToDouble("positiontaking"),
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
        internal IList<MemberPositionTaking> ReadSuperEditMemberPositionTakingData(IDataReader dataReader)
        {
            var positionTakings = new List<MemberPositionTaking>();
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
        internal IList<MemberPositionTaking> ReadMasterEditMemberPositionTakingData(IDataReader dataReader)
        {
            var positionTakings = new List<MemberPositionTaking>();
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
