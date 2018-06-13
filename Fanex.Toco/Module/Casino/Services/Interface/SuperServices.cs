namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using ViewModels;

    public interface SuperServices
    {
        CasinoSettingModel SuperCasinoModel(int custId, int uplineId);

        Result UpdateSuper(UpdateCasinoCommand command);
    }
}