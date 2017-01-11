namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Core.Models;

    public interface ICasinoMemberWinLimitService
    {
        MemberWinLimit GetMemberWinLimit(int custId);

        CurrentMemberWinLoss GetCurrentWinLimit(int custId);

        Result UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit);
    }
}