// -----------------------------------------------------------------------
// <copyright file="TestableKenoSettingBaseRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.Keno.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.Repositories;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TestableKenoSettingBaseRepository : KenoSettingBaseRepository
    {
        internal override Result ExecuteUpdateSetting(ICriteria criteria)
        {
            return new Result()
            {
                ErrCode = 0,
                ErrMsg = string.Empty
            };
        }
    }
}
