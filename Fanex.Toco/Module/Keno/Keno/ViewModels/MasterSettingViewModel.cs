// --------------------------------------------------------------------------
// <copyright file="MasterSettingViewModel.cs" company="Nexcel Solutions Vietnam">
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
    /// The MasterSetting view model.
    /// </summary>
    public class MasterSettingViewModel : CustomerSettingBase
    {
        public IList<MasterPositionTaking> PositionTakings { get; set; }

        internal static void CopyToOnePositionTaking(MasterPositionTaking copiedMasterPositionTaking, MasterPositionTaking masterPositionTaking)
        {
            masterPositionTaking.IS3 = copiedMasterPositionTaking.IS3;
            masterPositionTaking.S3 = copiedMasterPositionTaking.S3;
            masterPositionTaking.US3 = copiedMasterPositionTaking.US3;
            masterPositionTaking.S4 = copiedMasterPositionTaking.S4;
            masterPositionTaking.M3 = copiedMasterPositionTaking.M3;
            masterPositionTaking.N3 = copiedMasterPositionTaking.N3;
            masterPositionTaking.MComm = copiedMasterPositionTaking.MComm;
            masterPositionTaking.SComm = copiedMasterPositionTaking.SComm;
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

                foreach (var masterPositionTaking in this.PositionTakings)
                {
                    if (IsCopyToItself(masterPositionTaking.BetTypeId, this.PositionTakingCopyAll.BettypeId))
                    {
                        continue;
                    }

                    CopyToOnePositionTaking(copiedSuperPositionTaking, masterPositionTaking);
                }
            }
        }

        internal bool CopyAllIsValidForPositionTaking()
        {
            return this.PositionTakingCopyAll != null && this.PositionTakingCopyAll.IsCopiedAll && this.PositionTakings != null && this.PositionTakings.Count > 0;
        }
    }
}
