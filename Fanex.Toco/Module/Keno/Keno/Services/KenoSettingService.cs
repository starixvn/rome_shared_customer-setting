// --------------------------------------------------------------------------
// <copyright file="KenoSettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using Fanex.Common;
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.CustomerSetting.Module.Keno.Criteria;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.Repositories;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using Fanex.NAP.Framework.Data;
    using Newtonsoft.Json;

    /// <summary>
    /// Customer Service.
    /// </summary>
    public class KenoSettingService : IKenoSettingService
    {
        private readonly IKenoSettingRepository _kenoSettingRepository;

        public KenoSettingService()
            : this(null)
        {
        }

        public KenoSettingService(IKenoSettingRepository kenoSettingRepository)
        {
            _kenoSettingRepository = kenoSettingRepository ?? new KenoSettingRepository();
        }

        public SuperSetting GetSuperSetting(int custId)
        {
            var criteria = new GetSuperSettingCriteria() { CustId = custId };
            return _kenoSettingRepository.GetSuperSetting(criteria);
        }

        public Result UpdateSuperBetSetting(CustomerInformation customerInformation, BetSetting betSetting)
        {
            var criteria = CreateUpdateSuperBetSettingCriteria(customerInformation, betSetting);
            return _kenoSettingRepository.UpdateSuperBetSetting(criteria);
        }

        public Result UpdateSuperPositionTaking(CustomerInformation customerInformation, SuperPositionTaking superPositionTaking)
        {
            var criteria = CreateUpdateSuperPositionTakingCriteria(customerInformation, superPositionTaking);
            return _kenoSettingRepository.UpdateSuperPositionTaking(criteria);
        }

        public IList<Result> UpdateSuperBetSettings(CustomerInformation customerInformation, IList<BetSetting> betSettings)
        {
            var results = new List<Result>();

            foreach (var betSetting in betSettings)
            {
                results.Add(this.UpdateSuperBetSetting(customerInformation, betSetting));
            }

            return results;
        }

        public IList<Result> UpdateSuperPositionTakings(CustomerInformation customerInformation, IList<SuperPositionTaking> positionTakings)
        {
            var results = new List<Result>();

            foreach (var positionTaking in positionTakings)
            {
                results.Add(this.UpdateSuperPositionTaking(customerInformation, positionTaking));
            }

            return results;
        }

        public IList<Result> UpdateSuperCustomerSetting(SuperSetting superSetting)
        {
            var results = new List<Result>();
            results.AddRange(this.UpdateSuperBetSettings(superSetting.CustomerInformation, superSetting.BetSettings));
            results.AddRange(this.UpdateSuperPositionTakings(superSetting.CustomerInformation, superSetting.SuperPositionTakings));
            return results;
        }

        /// <summary>
        /// Creates the update super bet setting criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="betSetting">The bet setting.</param>
        /// <returns>The UpdateSuperBetSettingCriteria object.</returns>
        private static UpdateSuperBetSettingCriteria CreateUpdateSuperBetSettingCriteria(CustomerInformation customerInformation, BetSetting betSetting)
        {
            return new UpdateSuperBetSettingCriteria()
            {
                CustId = customerInformation.CustId,
                BetTypeId = betSetting.BetTypeId,
                UpdateStatus = betSetting.IsStatusUpdated,
                Disable = !betSetting.IsEnabled,
                DisableDownLines = !betSetting.IsDownlinesEnabled,
                UpdateMinMaxBet = betSetting.IsBetSettingUpdated,
                MinBet = betSetting.MinBet,
                MaxBet = betSetting.MaxBet,
                MaxPerMatch = betSetting.MaxPerMatch,
                UserId = AppSession.AccId,
                SubAccId = AppSession.SubAccountId,
                Ip = Utils.GetClientIP(),
                Language = customerInformation.Language
            };
        }

        /// <summary>
        /// Creates the update super position taking criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="superPositionTaking">The super position taking.</param>
        /// <returns>The UpdateSuperPositionTakingCriteria object.</returns>
        private static UpdateSuperPositionTakingCriteria CreateUpdateSuperPositionTakingCriteria(
            CustomerInformation customerInformation,
            SuperPositionTaking superPositionTaking)
        {
            return new UpdateSuperPositionTakingCriteria()
            {
                CustId = customerInformation.CustId,
                SportType = superPositionTaking.SportTypeId,
                BetType = superPositionTaking.BetTypeId,
                SPositionTaking = superPositionTaking.L4,
                Min_PositionTaking = superPositionTaking.N4,
                Discount = superPositionTaking.SComm,
                UpdatePositionTaking = superPositionTaking.IsUpdatePT,
                UpdateMinPositionTaking = superPositionTaking.IsUpdateN4,
                UpdateCommission = superPositionTaking.IsUpdateSCo,
                UserId = AppSession.AccId,
                Ip = Utils.GetClientIP(),
                Language = customerInformation.Language
            };
        }
    }
}