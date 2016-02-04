namespace Fanex.CustomerSetting.Module.Casino.Services.Impl
{
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Repositories.Impl;
    using Fanex.CustomerSetting.Module.Casino.Repositories.Interface;
    using Fanex.CustomerSetting.Module.Casino.Services.Interface;
    using Fanex.CustomerSetting.Module.Core.Models;

    public class CasinoMemberWinLimitService : ICasinoMemberWinLimitService
    {
        private IMemberWinLimitRepository _data = new MemberWinLimitRepository();

        public MemberWinLimit GetMemberWinLimit(int custId)
        {
            return _data.GetMemberWinLimit(custId);
        }

        public CurrentMemberWinLimit GetCurrentWinLimit(int custId)
        {
            return _data.GetCurrentWinLimit(custId);
        }

        public Result UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit)
        {
            return _data.UpdateMemberWinLimit(custId, userId, subAccId, memberWinLimit);
        }
    }
}