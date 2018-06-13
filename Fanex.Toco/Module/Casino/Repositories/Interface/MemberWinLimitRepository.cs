namespace Fanex.CustomerSetting.Module.Casino.Repositories.Interface
{
    using Core.Models;
    using Entities;

    public interface MemberWinLimitRepository
    {
        MemberWinLimit GetMemberWinLimit(int custId);

        CurrentMemberWinLoss GetCurrentWinLimit(int custId);

        Result UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit);
    }
}