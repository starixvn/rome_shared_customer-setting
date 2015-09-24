namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Repositories
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.NAP.Framework.Data;

    public interface IColossusBetSettingRepository
    {
        ColossusBetModel GetSetting(ICriteria criteria, SettingContext settingContext);

        IList<Result> UpdateSetting(ICriteria criteria);
    }
}
