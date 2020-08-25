// --------------------------------------------------------------------------
// <copyright file="AgentCriteriaGeneratorService.cs" company="">
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
        public ICriteria CreateUpdateBetSettingCriteria(BetSetting betSetting, byte? systemId)
        {
            return new UpdateAgentBetSettingCriteria()
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
                M2 = agentPositionTaking.M2,
                UM2 = agentPositionTaking.UM2,
                IM2 = agentPositionTaking.IM2,
                A2 = agentPositionTaking.A2,
                N2 = agentPositionTaking.N2,
                UpdateAllMembers = agentPositionTaking.IsUpdateAllMember,
                NewMPositionTaking = agentPositionTaking.NewMemberPT,
                Discount = 0,
                UpdatePositionTaking = agentPositionTaking.IsUpdateA2 || agentPositionTaking.IsUpdateM2,
                UpdateMinPositionTaking = agentPositionTaking.IsUpdateN2,
                UpdateAutoPositionTaking = agentPositionTaking.IsUpdateUM2,
                UpdateCommission = false,
                UserId = CustomerInformation.UserId,
                SubAccId = CustomerInformation.SubAccId,
                Ip = CustomerInformation.Ip,
                Language = CustomerInformation.Language
            };
        }
    }
}