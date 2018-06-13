namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using Entities;
    using ViewModels;

    public interface MemberServices
    {
        CasinoSettingModel MemberCasinoModel(int custId, int uplineId);

        CasinoSettingModel MemberCasinoModelBySuper(int custId, int uplineId);

        CasinoSettingModel MemberCasinoModelByMaster(int custId, int uplineId);

        Result UpdateMember(
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

        Result UpdateMemberBySuper(
            int custId,
            int uplineId,
            string ip,
            int modifiedId,
            string modifiedBy,
            int fromSystem,
            byte systemId,
            CasinoSettingModel settingModel,
            GeneralCasinoCustomer customerModel);

        Result UpdateMemberByMaster(
            int custId,
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