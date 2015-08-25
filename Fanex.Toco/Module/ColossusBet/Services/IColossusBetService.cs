namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Criteria;

    public interface IColossusBetService
    {
        ColossusBetModel GetSuperSetting(GetSuperSettingCriteria criteria);

        IList<Result> UpdateSetting(UpdatedSettingCriteria criteria);

        ColossusBetModel GetMasterSetting(GetMasterSettingCriteria criteria);

        ColossusBetModel GetAgentSetting(GetAgentSettingCriteria criteria);

        ColossusBetModel GetMemberSetting(GetMemberSettingCriteria criteria);
    }
}