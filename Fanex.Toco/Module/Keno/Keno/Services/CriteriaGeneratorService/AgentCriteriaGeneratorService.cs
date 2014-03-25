// --------------------------------------------------------------------------
// <copyright file="AgentCriteriaGeneratorService.cs" company="Nexcel Solutions Vietnam">
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
    public class AgentCriteriaGeneratorService : CriteriaGeneratorServiceBase, ICriteriaGeneratorService
    {
        public AgentCriteriaGeneratorService()
            : base(null)
        {
        }

        public AgentCriteriaGeneratorService(IKenoSession kenoSession)
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
            return new UpdateAgentBetSettingCriteria()
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
            return CreateUpdateAgentPositionTakingCriteria(customerInformation, positionTaking);
        }

        /// <summary>
        /// Creates the update agent position taking criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="agentPositionTaking">The agent position taking.</param>
        /// <returns>The UpdateAgentPositionTakingCriteria object.</returns>
        internal UpdateAgentPositionTakingCriteria CreateUpdateAgentPositionTakingCriteria(
            CustomerInformation customerInformation,
            AgentPositionTaking agentPositionTaking)
        {
            return new UpdateAgentPositionTakingCriteria()
            {
                CustId = customerInformation.CustId,
                SportType = Constants.SportType,
                BetType = agentPositionTaking.BetTypeId,
                MPositionTaking = agentPositionTaking.M2,
                Auto_MPositionTaking = agentPositionTaking.UM2,
                IsAuto_MPositionTaking = agentPositionTaking.IM2,
                APositionTaking = agentPositionTaking.A2,
                APositionTaking_Min = agentPositionTaking.N2,
                UpdateAllMembers = agentPositionTaking.IsUpdateAllMembers,
                NewMPositionTaking = agentPositionTaking.NewMemberPT,
                Discount = 0,
                UpdatePositionTaking = agentPositionTaking.IsUpdateA2,
                UpdateMinPositionTaking = agentPositionTaking.IsUpdateN2,
                UpdateAutoPositionTaking = agentPositionTaking.IsUpdateUM2,
                UpdateCommission = false,
                UserId = KenoSession.UserId,
                Ip = KenoSession.Ip,
                Language = customerInformation.Language
            };
        }
    }
}