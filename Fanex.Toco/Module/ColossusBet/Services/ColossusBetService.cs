namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Criteria;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Repositories;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Core.Services.Impl;

    /// <summary>
    /// Customer Service.
    /// </summary>
    public class ColossusBetService : IColossusBetService
    {
        private readonly IColossusBetSettingRepository _colossusBetSettingRepository;
        private ICriteriaGeneratorService _criteriaGeneratorService;

        public ColossusBetService()
            : this(null, null)
        {
        }

        public ColossusBetService(IColossusBetSettingRepository colossusBetSettingRepository, ICriteriaGeneratorService criteriaGeneratorService)
        {
            _colossusBetSettingRepository = colossusBetSettingRepository ?? new ColossusBetSettingRepository();
            _criteriaGeneratorService = criteriaGeneratorService ?? null;
        }

        public ColossusBetModel GetSuperSetting(GetSuperSettingCriteria criteria)
        {
            return _colossusBetSettingRepository.GetSuperSetting(criteria);
        }

        public ColossusBetModel GetMasterSetting(GetMasterSettingCriteria criteria)
        {
            return _colossusBetSettingRepository.GetMasterSetting(criteria);
        }

        public ColossusBetModel GetAgentSetting(GetAgentSettingCriteria criteria)
        {
            return _colossusBetSettingRepository.GetAgentSetting(criteria);
        }

        public ColossusBetModel GetMemberSetting(GetMemberSettingCriteria criteria)
        {
            return _colossusBetSettingRepository.GetMemberSetting(criteria);
        }

        public IList<Result> UpdateSetting(UpdatedSettingCriteria criteria)
        {
            return new List<Result>();
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
    }
}