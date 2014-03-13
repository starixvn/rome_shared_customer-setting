// --------------------------------------------------------------------------
// <copyright file="IKenoSettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;

    public interface IKenoSettingService
    {
        SuperSettingViewModel GetSuperSetting(int custId);

        IList<Result> UpdateSuperCustomerSetting(SuperSettingViewModel superSetting);

        MasterSettingViewModel GetMasterSetting(int custId);

        IList<Result> UpdateMasterCustomerSetting(MasterSettingViewModel masterSetting);

        AgentSettingViewModel GetAgentSetting(int custId);

        IList<Result> UpdateAgentCustomerSetting(AgentSettingViewModel agentSetting);

        MemberSettingViewModel GetSettingInCaseOfAgentEditMember(int custId);

        IList<Result> UpdateMemberCustomerSettingInCaseOfAgentEditMember(MemberSettingViewModel memberSetting);

        MemberSettingViewModel GetSettingInCaseOfSuperEditMember(int custId);

        IList<Result> UpdateMemberCustomerSettingInCaseOfSuperEditMember(MemberSettingViewModel memberSetting);

        MemberSettingViewModel GetSettingInCaseOfMasterEditMember(int custId);

        IList<Result> UpdateMemberCustomerSettingInCaseOfMasterEditMember(MemberSettingViewModel memberSetting);
    }
}