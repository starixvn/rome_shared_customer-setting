// -----------------------------------------------------------------------
// <copyright file="TestableKenoSettingBaseRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Repositories;
    using Fanex.NAP.Framework.Data;
    using Fanex.CustomerSetting.Module.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TestableKenoSettingBaseRepository : KenoSettingBaseRepository
    {
        internal override IList<Result> ExecuteUpdateSetting(ICriteria criteria)
        {
            return new List<Result>()
            {
                new Result()
                {
                    ErrorCode = 0,
                    ErrorMessage = string.Empty
                }
            };
        }
    }
}
