// --------------------------------------------------------------------------
// <copyright file="MemberSettingViewModel.cs" company="Nexcel Solutions Vietnam">
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
    /// The MemberSetting view model.
    /// </summary>
    public class MemberSettingViewModel : CustomerSettingBase
    {
        public IList<MemberPositionTaking> PositionTakings { get; set; }

        internal static void CopyToOnePositionTaking(MemberPositionTaking copiedMemberPositionTaking, MemberPositionTaking memberPositionTaking)
        {
            memberPositionTaking.S4 = copiedMemberPositionTaking.S4;
            memberPositionTaking.N4 = copiedMemberPositionTaking.N4;
            memberPositionTaking.AComm = copiedMemberPositionTaking.AComm;
        }

        /// <summary>
        /// Updates the position takings in case of copy all.
        /// </summary>
        internal override void UpdatePositionTakingsInCaseOfCopyAll()
        {
            if (this.CopyAllIsValidForPositionTaking())
            {
                var copiedMemberPositionTaking = this.PositionTakings.Where(p => p.BetTypeId == this.PositionTakingCopyAll.BettypeId).FirstOrDefault();

                if (copiedMemberPositionTaking == null)
                {
                    return;
                }

                foreach (var memberPositionTaking in this.PositionTakings)
                {
                    if (IsCopyToItself(memberPositionTaking.BetTypeId, this.PositionTakingCopyAll.BettypeId))
                    {
                        continue;
                    }

                    CopyToOnePositionTaking(copiedMemberPositionTaking, memberPositionTaking);
                }
            }
        }

        internal bool CopyAllIsValidForPositionTaking()
        {
            return this.PositionTakingCopyAll != null && this.PositionTakingCopyAll.IsCopiedAll && this.PositionTakings != null && this.PositionTakings.Count > 0;
        }
    }
}
