// --------------------------------------------------------------------------
// <copyright file="AgentEditMemberSettingViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.ViewModels
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Keno.Entities;

    /// <summary>
    /// The AgentEditMemberSetting view model.
    /// </summary>
    public class AgentEditMemberSettingViewModel : CustomerSettingBase<AgentEditMemberPositionTaking>
    {
        internal override void CopyToOnePositionTaking(
            AgentEditMemberPositionTaking sourceMemberPositionTaking, 
            AgentEditMemberPositionTaking destinationMemberPositionTaking)
        {
            destinationMemberPositionTaking.A1 = sourceMemberPositionTaking.A1;
            destinationMemberPositionTaking.IsUpdateA1 = sourceMemberPositionTaking.IsUpdateA1;
            destinationMemberPositionTaking.IsUpdateComm = sourceMemberPositionTaking.IsUpdateComm;
            destinationMemberPositionTaking.Comm = sourceMemberPositionTaking.Comm;
        }
    }
}
