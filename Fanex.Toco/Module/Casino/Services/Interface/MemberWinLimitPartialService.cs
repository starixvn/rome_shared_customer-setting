namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using Entities;

    public interface MemberWinLimitPartialService
    {
        Result UpdateMemberWinLimit(
            int custId,
            int userId,
            int subAccId,
            string ip,
            string language,
            MemberWinLimit memberWinLimit,
            byte roleId);
    }
}