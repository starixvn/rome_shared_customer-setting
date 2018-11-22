// --------------------------------------------------------------------------
// <copyright file="ICriteriaGeneratorService.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Services
{
    using Data.Repository;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;

    public interface ICriteriaGeneratorService
    {
        IKenoSession KenoSession { get; }

        ICriteria CreateUpdateBetSettingCriteria(BetSetting betSetting, byte? systemId);

        ICriteria CreateUpdatePositionTakingCriteria(dynamic positionTaking);
    }
}