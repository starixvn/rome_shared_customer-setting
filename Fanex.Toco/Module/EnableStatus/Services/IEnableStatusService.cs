// --------------------------------------------------------------------------
// <copyright file="IEnableStatusService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Models;
    using Fanex.CustomerSetting.Module.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IEnableStatusService
    {
        EnableStatusModel GetProductStatus(SpElementConfiguration storedProcedure, RequestContext context);

        IList<Result> UpdateProductStatus(EnableStatusModel model, SpElementConfiguration storedProcedure);
    }
}
