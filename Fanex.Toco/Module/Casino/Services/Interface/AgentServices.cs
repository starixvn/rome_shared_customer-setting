namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using ViewModels;

    public interface AgentServices
    {
        CasinoSettingModel AgentCasinoModel(int custId, int uplineId);

        CasinoSettingModel AgentCasinoModelBySuper(int custId, int uplineId);

        Result UpdateAgent(UpdateCasinoCommand command);
    }
}