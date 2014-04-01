// --------------------------------------------------------------------------
// <copyright file="MasterEditMemberSettingViewModel.cs" company="Nexcel Solutions Vietnam">
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
    /// The MasterEditMemberSetting view model.
    /// </summary>
    public class MasterEditMemberSettingViewModel : CustomerSettingBase<MasterEditMemberPositionTaking>
    {
        internal override void CopyToOnePositionTaking(
            MasterEditMemberPositionTaking sourceMemberPositionTaking, 
            MasterEditMemberPositionTaking destinationMemberPositionTaking)
        {
            destinationMemberPositionTaking.M1 = sourceMemberPositionTaking.M1;
            destinationMemberPositionTaking.UM1 = sourceMemberPositionTaking.UM1;
            destinationMemberPositionTaking.IM1 = sourceMemberPositionTaking.IM1;
            destinationMemberPositionTaking.IsUpdateM1 = true;
            destinationMemberPositionTaking.IsUpdateUM1 = true;
        }
    }
}
