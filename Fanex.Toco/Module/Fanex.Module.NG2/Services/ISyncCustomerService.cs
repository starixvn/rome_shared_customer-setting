using Fanex.Module.NG2.Entities;

namespace Fanex.Module.NG2.Services
{
    public interface ISyncCustomerService
    {
        SyncCustomerResult CheckSyncCustomers();

        bool UpdateNotifiedEmail(string logIds);
    }
}