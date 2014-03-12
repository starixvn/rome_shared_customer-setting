// --------------------------------------------------------------------------
// <copyright file="SuperSettingViewModel.cs" company="Nexcel Solutions Vietnam">
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
    /// The SuperSetting view model.
    /// </summary>
    public class SuperSettingViewModel : CustomerSettingBase
    {
        public IList<SuperPositionTaking> PositionTakings { get; set; }

        internal static void CopyToOnePositionTaking(SuperPositionTaking copiedSuperPositionTaking, SuperPositionTaking superPositionTaking)
        {
            superPositionTaking.S4 = copiedSuperPositionTaking.S4;
            superPositionTaking.N4 = copiedSuperPositionTaking.N4;
            superPositionTaking.AComm = copiedSuperPositionTaking.AComm;
            superPositionTaking.IsUpdatePT = copiedSuperPositionTaking.IsUpdatePT;
            superPositionTaking.IsUpdateS4 = copiedSuperPositionTaking.IsUpdateS4;
            superPositionTaking.IsUpdateN4 = copiedSuperPositionTaking.IsUpdateN4;
            superPositionTaking.IsUpdateSCo = copiedSuperPositionTaking.IsUpdateSCo;
        }

        /// <summary>
        /// Updates the position takings in case of copy all.
        /// </summary>
        internal override void UpdatePositionTakingsInCaseOfCopyAll()
        {
            if (this.CopyAllIsValidForPositionTaking())
            {
                var copiedSuperPositionTaking = this.PositionTakings.Where(p => p.BetTypeId == this.PositionTakingCopyAll.BettypeId).FirstOrDefault();

                if (copiedSuperPositionTaking == null)
                {
                    return;
                }

                foreach (var superPositionTaking in this.PositionTakings)
                {
                    if (IsCopyToItself(superPositionTaking.BetTypeId, this.PositionTakingCopyAll.BettypeId))
                    {
                        continue;
                    }

                    CopyToOnePositionTaking(copiedSuperPositionTaking, superPositionTaking);
                }
            }
        }

        internal bool CopyAllIsValidForPositionTaking()
        {
            return this.PositionTakingCopyAll.IsCopiedAll && this.PositionTakings != null && this.PositionTakings.Count > 0;
        }
    }
}
