namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using Entities;
    using ViewModels;

    public interface AgentServices
    {
        CasinoSettingModel AgentCasinoModel(int custId, int uplineId);

        CasinoSettingModel AgentCasinoModelBySuper(int custId, int uplineId);

        Result UpdateAgent(
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