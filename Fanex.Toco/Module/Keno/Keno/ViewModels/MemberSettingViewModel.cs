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
    using System;
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Keno.Entities;

    /// <summary>
    /// The MemberSetting view model.
    /// </summary>
    public class MemberSettingViewModel : CustomerSettingBase
    {
        public IList<MemberPositionTaking> PositionTakings { get; set; }
    }
}
