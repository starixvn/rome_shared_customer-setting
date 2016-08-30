using Fanex.CustomerSetting.Module.Core.Models;

namespace Fanex.Module.NG2.Repository
{
    public interface ING2StatusRepository
    {
        Result SyncStatus(int custId, int userId, string ip);

        Result EnableStatus(int custId, int userId, bool disabled, string ip);
    }
}