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

        public ColossusBetModel GetSuperSetting(int custId)
        {
            return _colossusBetSettingRepository.GetSuperSetting(new GetSuperSettingCriteria()
            {
                CustId = custId
            });
        }

        public ColossusBetModel GetMasterSetting(int custId, int srecommend)
        {
            return _colossusBetSettingRepository.GetMasterSetting(new GetMasterSettingCriteria()
                {
                    CustId = custId,
                    Srecommend = srecommend
                });
        }

        public ColossusBetModel GetAgentSetting(int custId, int mrecommend)
        {
            return _colossusBetSettingRepository.GetAgentSetting(new GetAgentSettingCriteria()
                {
                    CustId = custId,
                    Mrecommend = mrecommend
                });
        }

        public ColossusBetModel GetMemberSetting(int custId, int recommend)
        {
            return _colossusBetSettingRepository.GetMemberSetting(new GetMemberSettingCriteria()
                {
                    CustId = custId,
                    Recommend = recommend
                });
        }

        public IList<Result> UpdateSetting(UpdatedSettingCriteria criteria)
        {
            return _colossusBetSettingRepository.UpdateSetting(criteria);
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