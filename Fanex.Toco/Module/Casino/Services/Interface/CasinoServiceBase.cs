namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using Entities;

    public interface CasinoServiceBase
    {
        CasinoCustomer GetCustInfoIBC(int custid, int uplineId);

        CasinoCustomer GetCustSettingCS(CasinoCustomer custIBCModel);

        Result UpdateCSStatus(
            int custid,
            bool syncCS,
            bool disabledCS,
            int userId,
            string ip,
            int subAccId,
            byte systemId,
            string lang,
            string username,
            string subUsername);
    }

}