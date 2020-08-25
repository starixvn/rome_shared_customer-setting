// --------------------------------------------------------------------------
// <copyright file="MasterCriteriaGeneratorService.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Services
{
    using Data.Repository;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Criteria;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Helpers;

    /// <summary>
    /// Criteria Generator Service.
    /// </summary>
    public class MasterCriteriaGeneratorService : CriteriaGeneratorServiceBase, ICriteriaGeneratorService
    {
        public MasterCriteriaGeneratorService(CustomerInformation customerInformation)
            : base(null, customerInformation)
        {
        }

        public MasterCriteriaGeneratorService(IKenoSession kenoSession, CustomerInformation customerInformation)
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
        public ICriteria CreateUpdateBetSettingCriteria(BetSetting betSetting, byte? systemId)
        {
            return new UpdateMasterBetSettingCriteria()
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
                Ip = CustomerInformation.Ip,
                Language = CustomerInformation.Language,
                SystemId = systemId ?? SystemIdHelper.GetFromConfigurationFile()
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
            return CreateUpdateMasterPositionTakingCriteria(positionTaking);
        }

        /// <summary>
        /// Creates the update master position taking criteria.
        /// </summary>
        /// <param name="masterPositionTaking">The master position taking.</param>
        /// <returns>
        /// The UpdateMasterPositionTakingCriteria object.
        /// </returns>
        internal UpdateMasterPositionTakingCriteria CreateUpdateMasterPositionTakingCriteria(MasterPositionTaking masterPositionTaking)
        {
            return new UpdateMasterPositionTakingCriteria()
            {
                CustId = CustomerInformation.CustId,
                SportType = Constants.SportType,
                BetType = masterPositionTaking.BetTypeId,
                S3 = masterPositionTaking.S3,
                US3 = masterPositionTaking.US3,
                IS3 = masterPositionTaking.IS3,
                M3 = masterPositionTaking.M3,
                N3 = masterPositionTaking.N3,
                UpdateAllMembers = masterPositionTaking.IsUpdateAllMember,
                NewSPositionTaking = masterPositionTaking.NewMemberPT,
                Discount = 0,
                UpdatePositiontaking = masterPositionTaking.IsUpdateM3 || masterPositionTaking.IsUpdateS3,
                UpdateMinPositiontaking = masterPositionTaking.IsUpdateN3,
                UpdateAutoPositionTaking = masterPositionTaking.IsUpdateUS3,
                UpdateCommission = false,
                UserId = CustomerInformation.UserId,
                SubAccId = CustomerInformation.SubAccId,
                Ip = CustomerInformation.Ip,
                Language = CustomerInformation.Language
            };
        }
    }
}