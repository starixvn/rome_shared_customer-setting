namespace Fanex.CustomerSetting.Module.NG2.EnableStatus.Repositories
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.NG2.EnableStatus.Entities;
    using Fanex.CustomerSetting.Module.NG2.EnableStatus.Mappings;
    using Fanex.CustomerSetting.Module.NG2.EnableStatus.Models;
    using Fanex.CustomerSetting.Module.NG2.Core.Models;

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
