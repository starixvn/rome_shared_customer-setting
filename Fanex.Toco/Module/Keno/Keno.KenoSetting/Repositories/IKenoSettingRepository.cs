// --------------------------------------------------------------------------
// <copyright file="IKenoSettingRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Repositories
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.ViewModels;
    using Fanex.NAP.Framework.Data;

    public interface IKenoSettingRepository
    {
        SuperSettingViewModel GetSuperSetting(ICriteria criteria);

        MasterSettingViewModel GetMasterSetting(ICriteria criteria);

        AgentSettingViewModel GetAgentSetting(ICriteria criteria);

        AgentEditMemberSettingViewModel GetSettingInCaseOfAgentEditMember(ICriteria criteria);

        SuperEditMemberSettingViewModel GetSettingInCaseOfSuperEditMember(ICriteria criteria);

        MasterEditMemberSettingViewModel GetSettingInCaseOfMasterEditMember(ICriteria criteria);

        IList<Result> UpdateBetSetting(ICriteria criteria);

        IList<Result> UpdatePositionTaking(ICriteria criteria);
    }
}
