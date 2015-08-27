namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Repositories
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.NAP.Framework.Data;

    public interface IColossusBetSettingRepository
    {
        ColossusBetModel GetSetting(ICriteria criteria, int custId, int roleId);

        IList<Result> UpdateSetting(ICriteria criteria);
    }
}
