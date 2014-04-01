// --------------------------------------------------------------------------
// <copyright file="IKenoSettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.ViewModels;

    public interface IKenoSettingService
    {
        SuperSettingViewModel GetSuperSetting(int custId);

        IList<Result> UpdateSuperCustomerSetting(SuperSettingViewModel superSetting);

        MasterSettingViewModel GetMasterSetting(int custId);

        IList<Result> UpdateMasterCustomerSetting(MasterSettingViewModel masterSetting);

        AgentSettingViewModel GetAgentSetting(int custId);

        IList<Result> UpdateAgentCustomerSetting(AgentSettingViewModel agentSetting);

        AgentEditMemberSettingViewModel GetSettingInCaseOfAgentEditMember(int custId);

        IList<Result> UpdateMemberCustomerSettingInCaseOfAgentEditMember(AgentEditMemberSettingViewModel memberSetting);

        SuperEditMemberSettingViewModel GetSettingInCaseOfSuperEditMember(int custId);

        IList<Result> UpdateMemberCustomerSettingInCaseOfSuperEditMember(SuperEditMemberSettingViewModel memberSetting);

        MasterEditMemberSettingViewModel GetSettingInCaseOfMasterEditMember(int custId);

        IList<Result> UpdateMemberCustomerSettingInCaseOfMasterEditMember(MasterEditMemberSettingViewModel memberSetting);
    }
}