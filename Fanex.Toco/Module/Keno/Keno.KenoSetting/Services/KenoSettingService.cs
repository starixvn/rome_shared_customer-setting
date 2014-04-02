// --------------------------------------------------------------------------
// <copyright file="KenoSettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Criteria;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Core.Services.Impl;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Criteria;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Repositories;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.ViewModels;

    /// <summary>
    /// Customer Service.
    /// </summary>
    public class KenoSettingService : IKenoSettingService
    {
        private readonly IKenoSettingRepository _kenoSettingRepository;
        private ICriteriaGeneratorService _criteriaGeneratorService;

        public KenoSettingService()
            : this(null, null)
        {
        }

        public KenoSettingService(IKenoSettingRepository kenoSettingRepository, ICriteriaGeneratorService criteriaGeneratorService)
        {
            _kenoSettingRepository = kenoSettingRepository ?? new KenoSettingRepository();
            _criteriaGeneratorService = criteriaGeneratorService ?? null;
        }

        public SuperSettingViewModel GetSuperSetting(int custId)
        {
            var criteria = new GetSuperSettingCriteria() { CustId = custId };
            var superSetting = _kenoSettingRepository.GetSuperSetting(criteria);
            superSetting.AdjustInformationWhenLoadSetting(custId);
            return superSetting;
        }

        public MasterSettingViewModel GetMasterSetting(int custId)
        {
            var criteria = new GetMasterSettingCriteria() { CustId = custId };
            var masterSetting = _kenoSettingRepository.GetMasterSetting(criteria);
            masterSetting.AdjustInformationWhenLoadSetting(custId);
            return masterSetting;
        }

        public AgentSettingViewModel GetAgentSetting(int custId)
        {
            var criteria = new GetAgentSettingCriteria() { CustId = custId };
            var agentSetting = _kenoSettingRepository.GetAgentSetting(criteria);
            agentSetting.AdjustInformationWhenLoadSetting(custId);
            return agentSetting;
        }

        public AgentEditMemberSettingViewModel GetSettingInCaseOfAgentEditMember(int custId)
        {
            var criteria = new AgentGetMemberSettingCriteria() { CustId = custId };
            var memberSetting = _kenoSettingRepository.GetSettingInCaseOfAgentEditMember(criteria);
            memberSetting.AdjustInformationWhenLoadSetting(custId);
            return memberSetting;
        }

        public SuperEditMemberSettingViewModel GetSettingInCaseOfSuperEditMember(int custId)
        {
            var criteria = new SuperGetMemberSettingCriteria() { CustId = custId };
            var memberSetting = _kenoSettingRepository.GetSettingInCaseOfSuperEditMember(criteria);
            memberSetting.AdjustInformationWhenLoadSetting(custId);
            return memberSetting;
        }

        public MasterEditMemberSettingViewModel GetSettingInCaseOfMasterEditMember(int custId)
        {
            var criteria = new MasterGetMemberSettingCriteria() { CustId = custId };
            var memberSetting = _kenoSettingRepository.GetSettingInCaseOfMasterEditMember(criteria);
            memberSetting.AdjustInformationWhenLoadSetting(custId);
            return memberSetting;
        }

        public IList<Result> UpdateSuperCustomerSetting(SuperSettingViewModel superSetting)
        {
            _criteriaGeneratorService = _criteriaGeneratorService ?? new SuperCriteriaGeneratorService();
            return UpdateCustomerSetting(superSetting);
        }

        public IList<Result> UpdateMasterCustomerSetting(MasterSettingViewModel masterSetting)
        {
            _criteriaGeneratorService = _criteriaGeneratorService ?? new MasterCriteriaGeneratorService();
            return UpdateCustomerSetting(masterSetting);
        }

        public IList<Result> UpdateAgentCustomerSetting(AgentSettingViewModel agentSetting)
        {
            _criteriaGeneratorService = _criteriaGeneratorService ?? new AgentCriteriaGeneratorService();
            return UpdateCustomerSetting(agentSetting);
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfAgentEditMember(AgentEditMemberSettingViewModel memberSetting)
        {
            _criteriaGeneratorService = _criteriaGeneratorService ?? new AgentEditMemberCriteriaGeneratorService();
            return UpdateCustomerSetting(memberSetting);
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfSuperEditMember(SuperEditMemberSettingViewModel memberSetting)
        {
            _criteriaGeneratorService = _criteriaGeneratorService ?? new SuperEditMemberCriteriaGeneratorService();
            return UpdateCustomerSetting(memberSetting);
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfMasterEditMember(MasterEditMemberSettingViewModel memberSetting)
        {
            _criteriaGeneratorService = _criteriaGeneratorService ?? new MasterEditMemberCriteriaGeneratorService();
            return UpdateCustomerSetting(memberSetting);
        }

        internal static string ReformatErrorMessageForBetSetting(int betTypeId, string inputMessage)
        {
            string betTypeName = CacheRef.GetBettype(betTypeId);
            return string.Format(
                "{0} - {1} - {2}",
                Resources.Messages.BetSetting,
                !string.IsNullOrEmpty(betTypeName) ? betTypeName : betTypeId.ToString(),
                inputMessage);
        }

        internal static string ReformatErrorMessageForPositionTaking(int betTypeId, string inputMessage)
        {
            string betTypeName = CacheRef.GetBettype(betTypeId);
            return string.Format(
                "{0} - {1} - {2}",
                Resources.Messages.PositionTaking,
                !string.IsNullOrEmpty(betTypeName) ? betTypeName : betTypeId.ToString(),
                inputMessage);
        }

        internal List<Result> UpdateCustomerSetting(dynamic customerSetting)
        {
            customerSetting.AdjustInformationWhenUpdateSetting();
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
        internal IList<Result> UpdateBetSettings(dynamic customerSetting)
        {
            var results = new List<Result>();

            foreach (var betSetting in customerSetting.BetSettings)
            {
                if (betSetting.IsDataChanged())
                {
                    results.AddRange(this.UpdateBetSetting(customerSetting.CustomerInformation, betSetting));
                }
            }

            return results;
        }

        /// <summary>
        /// Updates the bet setting.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="betSetting">The bet setting.</param>
        /// <returns>The list of results.</returns>
        internal IList<Result> UpdateBetSetting(CustomerInformation customerInformation, BetSetting betSetting)
        {
            var criteria = _criteriaGeneratorService.CreateUpdateBetSettingCriteria(customerInformation, betSetting);
            IList<Result> results = criteria is InvalidCriteria ? new List<Result>() : _kenoSettingRepository.UpdateBetSetting(criteria);

            foreach (var result in results)
            {
                result.ErrorMessage = ReformatErrorMessageForBetSetting(betSetting.BetTypeId, result.ErrorMessage);
            }

            return results;
        }

        /// <summary>
        /// Updates the position takings.
        /// </summary>
        /// <param name="customerSetting">The customer setting.</param>
        /// <returns>
        /// The list of results.
        /// </returns>
        internal IList<Result> UpdatePositionTakings(dynamic customerSetting)
        {
            var results = new List<Result>();

            foreach (var positionTaking in customerSetting.PositionTakings)
            {
                if (positionTaking.IsDataChanged())
                {
                    results.AddRange(this.UpdatePositionTaking(customerSetting.CustomerInformation, positionTaking));
                }
            }

            return results;
        }

        /// <summary>
        /// Updates the position taking.
        /// </summary>
        /// <typeparam name="T">The type of PositionTakingBase.</typeparam>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="positionTaking">The position taking.</param>
        /// <returns>
        /// The list of results.
        /// </returns>
        internal IList<Result> UpdatePositionTaking<T>(CustomerInformation customerInformation, T positionTaking) where T : PositionTakingBase
        {
            var criteria = _criteriaGeneratorService.CreateUpdatePositionTakingCriteria(customerInformation, positionTaking);
            var results = _kenoSettingRepository.UpdatePositionTaking(criteria);

            foreach (var result in results)
            {
                result.ErrorMessage = ReformatErrorMessageForPositionTaking(positionTaking.BetTypeId, result.ErrorMessage);
            }

            return results;
        }
    }
}