namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using Entities;
    using ViewModels;

    public interface MasterServices
    {
        CasinoSettingModel MasterCasinoModel(int custId, int uplineId);

        Result UpdateMaster(
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