namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using ViewModels;

    public interface MemberServices
    {
        CasinoSettingModel MemberCasinoModel(int custId, int uplineId);

        CasinoSettingModel MemberCasinoModelBySuper(int custId, int uplineId);

        CasinoSettingModel MemberCasinoModelByMaster(int custId, int uplineId);

        Result UpdateMember(UpdateCasinoCommand command);

        Result UpdateMemberBySuper(UpdateCasinoCommand command);

        Result UpdateMemberByMaster(UpdateCasinoCommand command);

        Result UpdateMemberSettings(
            int custId,
            int roleId,
            string ip,
            int modifiedId,
            string modifiedBy,
            int fromSystem,
            CasinoSettingModel model);
    }
}