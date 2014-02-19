// ************************************************************************************
// <copyright file="OpusSettingMaster.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="Thai.Tran">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.OpusCasino.ViewModels
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// OpusSettingSuper Class.
    /// </summary>
    public class OpusSettingMaster : OpusSetting
    {
        public OpusSettingMaster()
        {
            this.PositionTaking = new PositionTakingMaster();
        }

        public PositionTakingMaster PositionTaking { get; set; }
    }
}
