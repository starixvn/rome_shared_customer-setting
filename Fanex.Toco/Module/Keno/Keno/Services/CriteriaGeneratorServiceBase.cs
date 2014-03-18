// --------------------------------------------------------------------------
// <copyright file="CriteriaGeneratorServiceBase.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Services
{
    /// <summary>
    /// Criteria Generator Service.
    /// </summary>
    public abstract class CriteriaGeneratorServiceBase
    {
        private readonly IKenoSession _kenoSession;

        protected CriteriaGeneratorServiceBase(IKenoSession kenoSession)
        {
            _kenoSession = kenoSession ?? new KenoSession();
        }

        protected IKenoSession KenoSession
        {
            get { return _kenoSession; }
        }
    }
}