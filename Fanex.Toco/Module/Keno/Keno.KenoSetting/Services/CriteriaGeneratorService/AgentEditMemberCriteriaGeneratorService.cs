// --------------------------------------------------------------------------
// <copyright file="AgentEditMemberCriteriaGeneratorService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
    public class AgentEditMemberCriteriaGeneratorService : CriteriaGeneratorServiceBase, ICriteriaGeneratorService
    {
        public AgentEditMemberCriteriaGeneratorService()
            : base(null)
        {
        }

        public AgentEditMemberCriteriaGeneratorService(IKenoSession kenoSession)
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
            return new AgentUpdateMemberBetSettingCriteria()
            {
                CustId = customerInformation.CustId,
                SportType = Constants.SportType,
                BetType = betSetting.BetTypeId,
                UpdateStatus = betSetting.IsStatusUpdated,
                Disable = !betSetting.IsEnabled,
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
            return CreateUpdateMemberPositionTakingCriteria(customerInformation, positionTaking);
        }

        /// <summary>
        /// Creates the update member position taking criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="memberPositionTaking">The member position taking.</param>
        /// <returns>The AgentUpdateMemberPositionTakingCriteria object.</returns>
        internal AgentUpdateMemberPositionTakingCriteria CreateUpdateMemberPositionTakingCriteria(
            CustomerInformation customerInformation,
            AgentEditMemberPositionTaking memberPositionTaking)
        {
            return new AgentUpdateMemberPositionTakingCriteria()
            {
                CustId = customerInformation.CustId,
                SportType = Constants.SportType,
                BetType = memberPositionTaking.BetTypeId,
                UpdatePT = memberPositionTaking.IsUpdateA1,
                APositionTaking = memberPositionTaking.A1,
                UpdateComm = false,
                Discount = 0,
                SubAccId = KenoSession.SubAccId,
                UserId = KenoSession.UserId,
                Ip = KenoSession.Ip,
                Language = customerInformation.Language
            };
        }
    }
}