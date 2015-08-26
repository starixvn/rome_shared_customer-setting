namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Criteria;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.CustomerSetting.Module.Core.Models;

    public interface IColossusBetService
    {
        ColossusBetModel GetSuperSetting(int custId);

        ColossusBetModel GetMasterSetting(int custId, int srecommend);

        ColossusBetModel GetAgentSetting(int custId, int mrecommend);

        ColossusBetModel GetMemberSetting(int custId, int recommend);

        IList<Result> UpdateSetting(UpdatedSettingCriteria criteria);

    }
}