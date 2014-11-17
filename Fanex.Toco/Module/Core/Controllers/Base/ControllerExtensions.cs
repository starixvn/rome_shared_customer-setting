// --------------------------------------------------------------------------
// <copyright file="ControllerExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.4.5">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Controllers.Base
{
    using System.Web.Mvc;
    using Newtonsoft.Json;

    /// <summary>
    /// The ControllerExtensions class.
    /// </summary>
    public static class ControllerExtensions
    {
        public static JsonBase JsonExt(this Controller controller, object data, JsonSerializerSettings settings = null)
        {
            return new JsonBase(data, settings);
        }
    }
}