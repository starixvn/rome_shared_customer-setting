// --------------------------------------------------------------------------
// <copyright file="SuperEditMemberSettingViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.ViewModels
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Keno.Entities;

    /// <summary>
    /// The SuperEditMemberSetting view model.
    /// </summary>
    public class SuperEditMemberSettingViewModel : CustomerSettingBase<SuperEditMemberPositionTaking>
    {
        internal override void CopyToOnePositionTaking(dynamic sourceMemberPositionTaking, dynamic destinationMemberPositionTaking)
        {
            destinationMemberPositionTaking.S1 = sourceMemberPositionTaking.S1;
            destinationMemberPositionTaking.IS1 = sourceMemberPositionTaking.IS1;
            destinationMemberPositionTaking.US1 = sourceMemberPositionTaking.US1;
            destinationMemberPositionTaking.IsUpdateS1 = sourceMemberPositionTaking.IsUpdateS1;
            destinationMemberPositionTaking.IsUpdateUS1 = sourceMemberPositionTaking.IsUpdateUS1;
        }
    }
}
