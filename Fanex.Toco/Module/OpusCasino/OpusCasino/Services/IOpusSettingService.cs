// -----------------------------------------------------------------------
// <copyright file="IOpusSettingService.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections;
using Fanex.CustomerSetting.OpusCasino.ViewModels;
using Fanex.CustomerSetting.OpusCasino.Entities;

namespace Fanex.CustomerSetting.Module.Racebook.Services
{
    public interface IOpusSettingService
    {
        OpusSetting SelectCustomerInfo(int custId, int uplineRole, int downlineRole, int actionId);

        OpusSetting UpdateOpusSetting(OpusSetting opusSetting, int uplineRole, int downlineRole, int actionId, int userId, int subAccId, string ip, string language);

        Result UpdateSuper(OpusSetting opusSetting);
    }
}
