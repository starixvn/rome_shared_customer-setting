namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using ViewModels;

    public interface MasterServices
    {
        CasinoSettingModel MasterCasinoModel(int custId, int uplineId);

        Result UpdateMaster(UpdateCasinoCommand command);
    }
}