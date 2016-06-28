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
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.NAP.Framework.Data;

    public interface ICriteriaGeneratorService
    {
        IKenoSession KenoSession { get; }

        ICriteria CreateUpdateBetSettingCriteria(BetSetting betSetting);

        ICriteria CreateUpdatePositionTakingCriteria(dynamic positionTaking);
    }
}