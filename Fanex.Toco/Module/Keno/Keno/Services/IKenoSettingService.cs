// --------------------------------------------------------------------------
// <copyright file="IKenoSettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Services
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;

    public interface IKenoSettingService
    {
        SuperSetting GetSuperSetting(int custId);
    }
}