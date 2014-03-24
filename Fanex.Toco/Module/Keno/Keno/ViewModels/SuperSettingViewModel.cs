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
        internal override void CopyToOnePositionTaking(dynamic sourceSuperPositionTaking, dynamic destinationSuperPositionTaking)
        {
            destinationSuperPositionTaking.S4 = sourceSuperPositionTaking.S4;
            destinationSuperPositionTaking.N4 = sourceSuperPositionTaking.N4;
            destinationSuperPositionTaking.SComm = sourceSuperPositionTaking.SComm;
            destinationSuperPositionTaking.IsUpdateS4 = sourceSuperPositionTaking.IsUpdateS4;
            destinationSuperPositionTaking.IsUpdateN4 = sourceSuperPositionTaking.IsUpdateN4;
            destinationSuperPositionTaking.IsUpdateSCo = sourceSuperPositionTaking.IsUpdateSCo;
        }
    }
}
