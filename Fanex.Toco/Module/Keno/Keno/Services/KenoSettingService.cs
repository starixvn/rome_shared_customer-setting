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
        private readonly ICriteriaGeneratorService _criteriaGeneratorService;

        public KenoSettingService()
            : this(null, null)
        {
        }

        public KenoSettingService(IKenoSettingRepository kenoSettingRepository, ICriteriaGeneratorService criteriaGeneratorService)
        {
            _kenoSettingRepository = kenoSettingRepository ?? new KenoSettingRepository();
            _criteriaGeneratorService = criteriaGeneratorService ?? new CriteriaGeneratorService();
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
            superSetting.ActionType = ActionType.EditSuper;
            return UpdateCustomerSetting(superSetting);
        }

        public IList<Result> UpdateMasterCustomerSetting(MasterSettingViewModel masterSetting)
        {
            masterSetting.ActionType = ActionType.EditMaster;
            return UpdateCustomerSetting(masterSetting);
        }

        public IList<Result> UpdateAgentCustomerSetting(AgentSettingViewModel agentSetting)
        {
            agentSetting.ActionType = ActionType.EditMaster;
            return UpdateCustomerSetting(agentSetting);
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfAgentEditMember(MemberSettingViewModel memberSetting)
        {
            memberSetting.ActionType = ActionType.AgentEditMember;
            return UpdateCustomerSetting(memberSetting);
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfSuperEditMember(MemberSettingViewModel memberSetting)
        {
            memberSetting.ActionType = ActionType.SuperEditMember;
            return UpdateCustomerSetting(memberSetting);
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfMasterEditMember(MemberSettingViewModel memberSetting)
        {
            memberSetting.ActionType = ActionType.MasterEditMember;
            return UpdateCustomerSetting(memberSetting);
        }

        internal List<Result> UpdateCustomerSetting(CustomerSettingBase customerSetting)
        {
            customerSetting.UpdateSettingInCaseOfCopyAll();
            var results = new List<Result>();
            results.AddRange(this.UpdateBetSettings(customerSetting));
            results.AddRange(this.UpdatePositionTakings(customerSetting));
            return results;
        }

        /// <summary>
        /// Updates the bet settings.
        /// </summary>
        /// <param name="customerSetting">The customer setting.</param>
        /// <returns>
        /// The list of results.
        /// </returns>
        internal IList<Result> UpdateBetSettings(CustomerSettingBase customerSetting)
        {
            var results = new List<Result>();

            foreach (var betSetting in customerSetting.BetSettings)
            {
                var resultsForEachBetSetting = this.UpdateBetSetting(customerSetting.CustomerInformation, betSetting, customerSetting.ActionType);
                if (resultsForEachBetSetting != null && resultsForEachBetSetting.Count > 0)
                {
                    results.AddRange(resultsForEachBetSetting);
                }
            }

            return results;
        }

        internal IList<Result> UpdateBetSetting(CustomerInformation customerInformation, BetSetting betSetting, ActionType actionType)
        {
            var criteria = _criteriaGeneratorService.CreateUpdateBetSettingCriteria(customerInformation, betSetting, actionType);
            return _kenoSettingRepository.UpdateBetSetting(criteria);
        }

        /// <summary>
        /// Updates the position takings.
        /// </summary>
        /// <param name="customerSetting">The customer setting.</param>
        /// <returns>
        /// The list of results.
        /// </returns>
        internal IList<Result> UpdatePositionTakings(CustomerSettingBase customerSetting)
        {
            var results = new List<Result>();

            foreach (var positionTaking in customerSetting.PositionTakings)
            {
                var resultsForEachPositionTaking = this.UpdatePositionTaking(customerSetting.CustomerInformation, positionTaking, customerSetting.ActionType);
                if (resultsForEachPositionTaking != null && resultsForEachPositionTaking.Count > 0)
                {
                    results.AddRange(resultsForEachPositionTaking);
                }
            }

            return results;
        }

        internal IList<Result> UpdatePositionTaking(CustomerInformation customerInformation, PositionTakingBase positionTaking, ActionType actionType)
        {
            var criteria = _criteriaGeneratorService.CreateUpdatePositionTakingCriteria(customerInformation, positionTaking, actionType);
            return _kenoSettingRepository.UpdatePositionTaking(criteria);
        }
    }
}