namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Repositories
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.NAP.Framework.Data;

    public interface IColossusBetSettingRepository
    {
        ColossusBetModel GetSuperSetting(ICriteria criteria);

        ColossusBetModel GetMasterSetting(ICriteria criteria);

        ColossusBetModel GetAgentSetting(ICriteria criteria);

        ColossusBetModel GetMemberSetting(ICriteria criteria);

        IList<Result> UpdateSetting(ICriteria criteria);
    }
}
