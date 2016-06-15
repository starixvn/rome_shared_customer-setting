namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    using Core.Models;
    using Fanex.NAP.Framework.Data;
    using Repository.Impl;
    using Repository.Interface;

    public class DCSSettingCopyInsertMemberService : IDCSSettingCopyInsertMemberService
    {
        private readonly IDCSSettingCopyInsertMemberRepository _repository;

        public DCSSettingCopyInsertMemberService(IDCSSettingCopyInsertMemberRepository repository = null)
        {
            _repository = repository ?? new DCSSettingCopyInsertMemberRepository();
        }

       public Result CorrectMemberPositionTaking(ICriteria criteria)
        {
            return _repository.CorrectMemberPositionTaking(criteria);
        }

       public bool CheckMemberPositionTakingConstrain(ICriteria criteria)
       {
           return _repository.CheckMemberPositionTakingConstrain(criteria);
       }
    }
}
