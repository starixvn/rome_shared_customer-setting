namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    using Core.Models;
    using Fanex.NAP.Framework.Data;

    public interface IDCSSettingCopyInsertMemberService
    {
        Result CorrectMemberPositionTaking(ICriteria criteria);

        bool CheckMemberPositionTakingConstrain(ICriteria criteria);
    }
}