// --------------------------------------------------------------------------
// <copyright file="SuperSettingViewModel.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.ViewModels
{
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;

    /// <summary>
    /// The SuperSetting view model.
    /// </summary>
    public class SuperSettingViewModel : CustomerSettingBase<SuperPositionTaking>
    {
        internal override void CopyToOnePositionTaking(SuperPositionTaking sourceSuperPositionTaking, SuperPositionTaking destinationSuperPositionTaking)
        {
            destinationSuperPositionTaking.IsUpdateS4 = destinationSuperPositionTaking.S4 != sourceSuperPositionTaking.S4;
            destinationSuperPositionTaking.IsUpdateN4 = destinationSuperPositionTaking.N4 != sourceSuperPositionTaking.N4;

            destinationSuperPositionTaking.S4 = sourceSuperPositionTaking.S4;
            destinationSuperPositionTaking.N4 = sourceSuperPositionTaking.N4;
        }

        internal override void ChangeSettingWhenMultiple(SuperPositionTaking positionTaking, bool isMultiple)
        {
            if (isMultiple)
            {
                positionTaking.IsUpdateS4 = true;
                positionTaking.IsUpdateN4 = true;
            }
        }

        public byte? SystemId { get; set; }
    }
}
