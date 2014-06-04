// --------------------------------------------------------------------------
// <copyright file="SuperSettingViewModel.cs" company="Nexcel Solutions Vietnam">
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
    /// The SuperSetting view model.
    /// </summary>
    public class SuperSettingViewModel : CustomerSettingBase<SuperPositionTaking>
    {
        internal override void CopyToOnePositionTaking(SuperPositionTaking sourceSuperPositionTaking, SuperPositionTaking destinationSuperPositionTaking)
        {
            destinationSuperPositionTaking.S4 = sourceSuperPositionTaking.S4;
            destinationSuperPositionTaking.N4 = sourceSuperPositionTaking.N4;
            destinationSuperPositionTaking.IsUpdateS4 = true;
            destinationSuperPositionTaking.IsUpdateN4 = true;
        }
    }
}
