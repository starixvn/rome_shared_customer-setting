namespace Fanex.CustomerSetting.Module.Casino.Repositories.Interface
{
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IMemberWinLimitRepository
    {
        MemberWinLimit GetMemberWinLimit(int custId);

        CurrentMemberWinLimit GetCurrentWinLimit(int custId);

        Result UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit);
    }
}