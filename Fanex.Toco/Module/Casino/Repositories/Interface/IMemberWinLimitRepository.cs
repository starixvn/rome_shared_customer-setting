namespace Fanex.CustomerSetting.Module.Casino.Repositories.Interface
{
    using Fanex.CustomerSetting.Module.Casino.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IMemberWinLimitRepository
    {
        MemberWinLimit GetMemberWinLimit(int custId);

        CurrentMemberWinLimit GetCurrentWinLimit(int custId);

        bool UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit);
    }
}