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
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.CustomerSetting.Module.Keno.Entities;

    /// <summary>
    /// The AgentSetting view model.
    /// </summary>
    public class AgentSettingViewModel : CustomerSettingBase
    {
        internal override void CopyToOnePositionTaking(dynamic sourceAgentPositionTaking, dynamic destinationAgentPositionTaking)
        {
            destinationAgentPositionTaking.IM2 = sourceAgentPositionTaking.IM2;
            destinationAgentPositionTaking.M2 = sourceAgentPositionTaking.M2;
            destinationAgentPositionTaking.UM2 = sourceAgentPositionTaking.UM2;
            destinationAgentPositionTaking.M3 = sourceAgentPositionTaking.M3;
            destinationAgentPositionTaking.A2 = sourceAgentPositionTaking.A2;
            destinationAgentPositionTaking.N2 = sourceAgentPositionTaking.N2;
            destinationAgentPositionTaking.AComm = sourceAgentPositionTaking.AComm;
            destinationAgentPositionTaking.MComm = sourceAgentPositionTaking.MComm;
        }
    }
}
