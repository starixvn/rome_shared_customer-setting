// --------------------------------------------------------------------------
// <copyright file="SuperCriteriaGeneratorService.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Services
{
    using Fanex.CustomerSetting.Module.Core.Criteria;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Criteria;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Criteria Generator Service.
    /// </summary>
    public class SuperCriteriaGeneratorService : CriteriaGeneratorServiceBase, ICriteriaGeneratorService
    {
        public SuperCriteriaGeneratorService(CustomerInformation customerInformation)
            : base(null, customerInformation)
        {
        }

        public SuperCriteriaGeneratorService(IKenoSession kenoSession, CustomerInformation customerInformation)
            : base(kenoSession, customerInformation)
        {
        }

        /// <summary>
        /// Creates the update bet setting criteria.
        /// </summary>
        /// <param name="betSetting">The bet setting.</param>
        /// <returns>
        /// The instance of ICriteria.
        /// </returns>
        public ICriteria CreateUpdateBetSettingCriteria(BetSetting betSetting)
        {
            return new UpdateSuperBetSettingCriteria()
            {
                CustId = CustomerInformation.CustId,
                SportType = Constants.SportType,
                BetType = betSetting.BetTypeId,
                UpdateStatus = !KenoSession.IsBelongToSpecialGroups && betSetting.IsStatusUpdated,
                Disable = !betSetting.IsEnabled,
                UpdateMinMaxBet = betSetting.IsBetSettingUpdated,
                MinBet = betSetting.MinBet,
                MaxBet = betSetting.MaxBet,
                MaxPerMatch = betSetting.MaxPerMatch,
                UserId = CustomerInformation.UserId,
                SubAccId = CustomerInformation.SubAccId,
                Ip = KenoSession.Ip,
                Language = CustomerInformation.Language
            };
        }

        /// <summary>
        /// Creates the update position taking criteria.
        /// </summary>
        /// <param name="positionTaking">The position taking.</param>
        /// <returns>
        /// The instance of ICriteria.
        /// </returns>
        public ICriteria CreateUpdatePositionTakingCriteria(dynamic positionTaking)
        {
            return CreateUpdateSuperPositionTakingCriteria(positionTaking);
        }

        /// <summary>
        /// Creates the update super position taking criteria.
        /// </summary>
        /// <param name="superPositionTaking">The super position taking.</param>
        /// <returns>
        /// The UpdateSuperPositionTakingCriteria object.
        /// </returns>
        internal UpdateSuperPositionTakingCriteria CreateUpdateSuperPositionTakingCriteria(SuperPositionTaking superPositionTaking)
        {
            return new UpdateSuperPositionTakingCriteria()
            {
                CustId = CustomerInformation.CustId,
                SportType = Constants.SportType,
                BetType = superPositionTaking.BetTypeId,
                S4 = superPositionTaking.S4,
                N4 = superPositionTaking.N4,
                Discount = 0,
                UpdatePositionTaking = superPositionTaking.IsUpdateS4,
                UpdateMinPositionTaking = superPositionTaking.IsUpdateN4,
                UpdateCommission = false,
                UserId = CustomerInformation.UserId,
                SubAccId = CustomerInformation.SubAccId,
                Ip = KenoSession.Ip,
                Language = CustomerInformation.Language
            };
        }
    }
}