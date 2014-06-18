// --------------------------------------------------------------------------
// <copyright file="SuperEditMemberSettingViewModel.cs" company="Nexcel Solutions Vietnam">
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
    /// The SuperEditMemberSetting view model.
    /// </summary>
    public class SuperEditMemberSettingViewModel : CustomerSettingBase<SuperEditMemberPositionTaking>
    {
        internal override void CopyToOnePositionTaking(
            SuperEditMemberPositionTaking sourceMemberPositionTaking, 
            SuperEditMemberPositionTaking destinationMemberPositionTaking)
        {
            destinationMemberPositionTaking.IsUpdateS1 = destinationMemberPositionTaking.S1 != sourceMemberPositionTaking.S1;
            destinationMemberPositionTaking.IsUpdateUS1 = (destinationMemberPositionTaking.US1 != sourceMemberPositionTaking.US1) || (destinationMemberPositionTaking.IS1 != sourceMemberPositionTaking.IS1);

            destinationMemberPositionTaking.S1 = sourceMemberPositionTaking.S1;
            destinationMemberPositionTaking.IS1 = sourceMemberPositionTaking.IS1;
            destinationMemberPositionTaking.US1 = sourceMemberPositionTaking.US1;
        }

        internal override void ChangeSettingWhenMultiple(SuperEditMemberPositionTaking positionTaking, bool isMultiple)
        {
            if (isMultiple)
            {
                positionTaking.IsUpdateS1 = true;
                positionTaking.IsUpdateUS1 = true;
            }
        }
    }
}
