namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Criteria;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Repositories;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Core.Services.Impl;
    using Fanex.NAP.Framework.Data;

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

        public ColossusBetModel GetSetting(ICriteria criteria, int custId, int roleId)
        {
            return _colossusBetSettingRepository.GetSetting(criteria, custId, roleId);
        }

        public IList<Result> UpdateSetting(UpdatedSettingCriteria criteria)
        {
            return _colossusBetSettingRepository.UpdateSetting(criteria);
        }
    }
}