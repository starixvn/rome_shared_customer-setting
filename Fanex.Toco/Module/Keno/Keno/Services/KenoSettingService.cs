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

    /// <summary>
    /// Customer Service.
    /// </summary>
    public class KenoSettingService : IKenoSettingService
    {
        private readonly IKenoSettingRepository _kenoSettingRepository;
        private readonly IKenoSession _kenoSession;

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
            superSetting.UpdateSettingInCaseOfCopyAll();
            var results = new List<Result>();
            results.AddRange(this.UpdateSuperBetSettings(superSetting.CustomerInformation, superSetting.BetSettings));
            results.AddRange(this.UpdateSuperPositionTakings(superSetting.CustomerInformation, superSetting.PositionTakings));
            return results;
        }

        public IList<Result> UpdateMasterCustomerSetting(MasterSettingViewModel masterSetting)
        {
            return null;
        }

        public IList<Result> UpdateAgentCustomerSetting(AgentSettingViewModel agentSetting)
        {
            return null;
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfAgentEditMember(MemberSettingViewModel memberSetting)
        {
            return null;
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfSuperEditMember(MemberSettingViewModel memberSetting)
        {
            var results = new List<Result>();
            results.AddRange(this.UpdateMemberPositionTakingsInCaseOfSuperEditMember(memberSetting.CustomerInformation, memberSetting.PositionTakings));
            return results;
        }

        public IList<Result> UpdateMemberCustomerSettingInCaseOfMasterEditMember(MemberSettingViewModel memberSetting)
        {
            var results = new List<Result>();
            results.AddRange(this.UpdateMemberPositionTakingsInCaseOfMasterEditMember(memberSetting.CustomerInformation, memberSetting.PositionTakings));
            return results;
        }

        internal IList<Result> UpdateSuperBetSetting(CustomerInformation customerInformation, BetSetting betSetting)
        {
            var criteria = CreateUpdateSuperBetSettingCriteria(customerInformation, betSetting);
            return _kenoSettingRepository.UpdateSuperBetSetting(criteria);
        }

        internal IList<Result> UpdateSuperPositionTaking(CustomerInformation customerInformation, SuperPositionTaking superPositionTaking)
        {
            var criteria = CreateUpdateSuperPositionTakingCriteria(customerInformation, superPositionTaking);
            return _kenoSettingRepository.UpdateSuperPositionTaking(criteria);
        }

        internal IList<Result> UpdateSuperBetSettings(CustomerInformation customerInformation, IList<BetSetting> betSettings)
        {
            var results = new List<Result>();

            foreach (var betSetting in betSettings)
            {
                results.AddRange(this.UpdateSuperBetSetting(customerInformation, betSetting));
            }

            return results;
        }

        internal IList<Result> UpdateSuperPositionTakings(CustomerInformation customerInformation, IList<SuperPositionTaking> positionTakings)
        {
            var results = new List<Result>();

            foreach (var positionTaking in positionTakings)
            {
                results.AddRange(this.UpdateSuperPositionTaking(customerInformation, positionTaking));
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
            return _kenoSettingRepository.UpdateMemberPositionTakingInCaseOfSuperEditMember(criteria);
        }

        internal IList<Result> UpdateMemberPositionTakingInCaseOfMasterEditMember(CustomerInformation customerInformation, MemberPositionTaking memberPositionTaking)
        {
            var criteria = CreateMasterUpdateMemberPositionTakingCriteria(customerInformation, memberPositionTaking);
            return _kenoSettingRepository.UpdateMemberPositionTakingInCaseOfMasterEditMember(criteria);
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