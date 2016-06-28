// --------------------------------------------------------------------------
// <copyright file="AgentEditMemberSettingViewModel.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.ViewModels
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;

    /// <summary>
    /// The AgentEditMemberSetting view model.
    /// </summary>
    public class AgentEditMemberSettingViewModel : CustomerSettingBase<AgentEditMemberPositionTaking>
    {
        internal override void CopyToOnePositionTaking(
            AgentEditMemberPositionTaking sourceMemberPositionTaking, 
            AgentEditMemberPositionTaking destinationMemberPositionTaking)
        {
            destinationMemberPositionTaking.IsUpdateA1 = destinationMemberPositionTaking.A1 != sourceMemberPositionTaking.A1;
            destinationMemberPositionTaking.A1 = sourceMemberPositionTaking.A1;
        }

        internal override void ChangeSettingWhenMultiple(AgentEditMemberPositionTaking positionTaking, bool isMultiple)
        {
            if (isMultiple)
            {
                positionTaking.IsUpdateA1 = true;
            }
        }
    }
}
