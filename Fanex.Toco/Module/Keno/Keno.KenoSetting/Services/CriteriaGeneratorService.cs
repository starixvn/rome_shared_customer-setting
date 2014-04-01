// --------------------------------------------------------------------------
// <copyright file="CriteriaGeneratorService.cs" company="Nexcel Solutions Vietnam">
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
    public class CriteriaGeneratorService : ICriteriaGeneratorService
    {
        private readonly IKenoSession _kenoSession;

        public CriteriaGeneratorService()
            : this(null)
        {
        }

        public CriteriaGeneratorService(IKenoSession kenoSession)
        {
            _kenoSession = kenoSession ?? new KenoSession();
        }

        /// <summary>
        /// Creates the update bet setting criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="betSetting">The bet setting.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <returns>
        /// The instance of ICriteria.
        /// </returns>
        public ICriteria CreateUpdateBetSettingCriteria(CustomerInformation customerInformation, BetSetting betSetting, ActionType actionType)
        {
            if (actionType == ActionType.EditSuper)
            {
                return CreateUpdateSuperBetSettingCriteria(customerInformation, betSetting);
            }
            else if (actionType == ActionType.EditMaster)
            {
                return CreateUpdateMasterBetSettingCriteria(customerInformation, betSetting);
            }
            else if (actionType == ActionType.EditAgent)
            {
                return CreateUpdateAgentBetSettingCriteria(customerInformation, betSetting);
            }

            return null;
        }

        /// <summary>
        /// Creates the update position taking criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="positionTaking">The position taking.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <returns>
        /// The instance of ICriteria.
        /// </returns>
        public ICriteria CreateUpdatePositionTakingCriteria(CustomerInformation customerInformation, dynamic positionTaking, ActionType actionType)
        {
            if (actionType == ActionType.EditSuper)
            {
                return CreateUpdateSuperPositionTakingCriteria(customerInformation, positionTaking);
            }
            else if (actionType == ActionType.EditMaster)
            {
                return CreateUpdateMasterPositionTakingCriteria(customerInformation, positionTaking);
            }
            else if (actionType == ActionType.EditAgent)
            {
                return CreateUpdateAgentPositionTakingCriteria(customerInformation, positionTaking);
            }
            else if (actionType == ActionType.AgentEditMember)
            {
                return CreateUpdateMemberPositionTakingCriteria(customerInformation, positionTaking);
            }
            else if (actionType == ActionType.MasterEditMember)
            {
                return CreateUpdateMemberPositionTakingCriteria(customerInformation, positionTaking);
            }
            else if (actionType == ActionType.SuperEditMember)
            {
                return CreateUpdateMemberPositionTakingCriteria(customerInformation, positionTaking);
            }

            return null;
        }

        /// <summary>
        /// Creates the update super bet setting criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="betSetting">The bet setting.</param>
        /// <returns>The UpdateSuperBetSettingCriteria object.</returns>
        internal UpdateSuperBetSettingCriteria CreateUpdateSuperBetSettingCriteria(CustomerInformation customerInformation, BetSetting betSetting)
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
                UserId = _kenoSession.UserId,
                SubAccId = _kenoSession.SubAccId,
                Ip = _kenoSession.Ip,
                Language = customerInformation.Language
            };
        }

        /// <summary>
        /// Creates the update master bet setting criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="betSetting">The bet setting.</param>
        /// <returns>The UpdateMasterBetSettingCriteria object.</returns>
        internal UpdateMasterBetSettingCriteria CreateUpdateMasterBetSettingCriteria(CustomerInformation customerInformation, BetSetting betSetting)
        {
            return new UpdateMasterBetSettingCriteria()
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
                UserId = _kenoSession.UserId,
                SubAccId = _kenoSession.SubAccId,
                Ip = _kenoSession.Ip,
                Language = customerInformation.Language
            };
        }

        /// <summary>
        /// Creates the update agent bet setting criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="betSetting">The bet setting.</param>
        /// <returns>The UpdateAgentBetSettingCriteria object.</returns>
        internal UpdateAgentBetSettingCriteria CreateUpdateAgentBetSettingCriteria(CustomerInformation customerInformation, BetSetting betSetting)
        {
            return new UpdateAgentBetSettingCriteria()
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
                UserId = _kenoSession.UserId,
                SubAccId = _kenoSession.SubAccId,
                Ip = _kenoSession.Ip,
                Language = customerInformation.Language
            };
        }

        /// <summary>
        /// Creates the update super position taking criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="superPositionTaking">The super position taking.</param>
        /// <returns>The UpdateSuperPositionTakingCriteria object.</returns>
        internal UpdateSuperPositionTakingCriteria CreateUpdateSuperPositionTakingCriteria(
            CustomerInformation customerInformation,
            SuperPositionTaking superPositionTaking)
        {
            return new UpdateSuperPositionTakingCriteria()
            {
                CustId = customerInformation.CustId,
                SportType = superPositionTaking.SportTypeId,
                BetType = superPositionTaking.BetTypeId,
                SPositionTaking = superPositionTaking.S4,
                Min_PositionTaking = superPositionTaking.N4,
                Discount = superPositionTaking.SComm,
                UpdatePositionTaking = superPositionTaking.IsUpdateS4,
                UpdateMinPositionTaking = superPositionTaking.IsUpdateN4,
                UpdateCommission = superPositionTaking.IsUpdateSCo,
                UserId = _kenoSession.UserId,
                Ip = _kenoSession.Ip,
                Language = customerInformation.Language
            };
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
                SportType = masterPositionTaking.SportTypeId,
                BetType = masterPositionTaking.BetTypeId,
                SPositionTaking = masterPositionTaking.S4,
                Min_PositionTaking = masterPositionTaking.N4,
                Discount = masterPositionTaking.SComm,
                UpdatePositionTaking = masterPositionTaking.IsUpdateS3,
                UserId = _kenoSession.UserId,
                Ip = _kenoSession.Ip,
                Language = customerInformation.Language
            };
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
                SportType = agentPositionTaking.SportTypeId,
                BetType = agentPositionTaking.BetTypeId,
                SPositionTaking = agentPositionTaking.S4,
                Min_PositionTaking = agentPositionTaking.N4,
                Discount = agentPositionTaking.SComm,
                UserId = _kenoSession.UserId,
                Ip = _kenoSession.Ip,
                Language = customerInformation.Language
            };
        }

        /// <summary>
        /// Creates the update member position taking criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="memberPositionTaking">The member position taking.</param>
        /// <returns>The AgentUpdateMemberPositionTakingCriteria object.</returns>
        internal AgentUpdateMemberPositionTakingCriteria CreateUpdateMemberPositionTakingCriteria(
            CustomerInformation customerInformation,
            MemberPositionTaking memberPositionTaking)
        {
            return new AgentUpdateMemberPositionTakingCriteria()
            {
                CustId = customerInformation.CustId,
                SportType = memberPositionTaking.SportTypeId,
                BetType = memberPositionTaking.BetTypeId,
                Discount = memberPositionTaking.SComm,
                UserId = _kenoSession.UserId,
                Ip = _kenoSession.Ip,
                Language = customerInformation.Language
            };
        }

        /// <summary>
        /// Creates the super update member position taking criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="memberPositionTaking">The member position taking.</param>
        /// <returns>The SuperUpdateMemberPositionTakingCriteria object.</returns>
        internal SuperUpdateMemberPositionTakingCriteria CreateSuperUpdateMemberPositionTakingCriteria(
            CustomerInformation customerInformation,
            MemberPositionTaking memberPositionTaking)
        {
            return new SuperUpdateMemberPositionTakingCriteria()
            {
                CustId = customerInformation.CustId,
                SportType = memberPositionTaking.SportTypeId,
                BetType = memberPositionTaking.BetTypeId,
                SPositionTaking = memberPositionTaking.S1,
                UpdatePositionTaking = memberPositionTaking.IsUpdateA1,
                UserId = _kenoSession.UserId,
                Ip = _kenoSession.Ip,
                Language = customerInformation.Language
            };
        }

        /// <summary>
        /// Creates the master update member position taking criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="memberPositionTaking">The member position taking.</param>
        /// <returns>The MasterUpdateMemberPositionTakingCriteria object.</returns>
        internal MasterUpdateMemberPositionTakingCriteria CreateMasterUpdateMemberPositionTakingCriteria(
            CustomerInformation customerInformation,
            MemberPositionTaking memberPositionTaking)
        {
            return new MasterUpdateMemberPositionTakingCriteria()
            {
                CustId = customerInformation.CustId,
                SportType = memberPositionTaking.SportTypeId,
                BetType = memberPositionTaking.BetTypeId,
                MPositionTaking = memberPositionTaking.M1,
                UpdatePositionTaking = memberPositionTaking.IsUpdateA1,
                UserId = _kenoSession.UserId,
                Ip = _kenoSession.Ip,
                Language = customerInformation.Language
            };
        }
    }
}