// --------------------------------------------------------------------------
// <copyright file="AgentCriteriaGeneratorService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Services
{
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Criteria;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Criteria Generator Service.
    /// </summary>
    public class AgentCriteriaGeneratorService : CriteriaGeneratorServiceBase, ICriteriaGeneratorService
    {
        public AgentCriteriaGeneratorService(CustomerInformation customerInformation)
            : base(null, customerInformation)
        {
        }

        public AgentCriteriaGeneratorService(IKenoSession kenoSession, CustomerInformation customerInformation)
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
            return new UpdateAgentBetSettingCriteria()
            {
                CustId = CustomerInformation.CustId,
                SportType = Constants.SportType,
                BetType = betSetting.BetTypeId,
                UpdateStatus = betSetting.IsStatusUpdated,
                Disable = !betSetting.IsEnabled,
                DisableDownLines = !betSetting.IsDownlinesEnabled,
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
            return CreateUpdateAgentPositionTakingCriteria(positionTaking);
        }

        /// <summary>
        /// Creates the update agent position taking criteria.
        /// </summary>
        /// <param name="agentPositionTaking">The agent position taking.</param>
        /// <returns>
        /// The UpdateAgentPositionTakingCriteria object.
        /// </returns>
        internal UpdateAgentPositionTakingCriteria CreateUpdateAgentPositionTakingCriteria(AgentPositionTaking agentPositionTaking)
        {
            return new UpdateAgentPositionTakingCriteria()
            {
                CustId = CustomerInformation.CustId,
                SportType = Constants.SportType,
                BetType = agentPositionTaking.BetTypeId,
                MPositionTaking = agentPositionTaking.M2,
                Auto_MPositionTaking = agentPositionTaking.UM2,
                IsAuto_MPositionTaking = agentPositionTaking.IM2,
                APositionTaking = agentPositionTaking.A2,
                APositionTaking_Min = agentPositionTaking.N2,
                UpdateAllMembers = agentPositionTaking.IsUpdateAllMember,
                NewMPositionTaking = agentPositionTaking.NewMemberPT,
                Discount = 0,
                UpdatePositionTaking = agentPositionTaking.IsUpdateA2 || agentPositionTaking.IsUpdateM2,
                UpdateMinPositionTaking = agentPositionTaking.IsUpdateN2,
                UpdateAutoPositionTaking = agentPositionTaking.IsUpdateUM2,
                UpdateCommission = false,
                UserId = CustomerInformation.UserId,
                Ip = KenoSession.Ip,
                Language = CustomerInformation.Language
            };
        }
    }
}