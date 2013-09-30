using System.Collections;
using System.Collections.Generic;
using System.Data;
using Fanex.CustomerSetting.Module.Racebook.Entities;

namespace Fanex.CustomerSetting.Module.Racebook.Repositories
{
    public interface IRacingDAO
    {
        DataTable GetRacingStatus(int custId);

        DataTable GetRacingSetting(int custId, int roleId);

        Result UpdateRacingSetting(Hashtable setting, int roleId);

        Result UpdateRacingStatus(Hashtable settingParams);

        Dictionary<int, string> GetExchange();
    }
}