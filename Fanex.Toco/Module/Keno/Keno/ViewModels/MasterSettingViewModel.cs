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
        internal override void CopyToOnePositionTaking(dynamic sourceMasterPositionTaking, dynamic destinationMasterPositionTaking)
        {
            destinationMasterPositionTaking.IS3 = sourceMasterPositionTaking.IS3;
            destinationMasterPositionTaking.S3 = sourceMasterPositionTaking.S3;
            destinationMasterPositionTaking.US3 = sourceMasterPositionTaking.US3;
            destinationMasterPositionTaking.S4 = sourceMasterPositionTaking.S4;
            destinationMasterPositionTaking.M3 = sourceMasterPositionTaking.M3;
            destinationMasterPositionTaking.N3 = sourceMasterPositionTaking.N3;
            destinationMasterPositionTaking.MComm = sourceMasterPositionTaking.MComm;
            destinationMasterPositionTaking.SComm = sourceMasterPositionTaking.SComm;
        }
    }
}
