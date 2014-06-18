// --------------------------------------------------------------------------
// <copyright file="AgentSettingViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
    /// The AgentSetting view model.
    /// </summary>
    public class AgentSettingViewModel : CustomerSettingBase<AgentPositionTaking>
    {
        /// <summary>
        /// Copies to one position taking.
        /// </summary>
        /// <param name="sourceAgentPositionTaking">The source agent position taking.</param>
        /// <param name="destinationAgentPositionTaking">The destination agent position taking.</param>
        internal override void CopyToOnePositionTaking(AgentPositionTaking sourceAgentPositionTaking, AgentPositionTaking destinationAgentPositionTaking)
        {
            destinationAgentPositionTaking.IsUpdateA2 = destinationAgentPositionTaking.A2 != sourceAgentPositionTaking.A2;
            destinationAgentPositionTaking.IsUpdateN2 = destinationAgentPositionTaking.N2 != sourceAgentPositionTaking.N2;
            destinationAgentPositionTaking.IsUpdateM2 = destinationAgentPositionTaking.M2 != sourceAgentPositionTaking.M2;
            destinationAgentPositionTaking.IsUpdateUM2 = (destinationAgentPositionTaking.UM2 != sourceAgentPositionTaking.UM2) || (destinationAgentPositionTaking.IM2 != sourceAgentPositionTaking.IM2);

            destinationAgentPositionTaking.M2 = sourceAgentPositionTaking.M2;
            destinationAgentPositionTaking.UM2 = sourceAgentPositionTaking.UM2;
            destinationAgentPositionTaking.IM2 = sourceAgentPositionTaking.IM2;
            destinationAgentPositionTaking.A2 = sourceAgentPositionTaking.A2;
            destinationAgentPositionTaking.N2 = sourceAgentPositionTaking.N2;
            destinationAgentPositionTaking.IsUpdateAllMember = sourceAgentPositionTaking.IsUpdateAllMember;
            destinationAgentPositionTaking.NewMemberPT = sourceAgentPositionTaking.NewMemberPT;
        }

        internal override void ChangeSettingWhenMultiple(AgentPositionTaking positionTaking, bool isMultiple)
        {
            if (isMultiple)
            {
                positionTaking.IsUpdateA2 = true;
                positionTaking.IsUpdateN2 = true;
                positionTaking.IsUpdateM2 = true;
                positionTaking.IsUpdateUM2 = true;
            }
        }
    }
}
