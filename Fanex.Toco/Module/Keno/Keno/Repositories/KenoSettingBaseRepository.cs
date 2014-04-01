// --------------------------------------------------------------------------
// <copyright file="KenoSettingBaseRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Fanex.Common;
    using Fanex.Common.Extensions;
    using Fanex.CustomerSetting.Module.Keno.Criteria;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.NAP.Framework.Exceptions; 

    /// <summary>
    /// KenoSettingRepository Class.
    /// </summary>
    public class KenoSettingBaseRepository : IKenoSettingBaseRepository
    {
        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Result object.</returns>
        public Result UpdateSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.ExecuteNonQuery(criteria.GetSpParams());
                var outputParams = objDb.GetOuputParams();
                return new Result()
                {
                    ErrCode = Convert.ToInt32(outputParams["errcode"]),
                    ErrMsg = Convert.ToString(outputParams["errmsg"])
                };
            }
        }
    }
}
