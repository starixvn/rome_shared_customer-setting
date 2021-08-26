namespace Fanex.CustomerSetting.Module.Casino.Services.Impl
{
    using Core.Models;
    using Criteria;
    using Entities;
    using Interface;
    using Repositories.Impl;
    using Repositories.Interface;
    using System.Collections.Generic;

    public class MemberWinLimitPartialServiceImpl : MemberWinLimitPartialService
    {
        private readonly MemberWinLimitRepository repository;
        private readonly CasinoRepository casinoRepository;
        private readonly CasinoServiceBase casinoService;

        public MemberWinLimitPartialServiceImpl() : this(null, null, null)
        {
        }

        internal MemberWinLimitPartialServiceImpl(MemberWinLimitRepository repository, CasinoRepository casinoRepository, CasinoServiceBase casinoService)
        {
            this.repository = repository ?? new MemberWinLimitRepositoryImpl();
            this.casinoRepository = casinoRepository ?? new CasinoRepositoryImpl();
            this.casinoService = casinoService ?? new CasinoServiceBaseImpl();
        }

        public Result UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit, byte roleId)
        {
            var checkUmResult = casinoService.CheckUM(custId);
            if (!checkUmResult.IsSuccess)
            {
                return checkUmResult;
            }

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

        public IList<CasinoResult> MultipleCopySuperMemberWinLimit(int custId, List<int> custIds, int userId)
        {
            var results = new List<CasinoResult>();
            var memberWinLimit = repository.GetMemberWinLimit(custId);
            foreach (var toCustIds in custIds)
            {
                var result = UpdateMemberWinLimit(toCustIds, userId, 0, memberWinLimit, 4);
                if (result.ErrorCode > 0)
                {
                    results.Add(new CasinoResult(toCustIds, result));
                }
            }
            return results;
        }
    }
}