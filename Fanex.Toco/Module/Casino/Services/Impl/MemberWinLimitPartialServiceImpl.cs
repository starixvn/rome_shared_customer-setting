namespace Fanex.CustomerSetting.Module.Casino.Services.Impl
{
    using Core.Models;
    using Criteria;
    using Entities;
    using Interface;
    using Repositories.Impl;
    using Repositories.Interface;

    public class MemberWinLimitPartialServiceImpl : MemberWinLimitPartialService
    {
        private readonly MemberWinLimitRepository repository;
        private readonly CasinoRepository casinoRepository;

        public MemberWinLimitPartialServiceImpl() : this(null, null) { }

        internal MemberWinLimitPartialServiceImpl(MemberWinLimitRepository repository, CasinoRepository casinoRepository)
        {
            this.repository = repository ?? new MemberWinLimitRepositoryImpl();
            this.casinoRepository = casinoRepository ?? new CasinoRepositoryImpl();
        }

        public Result UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit, byte roleId)
        {
            if (roleId != 1)
            {
                var allowDownlines = casinoRepository.ExecuteNonQuery(new AllowedDownlinesCriteria(custId, roleId));
                if (!allowDownlines.IsSuccess)
                {
                    return allowDownlines;
                }
            }

            return repository.UpdateMemberWinLimit(custId, userId, subAccId, memberWinLimit);
        }
    }
}