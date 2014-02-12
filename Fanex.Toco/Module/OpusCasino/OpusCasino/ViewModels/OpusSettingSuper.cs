// ************************************************************************************
// <copyright file="OpusSettingSuper.cs" company="Nexcel Solutions Vietnam">
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
    public class OpusSettingSuper : OpusSetting
    {        
        public OpusSettingSuper()
        {            
            this.PositionTaking = new PositionTakingSuper();         
        }

        public PositionTakingSuper PositionTaking { get; set; }
    }
}
