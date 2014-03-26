// --------------------------------------------------------------------------
// <copyright file="MasterCriteriaGeneratorService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Services
{
    using Fanex.CustomerSetting.Module.Keno.Criteria;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Criteria Generator Service.
    /// </summary>
    public class MasterCriteriaGeneratorService : CriteriaGeneratorServiceBase, ICriteriaGeneratorService
    {
        public MasterCriteriaGeneratorService()
            : base(null)
        {
        }

        public MasterCriteriaGeneratorService(IKenoSession kenoSession)
            : base(kenoSession)
        {
        }

        /// <summary>
        /// Creates the update bet setting criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="betSetting">The bet setting.</param>
        /// <returns>
        /// The instance of ICriteria.
        /// </returns>
        public ICriteria CreateUpdateBetSettingCriteria(CustomerInformation customerInformation, BetSetting betSetting)
        {
            return new UpdateMasterBetSettingCriteria()
            {
                CustId = customerInformation.CustId,
                SportType = Constants.SportType,
                BetType = betSetting.BetTypeId,
                UpdateStatus = betSetting.IsStatusUpdated,
                Disable = !betSetting.IsEnabled,
                DisableDownLines = !betSetting.IsDownlinesEnabled,
                UpdateMinMaxBet = betSetting.IsBetSettingUpdated,
                MinBet = betSetting.MinBet,
                MaxBet = betSetting.MaxBet,
                MaxPerMatch = betSetting.MaxPerMatch,
                UserId = KenoSession.UserId,
                SubAccId = KenoSession.SubAccId,
                Ip = KenoSession.Ip,
                Language = customerInformation.Language
            };
        }

        /// <summary>
        /// Creates the update position taking criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="positionTaking">The position taking.</param>
        /// <returns>
        /// The instance of ICriteria.
        /// </returns>
        public ICriteria CreateUpdatePositionTakingCriteria(CustomerInformation customerInformation, dynamic positionTaking)
        {
            return CreateUpdateMasterPositionTakingCriteria(customerInformation, positionTaking);
        }

        /// <summary>
        /// Creates the update master position taking criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="masterPositionTaking">The master position taking.</param>
        /// <returns>The UpdateMasterPositionTakingCriteria object.</returns>
        internal UpdateMasterPositionTakingCriteria CreateUpdateMasterPositionTakingCriteria(
            CustomerInformation customerInformation,
            MasterPositionTaking masterPositionTaking)
        {
            return new UpdateMasterPositionTakingCriteria()
            {
                CustId = customerInformation.CustId,
                SportType = Constants.SportType,
                BetType = masterPositionTaking.BetTypeId,
                SPositionTaking = masterPositionTaking.S3,
                Auto_SPositionTaking = masterPositionTaking.US3,
                IsAuto_SPositionTaking = masterPositionTaking.IS3,
                MPositionTaking = masterPositionTaking.M3,
                MPositionTaking_Min = masterPositionTaking.N3,
                UpdateAllMembers = masterPositionTaking.IsUpdateAllMember,
                NewSPositionTaking = masterPositionTaking.NewMemberPT,
                Discount = 0,
                UpdatePositiontaking = masterPositionTaking.IsUpdateM3,
                UpdateMinPositiontaking = masterPositionTaking.IsUpdateN3,
                UpdateAutoPositionTaking = masterPositionTaking.IsUpdateUS3,
                UpdateCommission = false,
                UserId = KenoSession.UserId,
                Ip = KenoSession.Ip,
                Language = customerInformation.Language
            };
        }
    }
}