namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using Entities;

    public interface MemberWinLimitService
    {
        MemberWinLimit GetMemberWinLimit(int custId);

        CurrentMemberWinLoss GetCurrentWinLimit(int custId);

        Result UpdateMemberWinLimit(
            int custId,
            int userId,
            int subAccId,
            string ip,
            string language,
            MemberWinLimit memberWinLimit);
    }
}