using Fanex.CustomerSetting.Module.Core.Models;
using Fanex.Module.NG2.Entities;

namespace Fanex.Module.NG2.Services
{
    public interface ING2StatusService
    {
        Result SyncStatus(int custId, int userId, string ip);

        Result EnableStatus(int custId, int userId, bool disabled, string ip);

        Result AutoInsertAccount(int custId, string username, RoleId roleId, int uplineId, bool uplineDisabled, SiteId siteId, int currencyId, string language = "en-US");
    }
}