// --------------------------------------------------------------------------
// <copyright file="MasterSettingViewModel.cs" company="Nexcel Solutions Vietnam">
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
    /// The MasterSetting view model.
    /// </summary>
    public class MasterSettingViewModel : CustomerSettingBase<MasterPositionTaking>
    {
        /// <summary>
        /// Copies to one position taking.
        /// </summary>
        /// <param name="sourceMasterPositionTaking">The source master position taking.</param>
        /// <param name="destinationMasterPositionTaking">The destination master position taking.</param>
        internal override void CopyToOnePositionTaking(MasterPositionTaking sourceMasterPositionTaking, MasterPositionTaking destinationMasterPositionTaking)
        {
            destinationMasterPositionTaking.S3 = sourceMasterPositionTaking.S3;
            destinationMasterPositionTaking.US3 = sourceMasterPositionTaking.US3;
            destinationMasterPositionTaking.IS3 = sourceMasterPositionTaking.IS3;
            destinationMasterPositionTaking.M3 = sourceMasterPositionTaking.M3;
            destinationMasterPositionTaking.N3 = sourceMasterPositionTaking.N3;
            destinationMasterPositionTaking.IsUpdateAllMember = sourceMasterPositionTaking.IsUpdateAllMember;
            destinationMasterPositionTaking.NewMemberPT = sourceMasterPositionTaking.NewMemberPT;
            destinationMasterPositionTaking.IsUpdateM3 = sourceMasterPositionTaking.IsUpdateM3;
            destinationMasterPositionTaking.IsUpdateN3 = sourceMasterPositionTaking.IsUpdateN3;
            destinationMasterPositionTaking.IsUpdateUS3 = sourceMasterPositionTaking.IsUpdateUS3;
        }
    }
}
