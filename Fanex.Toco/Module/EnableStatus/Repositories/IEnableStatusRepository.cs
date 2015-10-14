// --------------------------------------------------------------------------
// <copyright file="IEnableStatusRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Repositories
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Models;
    using Fanex.CustomerSetting.Module.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IEnableStatusRepository
    {
        EnableStatusModel GetProductStatus(SpElementConfiguration storedProcedure, RequestContext context);

        Result UpdateProductStatus(SpElementConfiguration storedProcedure, RequestContext context, EnableStatusEntity entity);

        Result UpdateProductStatus(SpElementConfiguration storedProcedure, RequestContext context, IList<EnableStatusEntity> entities);
    }
}
