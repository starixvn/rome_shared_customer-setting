namespace Fanex.CustomerSetting.Module.Casino.Services.Impl
{
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Repositories.Impl;
    using Fanex.CustomerSetting.Module.Casino.Repositories.Interface;
    using Fanex.CustomerSetting.Module.Casino.Services.Interface;

    public class CasinoMemberWinLimitService : ICasinoMemberWinLimitService
    {
        private IMemberWinLimitRepository data = new MemberWinLimitRepository();

        public MemberWinLimit GetMemberWinLimit(int custId)
        {
            return data.GetMemberWinLimit(custId);
        }

        public CurrentMemberWinLimit GetCurrentWinLimit(int custId)
        {
            return data.GetCurrentWinLimit(custId);
        }

        public bool UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit)
        {
            return data.UpdateMemberWinLimit(custId, userId, subAccId, memberWinLimit);
        }
    }
}