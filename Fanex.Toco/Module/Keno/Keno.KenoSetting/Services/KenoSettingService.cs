// --------------------------------------------------------------------------
// <copyright file="KenoSettingService.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Services
{
    using System;
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

        public SuperSettingViewModel GetSuperSetting(CustomerInformationViewModel customerInformationViewModel)
        {
            var criteria = new GetSuperSettingCriteria() { CustId = customerInformationViewModel.CustId };
            var superSetting = _kenoSettingRepository.GetSuperSetting(criteria);
            superSetting.AdjustInformationWhenLoadSetting(customerInformationViewModel);
            return superSetting;
        }

        public MasterSettingViewModel GetMasterSetting(CustomerInformationViewModel customerInformationViewModel)
        {
            var criteria = new GetMasterSettingCriteria() { CustId = customerInformationViewModel.CustId };
            var masterSetting = _kenoSettingRepository.GetMasterSetting(criteria);
            masterSetting.AdjustInformationWhenLoadSetting(customerInformationViewModel);
            return masterSetting;
        }

        public AgentSettingViewModel GetAgentSetting(CustomerInformationViewModel customerInformationViewModel)
        {
            var criteria = new GetAgentSettingCriteria() { CustId = customerInformationViewModel.CustId };
            var agentSetting = _kenoSettingRepository.GetAgentSetting(criteria);
            agentSetting.AdjustInformationWhenLoadSetting(customerInformationViewModel);
            return agentSetting;
        }

        public AgentEditMemberSettingViewModel GetSettingInCaseOfAgentEditMember(CustomerInformationViewModel customerInformationViewModel)
        {
            var criteria = new AgentGetMemberSettingCriteria() { CustId = customerInformationViewModel.CustId };
            var memberSetting = _kenoSettingRepository.GetSettingInCaseOfAgentEditMember(criteria);
            memberSetting.AdjustInformationWhenLoadSetting(customerInformationViewModel);
            return memberSetting;
        }

        public SuperEditMemberSettingViewModel GetSettingInCaseOfSuperEditMember(CustomerInformationViewModel customerInformationViewModel)
        {
            var criteria = new SuperGetMemberSettingCriteria() { CustId = customerInformationViewModel.CustId };
            var memberSetting = _kenoSettingRepository.GetSettingInCaseOfSuperEditMember(criteria);
            memberSetting.AdjustInformationWhenLoadSetting(customerInformationViewModel);
            return memberSetting;
        }

        public MasterEditMemberSettingViewModel GetSettingInCaseOfMasterEditMember(CustomerInformationViewModel customerInformationViewModel)
        {
            var criteria = new MasterGetMemberSettingCriteria() { CustId = customerInformationViewModel.CustId };
            var memberSetting = _kenoSettingRepository.GetSettingInCaseOfMasterEditMember(criteria);
            memberSetting.AdjustInformationWhenLoadSetting(customerInformationViewModel);
            return memberSetting;
        }

        public IList<Result> UpdateSuperCustomerSetting(SuperSettingViewModel superSetting)
        {
            _criteriaGeneratorService = _criteriaGeneratorService ?? new SuperCriteriaGeneratorService(superSetting.CustomerInformation);
            return UpdateCustomerSetting(superSetting);
        }

        public IList<Result> UpdateMasterCustomerSetting(MasterSettingViewModel masterSetting)
        {
            _criteriaGeneratorService = _criteriaGeneratorService ?? new MasterCriteriaGeneratorService(masterSetting.CustomerInformation);
            return UpdateCustomerSetting(masterSetting);
        }

        public IList<Result> UpdateAgentCustomerSetting(AgentSettingViewModel agentSetting)
        {
            _criteriaGeneratorService = _criteriaGeneratorService ?? new AgentCriteriaGeneratorService(agentSetting.CustomerInformation);
            return UpdateCustomerSetting(agentSetting);
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfAgentEditMember(AgentEditMemberSettingViewModel memberSetting)
        {
            _criteriaGeneratorService = _criteriaGeneratorService ?? new AgentEditMemberCriteriaGeneratorService(memberSetting.CustomerInformation);
            return UpdateCustomerSetting(memberSetting);
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfSuperEditMember(SuperEditMemberSettingViewModel memberSetting)
        {
            _criteriaGeneratorService = _criteriaGeneratorService ?? new SuperEditMemberCriteriaGeneratorService(memberSetting.CustomerInformation);
            return UpdateCustomerSetting(memberSetting);
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfMasterEditMember(MasterEditMemberSettingViewModel memberSetting)
        {
            _criteriaGeneratorService = _criteriaGeneratorService ?? new MasterEditMemberCriteriaGeneratorService(memberSetting.CustomerInformation);
            return UpdateCustomerSetting(memberSetting);
        }

        /// <summary>
        /// Updates the bet setting.
        /// </summary>
        /// <param name="betSetting">The bet setting.</param>
        /// <returns>
        /// The list of results.
        /// </returns>
        public IList<Result> UpdateBetSetting(BetSetting betSetting)
        {
            var criteria = _criteriaGeneratorService.CreateUpdateBetSettingCriteria(betSetting);
            IList<Result> results = criteria is InvalidCriteria ? new List<Result>() : _kenoSettingRepository.UpdateBetSetting(criteria);

            foreach (var result in results)
            {
                result.ErrorMessage = ReformatErrorMessageForBetSetting(betSetting.BetTypeId, result.ErrorMessage);
            }

            return results;
        }

        /// <summary>
        /// Updates the bet setting.
        /// </summary>
        /// <param name="betSetting">The bet setting.</param>
        /// <param name="custId">Customer id.</param>
        /// <param name="userId">Admin user id.</param>
        /// <returns>
        /// The list of results.
        /// </returns>
        public IList<Result> UpdateBetSettingSuper(BetSetting betSetting, int custId, int userId)
        {
            _criteriaGeneratorService = new SuperCriteriaGeneratorService(new CustomerInformation()
            {
                CustId = custId,
                UserId = userId
            });
            var criteria = _criteriaGeneratorService.CreateUpdateBetSettingCriteria(betSetting);
            IList<Result> results = criteria is InvalidCriteria ? new List<Result>() : _kenoSettingRepository.UpdateBetSetting(criteria);

            foreach (var result in results)
            {
                result.ErrorMessage = ReformatErrorMessageForBetSetting(betSetting.BetTypeId, result.ErrorMessage);
            }

            return results;
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

        /// <summary>
        /// Update customer setting.
        /// </summary>
        /// <param name="customerSetting">Customer setting.</param>
        /// <returns>List result.</returns>
        internal List<Result> UpdateCustomerSetting(dynamic customerSetting)
        {
            customerSetting.AdjustInformationWhenUpdateSetting();
            var results = new List<Result>();
            try
            {
                results.AddRange(this.UpdateBetSettings(customerSetting));
                results.AddRange(this.UpdatePositionTakings(customerSetting));
            }
            catch (Exception ex)
            {
                results.Add(new Result
                {
                    ErrorCode = 100,
                    ErrorMessage = ex.Message
                });
            }

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
                    results.AddRange(this.UpdateBetSetting(betSetting));
                }
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
                    results.AddRange(this.UpdatePositionTaking(positionTaking));
                }
            }

            return results;
        }

        /// <summary>
        /// Updates the position taking.
        /// </summary>
        /// <typeparam name="T">The type of PositionTakingBase.</typeparam>
        /// <param name="positionTaking">The position taking.</param>
        /// <returns>
        /// The list of results.
        /// </returns>
        internal IList<Result> UpdatePositionTaking<T>(T positionTaking) where T : PositionTakingBase
        {
            var criteria = _criteriaGeneratorService.CreateUpdatePositionTakingCriteria(positionTaking);
            var results = _kenoSettingRepository.UpdatePositionTaking(criteria);

            foreach (var result in results)
            {
                result.ErrorMessage = ReformatErrorMessageForPositionTaking(positionTaking.BetTypeId, result.ErrorMessage);
            }

            return results;
        }
    }
}