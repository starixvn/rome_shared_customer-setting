using Fanex.Module.NG2.Entities;

namespace Fanex.Module.NG2.Repository
{
    public interface ING2StatusRepository
    {
        Result SyncStatus(int custId, int userId, string ip, int subAccId = 0);

        Result EnableStatus(int custId, int userId, bool disabled, string ip, byte systemId, int subAccId = 0);
    }
}