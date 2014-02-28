// --------------------------------------------------------------------------
// <copyright file="CustomerSetting.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Repositories
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Fanex.Common;
    using Fanex.Common.Extensions;
    using Fanex.CustomerSetting.Module.Keno.Criteria;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using Fanex.Data;
    using Fanex.Keno;
    using Fanex.NAP.Framework.Data;
    using Fanex.NAP.Framework.Exceptions;
    using Fanex.CustomerSetting.Module.Keno.Entities;

    /// <summary>
    /// KenoSettingRepository Class.
    /// </summary>
    public class KenoSettingRepository : IKenoSettingRepository
    {
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
                    SCO = dataReader.ToDouble("sdiscount")
                };

                positionTakings.Add(bettingSetting);
            }

            return positionTakings;
        }
    }
}
