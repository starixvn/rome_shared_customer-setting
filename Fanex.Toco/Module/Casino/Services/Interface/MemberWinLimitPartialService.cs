namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using Entities;
    using System.Collections.Generic;

    public interface MemberWinLimitPartialService
    {
        Result UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit, byte roleId);

        IList<Result> MultipleCopySuperMemberWinLimit(int custId, List<int> custIds, int userId);
    }
}