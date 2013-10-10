// ************************************************************************************
// <copyright file="BaseRacingSettingController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Core.Security;
using Fanex.CustomerSetting.Module.Core.Web;
using Fanex.CustomerSetting.Module.Core.Web.Controllers;
using Fanex.CustomerSetting.OpusCasino.App_Start;
using Fanex.CustomerSetting.OpusCasino.Helper;
using Fanex.CustomerSetting.OpusCasino.Services;
using Newtonsoft.Json;
using Fanex.PTRacing.BusinessObjects;

namespace Fanex.CustomerSetting.OpusCasino.Controllers
{
    /// <summary>
    /// Class BaseRacingSettingController.
    /// </summary>
    public class BaseOpusCasinoSettingController : BaseController
    {
        /// <summary>
        /// Execute before each request, if Downtime then go to Downtime page.
        /// Or not Authenticate will go to Login Page.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            //AppSession.AccId = 8;
            ResourceHelper.SetCulture();
            var rootUrl = Url.Content("~") + filterContext.RouteData.DataTokens["area"].ToString() + "/" + ModuleRegistrator.Module + "/";
            ViewBag.RootUrl = rootUrl;
            base.OnActionExecuting(filterContext);
        }
    }
}
