// --------------------------------------------------------------------------
// <copyright file="IKenoSettingService.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.ViewModels;

    public interface IKenoSettingService
    {
        SuperSettingViewModel GetSuperSetting(CustomerInformationViewModel customerInformationViewModel);

        IList<Result> UpdateSuperCustomerSetting(SuperSettingViewModel superSetting);

        MasterSettingViewModel GetMasterSetting(CustomerInformationViewModel customerInformationViewModel);

        IList<Result> UpdateMasterCustomerSetting(MasterSettingViewModel masterSetting);

        AgentSettingViewModel GetAgentSetting(CustomerInformationViewModel customerInformationViewModel);

        IList<Result> UpdateAgentCustomerSetting(AgentSettingViewModel agentSetting);

        AgentEditMemberSettingViewModel GetSettingInCaseOfAgentEditMember(CustomerInformationViewModel customerInformationViewModel);

        IList<Result> UpdateMemberCustomerSettingInCaseOfAgentEditMember(AgentEditMemberSettingViewModel memberSetting, bool cloneSetting = false);

        SuperEditMemberSettingViewModel GetSettingInCaseOfSuperEditMember(CustomerInformationViewModel customerInformationViewModel);

        IList<Result> UpdateMemberCustomerSettingInCaseOfSuperEditMember(SuperEditMemberSettingViewModel memberSetting);

        MasterEditMemberSettingViewModel GetSettingInCaseOfMasterEditMember(CustomerInformationViewModel customerInformationViewModel);

        IList<Result> UpdateMemberCustomerSettingInCaseOfMasterEditMember(MasterEditMemberSettingViewModel memberSetting);

        IList<Result> UpdateBetSetting(BetSetting betSetting);

        IList<Result> UpdateBetSettingSuper(BetSetting betSetting, int custId, int userId);
    }
}