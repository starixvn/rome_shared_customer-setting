namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Entities;

    public interface CasinoServiceBase
    {
        CasinoCustomer GetCustInfoIBC(int custid, int uplineId);

        CasinoCustomer GetCustSettingCS(CasinoCustomer custIBCModel);
    }

}