namespace Fanex.CustomerSetting.Module.SearchCopy.Repository.Interface
{
    using Core.Models;
    using Fanex.NAP.Framework.Data;

    public interface IDCSSettingCopyInsertMemberRepository
    {
        Result CorrectMemberPositionTaking(ICriteria criteria);

        bool CheckMemberPositionTakingConstrain(ICriteria criteria);
    }
}
