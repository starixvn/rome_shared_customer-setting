// --------------------------------------------------------------------------
// <copyright file="CustomerSettingBase.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The CustomerSettingBase entity.
    /// </summary>
    public class CustomerSettingBase
    {
        public CustomerInformation CustomerInformation { get; set; }

        public IList<BetSetting> BetSettings { get; set; }

        public CopyAll BetSettingCopyAll { get; set; }

        public CopyAll PositionTakingCopyAll { get; set; }
    }
}