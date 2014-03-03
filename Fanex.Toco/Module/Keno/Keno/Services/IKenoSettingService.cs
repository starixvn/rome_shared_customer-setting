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
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;

    public interface IKenoSettingService
    {
        SuperSetting GetSuperSetting(int custId);

        Result UpdateSuperBetSetting(CustomerInformation customerInformation, BetSetting betSetting);

        Result UpdateSuperPositionTaking(CustomerInformation customerInformation, SuperPositionTaking superPositionTaking);

        IList<Result> UpdateSuperBetSettings(CustomerInformation customerInformation, IList<BetSetting> betSettings);

        IList<Result> UpdateSuperPositionTakings(CustomerInformation customerInformation, IList<SuperPositionTaking> positionTakings);
    }
}