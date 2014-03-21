// --------------------------------------------------------------------------
// <copyright file="ICriteriaGeneratorService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Services
{
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.NAP.Framework.Data;

    public interface ICriteriaGeneratorService
    {
        IKenoSession KenoSession { get; }

        ICriteria CreateUpdateBetSettingCriteria(CustomerInformation customerInformation, BetSetting betSetting);

        ICriteria CreateUpdatePositionTakingCriteria(CustomerInformation customerInformation, dynamic positionTaking);
    }
}