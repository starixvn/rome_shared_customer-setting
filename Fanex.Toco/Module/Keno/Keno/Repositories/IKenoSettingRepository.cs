// --------------------------------------------------------------------------
// <copyright file="IKenoSettingRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Repositories
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using Fanex.NAP.Framework.Data;
   
    public interface IKenoSettingRepository
    {
        SuperSetting GetSuperSetting(ICriteria criteria);

        Result UpdateSuperBetSetting(ICriteria criteria);

        Result UpdateSuperPositionTaking(ICriteria criteria);
    }
}
