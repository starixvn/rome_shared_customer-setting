// --------------------------------------------------------------------------
// <copyright file="KenoSettingBaseController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Controllers
{
    using System.Globalization;
    using System.Threading;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.App_Start;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Services;

    /// <summary>
    /// Class KenoSettingBaseController.
    /// </summary>
    public class KenoSettingBaseController : BaseController
    {
        /// <summary>
        /// Execute before each request, if Downtime then go to Downtime page.
        /// Or not Authenticate will go to Login Page.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            SetCurrentCulture();
            var rootUrl = Url.Content("~") + filterContext.RouteData.DataTokens["area"].ToString() + "/" + ModuleRegistrator.Module + "/";
            ViewBag.RootUrl = rootUrl;
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Sets the current culture.
        /// </summary>
        private static void SetCurrentCulture()
        {
            string cultureName = new KenoSession().Language;

            if (string.IsNullOrEmpty(cultureName))
            {
                cultureName = "zh-CN";
            }

            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }
    }
}
