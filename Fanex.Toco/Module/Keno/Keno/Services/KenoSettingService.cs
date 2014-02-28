// --------------------------------------------------------------------------
// <copyright file="CustomerSetting.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using Fanex.Common;
    using Fanex.NAP.Framework.Data;
    using Newtonsoft.Json;
    using Fanex.CustomerSetting.Module.Keno.Criteria;
    using Fanex.CustomerSetting.Module.Keno.Repositories;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;

    /// <summary>
    /// Customer Service.
    /// </summary>
    public class KenoSettingService : IKenoSettingService
    {
        private readonly IKenoSettingRepository _kenoSettingRepository;

        public KenoSettingService()
            : this(null)
        {
        }

        public KenoSettingService(IKenoSettingRepository kenoSettingRepository)
        {
            _kenoSettingRepository = kenoSettingRepository;
        }

        public SuperSetting GetSuperSetting(int custId)
        {
            var criteria = new GetSuperSettingCriteria();
            return _kenoSettingRepository.GetSuperSetting(criteria);
        }
    }
}