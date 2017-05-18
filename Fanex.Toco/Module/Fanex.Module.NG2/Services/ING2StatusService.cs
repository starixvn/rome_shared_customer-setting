using Fanex.Module.NG2.Entities;

namespace Fanex.Module.NG2.Services
{
    public interface ING2StatusService
    {
        Result SyncStatus(int custId, int userId, string ip, int subAccId = 0);

        Result EnableStatus(int custId, int userId, bool disabled, string ip, byte systemId, int subAccId = 0);

        Result AutoInsertAccount(
            int custId, 
            string username, 
            RoleId roleId, 
            int uplineId, 
            bool uplineDisabled, 
            SiteId siteId, 
            int currencyId, 
            int userId,
            byte systemId,
            string language = "en-US", 
            int subAccId = 0);

        Result RetrySyncCustomers(ONEbook.NG2.API.DTO.CreateAccount.Request request);
    }
}