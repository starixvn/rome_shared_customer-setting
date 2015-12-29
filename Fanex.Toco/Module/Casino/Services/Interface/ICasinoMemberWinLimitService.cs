namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Fanex.CustomerSetting.Module.Casino.Entities;

    public interface ICasinoMemberWinLimitService
    {
        MemberWinLimit GetMemberWinLimit(int custId);

        CurrentMemberWinLimit GetCurrentWinLimit(int custId);

        bool UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit);
    }
}