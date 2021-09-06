namespace Fanex.CustomerSetting.Module.Casino.Services.Interface
{
    using Core.Models;
    using Entities;
    using System.Collections.Generic;

    public interface MemberWinLimitPartialService
    {
        Result UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit, byte roleId, string sourceUserName = null);

        IList<CasinoResult> MultipleCopySuperMemberWinLimit(int custId, List<int> custIds, int userId, string sourceUserName);
    }
}