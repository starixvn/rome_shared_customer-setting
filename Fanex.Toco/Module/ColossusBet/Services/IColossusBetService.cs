namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Criteria;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.NAP.Framework.Data;

    public interface IColossusBetService
    {
        ColossusBetModel GetSetting(ICriteria criteria, int custId, int roleId);

        IList<Result> UpdateSetting(UpdatedSettingCriteria criteria);
    }
}