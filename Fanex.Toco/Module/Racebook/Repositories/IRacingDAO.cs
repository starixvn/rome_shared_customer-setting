// ************************************************************************************
// <copyright file="IRacingDAO.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="kan.nguyen">Created.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.Racebook.Repositories
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.CustomerSetting.Module.Racebook.Entities;

    public interface IRacingDAO
    {
        DataTable GetRacingStatus(int custId);

        DataTable GetRacingSetting(int custId, int roleId);

        Result UpdateRacingSetting(Hashtable setting, int roleId);

        Result UpdateRacingStatus(Hashtable settingParams);

        Dictionary<int, string> GetExchange();
    }
}