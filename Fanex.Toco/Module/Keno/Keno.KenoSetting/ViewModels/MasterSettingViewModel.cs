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
            destinationMasterPositionTaking.IsUpdateM3 = destinationMasterPositionTaking.M3 != sourceMasterPositionTaking.M3;
            destinationMasterPositionTaking.IsUpdateN3 = destinationMasterPositionTaking.N3 != sourceMasterPositionTaking.N3;
            destinationMasterPositionTaking.IsUpdateUS3 = (destinationMasterPositionTaking.US3 != sourceMasterPositionTaking.US3) 
                                                          || (destinationMasterPositionTaking.IS3 != sourceMasterPositionTaking.IS3);
            destinationMasterPositionTaking.IsUpdateS3 = (destinationMasterPositionTaking.S3 != sourceMasterPositionTaking.S3)
                                                          || (destinationMasterPositionTaking.IS3 != sourceMasterPositionTaking.IS3);

            destinationMasterPositionTaking.S3 = sourceMasterPositionTaking.S3;
            destinationMasterPositionTaking.US3 = sourceMasterPositionTaking.US3;
            destinationMasterPositionTaking.IS3 = sourceMasterPositionTaking.IS3;
            destinationMasterPositionTaking.M3 = sourceMasterPositionTaking.M3;
            destinationMasterPositionTaking.N3 = sourceMasterPositionTaking.N3;
            destinationMasterPositionTaking.IsUpdateAllMember = sourceMasterPositionTaking.IsUpdateAllMember;
            destinationMasterPositionTaking.NewMemberPT = sourceMasterPositionTaking.NewMemberPT;
        }

        internal override void ChangeSettingWhenMultiple(MasterPositionTaking positionTaking, bool isMultiple)
        {
            if (isMultiple)
            {
                positionTaking.IsUpdateM3 = true;
                positionTaking.IsUpdateN3 = true;
                positionTaking.IsUpdateUS3 = true;
            }
        }
    }
}
