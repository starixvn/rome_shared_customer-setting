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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Fanex.Common;
    using Fanex.Common.Extensions;
    using Fanex.CustomerSetting.Module.Keno.Criteria;
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
                    superSetting.CustomerInformation = ReaderAgentInfoData(reader);
                    superSetting.BetSettings = ReadBetLimitData(reader);
                    superSetting.SuperPositionTakings = ReadPositionTakingData(reader);
                }
            }

            return superSetting;
        }

        public Result UpdateSuperBetSetting(ICriteria criteria)
        {
            return this.UpdateSetting(criteria);
        }

        public Result UpdateSuperPositionTaking(ICriteria criteria)
        {
            return this.UpdateSetting(criteria);
        }

        /// <summary>
        /// Readers the agent information data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The CustomerInformation object.</returns>
        private CustomerInformation ReaderAgentInfoData(IDataReader dataReader)
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
        private IList<BetSetting> ReadBetLimitData(IDataReader dataReader)
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
                    IsDownlinesEnabled = !dataReader.ToBool("DisableDownlinesKeno")
                };

                betSettings.Add(bettingSetting);
            }

            return betSettings;
        }

        /// <summary>
        /// Reads the position taking data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of SuperPositionTaking.</returns>
        private IList<SuperPositionTaking> ReadPositionTakingData(IDataReader dataReader)
        {
            var positionTakings = new List<SuperPositionTaking>();
            while (dataReader.Read())
            {
                var bettingSetting = new SuperPositionTaking
                {
                    BetTypeId = dataReader.ToInt("IBCBetTypeID"),
                    N4 = dataReader.ToDouble("minpositiontaking"),
                    L4 = dataReader.ToDouble("spositiontaking"),
                    SCo = dataReader.ToDouble("sdiscount")
                };

                positionTakings.Add(bettingSetting);
            }

            return positionTakings;
        }
    }
}
