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
        public IList<AgentPositionTaking> PositionTakings { get; set; }

        internal static void CopyToOnePositionTaking(AgentPositionTaking copiedAgentPositionTaking, AgentPositionTaking agentPositionTaking)
        {
            agentPositionTaking.IM2 = copiedAgentPositionTaking.IM2;
            agentPositionTaking.M2 = copiedAgentPositionTaking.M2;
            agentPositionTaking.UM2 = copiedAgentPositionTaking.UM2;
            agentPositionTaking.M3 = copiedAgentPositionTaking.M3;
            agentPositionTaking.A2 = copiedAgentPositionTaking.A2;
            agentPositionTaking.N2 = copiedAgentPositionTaking.N2;
            agentPositionTaking.AComm = copiedAgentPositionTaking.AComm;
            agentPositionTaking.MComm = copiedAgentPositionTaking.MComm;
        }

        /// <summary>
        /// Updates the position takings in case of copy all.
        /// </summary>
        internal override void UpdatePositionTakingsInCaseOfCopyAll()
        {
            if (this.CopyAllIsValidForPositionTaking())
            {
                var copiedAgentPositionTaking = this.PositionTakings.Where(p => p.BetTypeId == this.PositionTakingCopyAll.BettypeId).FirstOrDefault();

                if (copiedAgentPositionTaking == null)
                {
                    return;
                }

                foreach (var agentPositionTaking in this.PositionTakings)
                {
                    if (IsCopyToItself(agentPositionTaking.BetTypeId, this.PositionTakingCopyAll.BettypeId))
                    {
                        continue;
                    }

                    CopyToOnePositionTaking(copiedAgentPositionTaking, agentPositionTaking);
                }
            }
        }

        internal bool CopyAllIsValidForPositionTaking()
        {
            return this.PositionTakingCopyAll != null && this.PositionTakingCopyAll.IsCopiedAll && this.PositionTakings != null && this.PositionTakings.Count > 0;
        }
    }
}
