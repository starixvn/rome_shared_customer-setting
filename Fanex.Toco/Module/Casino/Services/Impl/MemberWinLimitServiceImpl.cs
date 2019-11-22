namespace Fanex.CustomerSetting.Module.Casino.Services.Impl
{
    using Core.Models;
    using Entities;
    using Interface;
    using Repositories.Impl;
    using Repositories.Interface;

    public class MemberWinLimitServiceImpl : MemberWinLimitService
    {
        private readonly MemberWinLimitRepository memberWinLimitRepository = new MemberWinLimitRepositoryImpl();

        public MemberWinLimit GetMemberWinLimit(int custId)
            => memberWinLimitRepository.GetMemberWinLimit(custId);

        public CurrentMemberWinLoss GetCurrentWinLimit(int custId)
            => memberWinLimitRepository.GetCurrentWinLimit(custId);

        public Result UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit)
            => memberWinLimitRepository.UpdateMemberWinLimit(custId, userId, subAccId, memberWinLimit);
    }
}