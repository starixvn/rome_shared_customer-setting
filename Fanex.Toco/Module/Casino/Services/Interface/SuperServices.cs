namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using Entities;
    using ViewModels;

    public interface SuperServices
    {
        CasinoSettingModel SuperCasinoModel(int custId, int uplineId);

        Result UpdateSuper(
            int custId,
            string custName,
            int uplineId,
            string ip,
            int modifiedId,
            string modifiedBy,
            int fromSystem,
            byte systemId,
            CasinoSettingModel settingModel,
            GeneralCasinoCustomer customerModel);
    }
}