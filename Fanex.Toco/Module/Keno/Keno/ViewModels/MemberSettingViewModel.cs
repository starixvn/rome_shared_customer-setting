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
        internal override void CopyToOnePositionTaking(dynamic sourceMemberPositionTaking, dynamic destinationMemberPositionTaking)
        {
            destinationMemberPositionTaking.S4 = sourceMemberPositionTaking.S4;
            destinationMemberPositionTaking.N4 = sourceMemberPositionTaking.N4;
            destinationMemberPositionTaking.AComm = sourceMemberPositionTaking.AComm;
        }
    }
}
