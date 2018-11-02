namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using Entities;

    public interface MemberWinLimitPartialService
    {
        Result UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit, byte roleId);
    }
}