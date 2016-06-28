// --------------------------------------------------------------------------
// <copyright file="IKenoSettingBaseRepository.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Repositories
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.NAP.Framework.Data;

    public interface IKenoSettingBaseRepository
    {
        IList<Result> UpdateSetting(ICriteria criteria);
    }
}
