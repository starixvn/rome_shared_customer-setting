using Fanex.CustomerSetting.Module.NG2.Core.Criteria;
using Fanex.CustomerSetting.Module.NG2.Core.Models;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Fanex.CustomerSetting.Module.NG2.Core.Services.Interface
{
    public interface IServiceBase
    {
        /// <summary>
        /// Get IEnumerable<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IEnumerable<T> GetListData<T>(ICriteria criteria);

        Result CheckSpecialSitePermission(CheckSpecialSitePermissionCriteria criteria);
    }
}
