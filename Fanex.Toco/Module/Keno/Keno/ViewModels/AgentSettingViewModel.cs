// --------------------------------------------------------------------------
// <copyright file="AgentSettingViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.ViewModels
{
    using Fanex.CustomerSetting.Module.Keno.Entities;

    /// <summary>
    /// The AgentSetting view model.
    /// </summary>
    public class AgentSettingViewModel : CustomerSettingBase
    {
        /// <summary>
        /// Copies to one position taking.
        /// </summary>
        /// <param name="sourceAgentPositionTaking">The source agent position taking.</param>
        /// <param name="destinationAgentPositionTaking">The destination agent position taking.</param>
        internal override void CopyToOnePositionTaking(dynamic sourceAgentPositionTaking, dynamic destinationAgentPositionTaking)
        {
            destinationAgentPositionTaking.M2 = sourceAgentPositionTaking.M2;
            destinationAgentPositionTaking.UM2 = sourceAgentPositionTaking.UM2;
            destinationAgentPositionTaking.IM2 = sourceAgentPositionTaking.IM2;
            destinationAgentPositionTaking.A2 = sourceAgentPositionTaking.A2;
            destinationAgentPositionTaking.N2 = sourceAgentPositionTaking.N2;
            destinationAgentPositionTaking.IsUpdateAllMembers = sourceAgentPositionTaking.IsUpdateAllMembers;
            destinationAgentPositionTaking.NewMemberPT = sourceAgentPositionTaking.NewMemberPT;
            destinationAgentPositionTaking.AComm = sourceAgentPositionTaking.AComm;
            destinationAgentPositionTaking.IsUpdateA2 = sourceAgentPositionTaking.IsUpdateA2;
            destinationAgentPositionTaking.IsUpdateN2 = sourceAgentPositionTaking.IsUpdateN2;
            destinationAgentPositionTaking.IsUpdateM2 = sourceAgentPositionTaking.IsUpdateM2;
            destinationAgentPositionTaking.IsUpdateUM2 = sourceAgentPositionTaking.IsUpdateUM2;
            destinationAgentPositionTaking.IsUpdateACO = sourceAgentPositionTaking.IsUpdateACO;
            destinationAgentPositionTaking.IsUpdateA2 = sourceAgentPositionTaking.IsUpdateA2;
        }
    }
}
