// --------------------------------------------------------------------------
// <copyright file="IEnableStatusService.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.NG2.EnableStatus.Services
{
    using Fanex.CustomerSetting.Module.NG2.Core.Models;
    using Fanex.CustomerSetting.Module.NG2.EnableStatus.Entities;
    using Fanex.CustomerSetting.Module.NG2.EnableStatus.Mappings;
    using Fanex.CustomerSetting.Module.NG2.EnableStatus.Models;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IEnableStatusService
    {
        EnableStatusModel GetProductStatus(SpElementConfiguration storedProcedure, RequestContext context);

        IList<Result> UpdateProductStatus(EnableStatusModel model, SpElementConfiguration storedProcedure);
    }
}
