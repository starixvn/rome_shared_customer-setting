namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using ViewModels;

    public interface SuperServices
    {
        CasinoSettingModel SuperCasinoModel(int custId, int uplineId);

        Result UpdateSuper(UpdateCasinoCommand command);

        Result UpdateSuperBettingLimit(
            int custId,
            int roleId,
            string ip,
            int modifiedId,
            string modifiedBy,
            int fromSystem,
            CasinoSettingModel model);
    }
}