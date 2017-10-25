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

        public ColossusBetModel EditSetting(SettingContext settingContext)
        {
            switch (settingContext.RoleId)
            {
                case 4:
                    return _colossusBetSettingRepository.GetSetting(
                         new GetSuperSettingCriteria
                                {
                                    CustId = settingContext.CustId
                                },
                                settingContext);
                case 3:
                    return _colossusBetSettingRepository.GetSetting(
                      new GetMasterSettingCriteria
             {
                 CustId = settingContext.CustId,
                 Srecommend = settingContext.Srecommend
             },
             settingContext);

                case 2:
                    return _colossusBetSettingRepository.GetSetting(
                    new GetAgentSettingCriteria
           {
               CustId = settingContext.CustId,
               Mrecommend = settingContext.Mrecommend
           },
           settingContext);
                default:
                    return _colossusBetSettingRepository.GetSetting(
                   new GetMemberSettingCriteria
                   {
                       CustId = settingContext.CustId,
                       Recommend = settingContext.Recommend
                   },
                   settingContext);
            }
        }

        public IList<Result> UpdateSetting(UpdatedSettingCriteria criteria)
        {
            return _colossusBetSettingRepository.UpdateSetting(criteria);
        }
    }
}