// --------------------------------------------------------------------------
// <copyright file="IKenoSettingRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Repositories
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using Fanex.NAP.Framework.Data;

    public interface IKenoSettingRepository
    {
        SuperSettingViewModel GetSuperSetting(ICriteria criteria);

        IList<Result> UpdateSuperBetSetting(ICriteria criteria);

        IList<Result> UpdateSuperPositionTaking(ICriteria criteria);

        MasterSettingViewModel GetMasterSetting(ICriteria criteria);

        IList<Result> UpdateMasterBetSetting(ICriteria criteria);

        IList<Result> UpdateMasterPositionTaking(ICriteria criteria);

        AgentSettingViewModel GetAgentSetting(ICriteria criteria);

        IList<Result> UpdateAgentBetSetting(ICriteria criteria);

        IList<Result> UpdateAgentPositionTaking(ICriteria criteria);

        MemberSettingViewModel GetSettingInCaseOfAgentEditMember(ICriteria criteria);

        MemberSettingViewModel GetSettingInCaseOfSuperEditMember(ICriteria criteria);

        MemberSettingViewModel GetSettingInCaseOfMasterEditMember(ICriteria criteria);

        IList<Result> UpdateMemberPositionTakingInCaseOfSuperEditMember(ICriteria criteria);

        IList<Result> UpdateMemberPositionTakingInCaseOfMasterEditMember(ICriteria criteria);

        IList<Result> UpdateMemberPositionTakingInCaseOfAgentEditMember(ICriteria criteria);
    }
}
