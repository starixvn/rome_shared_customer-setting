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
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Keno.Criteria;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.Repositories;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Customer Service.
    /// </summary>
    public class KenoSettingService : IKenoSettingService
    {
        private readonly IKenoSettingRepository _kenoSettingRepository;
        private readonly IKenoSession _kenoSession;
        private ActionType _actionType;

        public KenoSettingService()
            : this(null, null)
        {
        }

        public KenoSettingService(IKenoSettingRepository kenoSettingRepository, IKenoSession kenoSession)
        {
            _kenoSettingRepository = kenoSettingRepository ?? new KenoSettingRepository();
            _kenoSession = kenoSession ?? new KenoSession();
        }

        public SuperSettingViewModel GetSuperSetting(int custId)
        {
            var criteria = new GetSuperSettingCriteria() { CustId = custId };
            return _kenoSettingRepository.GetSuperSetting(criteria);
        }

        public MasterSettingViewModel GetMasterSetting(int custId)
        {
            var criteria = new GetMasterSettingCriteria() { CustId = custId };
            return _kenoSettingRepository.GetMasterSetting(criteria);
        }

        public AgentSettingViewModel GetAgentSetting(int custId)
        {
            var criteria = new GetAgentSettingCriteria() { CustId = custId };
            return _kenoSettingRepository.GetAgentSetting(criteria);
        }

        public MemberSettingViewModel GetSettingInCaseOfAgentEditMember(int custId)
        {
            var criteria = new GetMemberSettingCriteria() { CustId = custId };
            return _kenoSettingRepository.GetSettingInCaseOfAgentEditMember(criteria);
        }

        public MemberSettingViewModel GetSettingInCaseOfSuperEditMember(int custId)
        {
            var criteria = new GetMemberSettingCriteria() { CustId = custId };
            return _kenoSettingRepository.GetSettingInCaseOfSuperEditMember(criteria);
        }

        public MemberSettingViewModel GetSettingInCaseOfMasterEditMember(int custId)
        {
            var criteria = new GetMemberSettingCriteria() { CustId = custId };
            return _kenoSettingRepository.GetSettingInCaseOfMasterEditMember(criteria);
        }

        public IList<Result> UpdateSuperCustomerSetting(SuperSettingViewModel superSetting)
        {
            _actionType = ActionType.EditSuper;
            superSetting.UpdateSettingInCaseOfCopyAll();
            var results = new List<Result>();
            results.AddRange(this.UpdateBetSettings(superSetting.CustomerInformation, superSetting.BetSettings));
            results.AddRange(this.UpdateSuperPositionTakings(superSetting.CustomerInformation, superSetting.PositionTakings));
            return results;
        }

        public IList<Result> UpdateMasterCustomerSetting(MasterSettingViewModel masterSetting)
        {
            _actionType = ActionType.EditMaster;
            masterSetting.UpdateSettingInCaseOfCopyAll();
            var results = new List<Result>();
            results.AddRange(this.UpdateBetSettings(masterSetting.CustomerInformation, masterSetting.BetSettings));
            results.AddRange(this.UpdateMasterPositionTakings(masterSetting.CustomerInformation, masterSetting.PositionTakings));
            return results;
        }

        public IList<Result> UpdateAgentCustomerSetting(AgentSettingViewModel agentSetting)
        {
            _actionType = ActionType.EditMaster;
            agentSetting.UpdateSettingInCaseOfCopyAll();
            var results = new List<Result>();
            results.AddRange(this.UpdateBetSettings(agentSetting.CustomerInformation, agentSetting.BetSettings));
            results.AddRange(this.UpdateAgentPositionTakings(agentSetting.CustomerInformation, agentSetting.PositionTakings));
            return results;
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfAgentEditMember(MemberSettingViewModel memberSetting)
        {
            _actionType = ActionType.AgentEditMember;
            memberSetting.UpdateSettingInCaseOfCopyAll();
            var results = new List<Result>();
            results.AddRange(this.UpdateBetSettings(memberSetting.CustomerInformation, memberSetting.BetSettings));
            results.AddRange(this.UpdateMemberPositionTakings(memberSetting.CustomerInformation, memberSetting.PositionTakings));
            return results;
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfSuperEditMember(MemberSettingViewModel memberSetting)
        {
            _actionType = ActionType.SuperEditMember;
            var results = new List<Result>();
            results.AddRange(this.UpdateMemberPositionTakingsInCaseOfSuperEditMember(memberSetting.CustomerInformation, memberSetting.PositionTakings));
            return results;
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfMasterEditMember(MemberSettingViewModel memberSetting)
        {
            _actionType = ActionType.MasterEditMember;
            var results = new List<Result>();
            results.AddRange(this.UpdateMemberPositionTakingsInCaseOfMasterEditMember(memberSetting.CustomerInformation, memberSetting.PositionTakings));
            return results;
        }

        internal IList<Result> UpdateSuperPositionTaking(CustomerInformation customerInformation, SuperPositionTaking superPositionTaking)
        {
            var criteria = CreateUpdateSuperPositionTakingCriteria(customerInformation, superPositionTaking);
            return _kenoSettingRepository.UpdatePositionTaking(criteria);
        }

        internal IList<Result> UpdateMasterPositionTaking(CustomerInformation customerInformation, MasterPositionTaking masterPositionTaking)
        {
            var criteria = CreateUpdateMasterPositionTakingCriteria(customerInformation, masterPositionTaking);
            return _kenoSettingRepository.UpdatePositionTaking(criteria);
        }

        internal IList<Result> UpdateAgentPositionTaking(CustomerInformation customerInformation, AgentPositionTaking masterPositionTaking)
        {
            var criteria = CreateUpdateAgentPositionTakingCriteria(customerInformation, masterPositionTaking);
            return _kenoSettingRepository.UpdatePositionTaking(criteria);
        }

        internal IList<Result> UpdateMemberPositionTaking(CustomerInformation customerInformation, MemberPositionTaking memberPositionTaking)
        {
            var criteria = CreateUpdateMemberPositionTakingCriteria(customerInformation, memberPositionTaking);
            return _kenoSettingRepository.UpdatePositionTaking(criteria);
        }

        /// <summary>
        /// Updates the bet settings.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="betSettings">The bet settings.</param>
        /// <returns>The list of results.</returns>
        internal IList<Result> UpdateBetSettings(CustomerInformation customerInformation, IList<BetSetting> betSettings)
        {
            var results = new List<Result>();

            foreach (var betSetting in betSettings)
            {
                var resultsForEachBetSetting = this.UpdateBetSetting(customerInformation, betSetting);
                if (resultsForEachBetSetting != null && resultsForEachBetSetting.Count > 0)
                {
                    results.AddRange(resultsForEachBetSetting);
                }
            }

            return results;
        }

        internal IList<Result> UpdateBetSetting(CustomerInformation customerInformation, BetSetting betSetting)
        {
            var criteria = CreateUpdateBetSettingCriteria(customerInformation, betSetting);
            return _kenoSettingRepository.UpdateBetSetting(criteria);
        }

        /// <summary>
        /// Updates the super position takings.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="positionTakings">The position takings.</param>
        /// <returns>The list of results.</returns>
        internal IList<Result> UpdateSuperPositionTakings(CustomerInformation customerInformation, IList<SuperPositionTaking> positionTakings)
        {
            var results = new List<Result>();

            foreach (var positionTaking in positionTakings)
            {
                var resultsForEachPositionTaking = this.UpdateSuperPositionTaking(customerInformation, positionTaking);
                if (resultsForEachPositionTaking != null && resultsForEachPositionTaking.Count > 0)
                {
                    results.AddRange(resultsForEachPositionTaking);
                }
            }

            return results;
        }

        /// <summary>
        /// Updates the master position takings.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="positionTakings">The position takings.</param>
        /// <returns>The list of results.</returns>
        internal IList<Result> UpdateMasterPositionTakings(CustomerInformation customerInformation, IList<MasterPositionTaking> positionTakings)
        {
            var results = new List<Result>();

            foreach (var positionTaking in positionTakings)
            {
                results.AddRange(this.UpdateMasterPositionTaking(customerInformation, positionTaking));
            }

            return results;
        }

        /// <summary>
        /// Updates the agent position takings.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="positionTakings">The position takings.</param>
        /// <returns>The list of results.</returns>
        internal IList<Result> UpdateAgentPositionTakings(CustomerInformation customerInformation, IList<AgentPositionTaking> positionTakings)
        {
            var results = new List<Result>();

            foreach (var positionTaking in positionTakings)
            {
                results.AddRange(this.UpdateAgentPositionTaking(customerInformation, positionTaking));
            }

            return results;
        }

        internal IList<Result> UpdateMemberPositionTakings(CustomerInformation customerInformation, IList<MemberPositionTaking> positionTakings)
        {
            var results = new List<Result>();

            foreach (var positionTaking in positionTakings)
            {
                results.AddRange(this.UpdateMemberPositionTaking(customerInformation, positionTaking));
            }

            return results;
        }

        internal IList<Result> UpdateMemberPositionTakingsInCaseOfSuperEditMember(CustomerInformation customerInformation, IList<MemberPositionTaking> positionTakings)
        {
            var results = new List<Result>();

            foreach (var positionTaking in positionTakings)
            {
                results.AddRange(this.UpdateMemberPositionTakingInCaseOfSuperEditMember(customerInformation, positionTaking));
            }

            return results;
        }

        internal IList<Result> UpdateMemberPositionTakingsInCaseOfMasterEditMember(CustomerInformation customerInformation, IList<MemberPositionTaking> positionTakings)
        {
            var results = new List<Result>();

            foreach (var positionTaking in positionTakings)
            {
                results.AddRange(this.UpdateMemberPositionTakingInCaseOfMasterEditMember(customerInformation, positionTaking));
            }

            return results;
        }

        internal IList<Result> UpdateMemberPositionTakingInCaseOfSuperEditMember(CustomerInformation customerInformation, MemberPositionTaking memberPositionTaking)
        {
            var criteria = CreateSuperUpdateMemberPositionTakingCriteria(customerInformation, memberPositionTaking);
            return _kenoSettingRepository.UpdatePositionTaking(criteria);
        }

        internal IList<Result> UpdateMemberPositionTakingInCaseOfMasterEditMember(CustomerInformation customerInformation, MemberPositionTaking memberPositionTaking)
        {
            var criteria = CreateMasterUpdateMemberPositionTakingCriteria(customerInformation, memberPositionTaking);
            return _kenoSettingRepository.UpdatePositionTaking(criteria);
        }

        /// <summary>
        /// Creates the update bet setting criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="betSetting">The bet setting.</param>
        /// <returns>
        /// The instance of ICriteria.
        /// </returns>
        internal ICriteria CreateUpdateBetSettingCriteria(CustomerInformation customerInformation, BetSetting betSetting)
        {
            if (_actionType == ActionType.EditSuper)
            {
                return CreateUpdateSuperBetSettingCriteria(customerInformation, betSetting);
            }
            else if (_actionType == ActionType.EditMaster)
            {
                return CreateUpdateMasterBetSettingCriteria(customerInformation, betSetting);
            }
            else if (_actionType == ActionType.EditAgent)
            {
                return CreateUpdateAgentBetSettingCriteria(customerInformation, betSetting);
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