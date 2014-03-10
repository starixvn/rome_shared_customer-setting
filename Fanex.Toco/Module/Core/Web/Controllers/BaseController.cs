// --------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Anders.Le" date="2012.10.12">Created</change>
//   <change who="Ricky.Nguyen" date="2012.11.14">Update Maintenance Time</change>
//   <change who="Andy.Nguyen" date="2013.06.16">Define constants and JsonBase method</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Controllers.Base;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// The base controller.
    /// </summary>
    public class BaseController : Controller
    {
        public const int Super = 4;
        public const int Master = 3;
        public const int Agent = 2;
        public const int Member = 1;
        public const int FromSystem = 4;

        public BaseController()
        {
        }

        /// <summary>
        /// Gets the value that indicates whether the current site allows users login directly.
        /// If "0", users must login through NAP Hub.
        /// </summary>
        /// <value>
        /// The is site login.
        /// </value>
        public int IsSiteLogin
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Ensures that ExecuteCore() is called.
        /// </summary>
        /// <value>
        /// Asynchronous is supported or not.
        /// </value>
        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }

        public virtual ActionResult JsonBase(object data, JsonSerializerSettings settings = null)
        {
            if (settings == null)
            {
                settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            return new JsonBase(data, settings);
        }

        /// <summary>
        /// Get list actions of Controller.
        /// </summary>
        /// <typeparam name="T">The Controller.</typeparam>
        /// <returns>List of actions.</returns>
        public List<string> GetActionListFromController<T>()
        {
            var listAction = new List<string>();

            Type type = typeof(T);
            MethodInfo[] methods = type.GetMethods();

            foreach (MethodInfo methodInfo in methods)
            {
                if (methodInfo.IsPublic && methodInfo.ReturnParameter != null
                    && typeof(ActionResult).IsAssignableFrom(methodInfo.ReturnParameter.ParameterType))
                {
                    listAction.Add(methodInfo.Name);
                }
            }

            return listAction;
        }

        /// <summary>
        /// Execute before each request, if Downtime then go to Downtime page
        /// Or not Authenticate will go to Login Page.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AppSession.CurrentActionName = filterContext.ActionDescriptor.ActionName;
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Called before the action result that is returned by an action method is executed.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action result.</param>
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // Get current working on menu for getting display name,... on webpage
            if (filterContext.Result is ViewResult && string.Equals(AppSession.CurrentActionName, "Index", StringComparison.OrdinalIgnoreCase))
            {
                /* var menuPath = MenuService.GetMenuPathFromRequestFilePath(filterContext.HttpContext.Request.FilePath, Request.ApplicationPath);
                 string controllerName = (string)filterContext.RouteData.Values["controller"];
                 AppSession.CurrentWorkingOnMenu = this._menuService.GetMenuByURL(menuPath);
                 *
                 */
            }
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = int.MaxValue
            };
        }

        /// <summary>
        /// Invokes the action in the current controller context.
        /// </summary>
        protected override void ExecuteCore()
        {
            string cultureName = AppSession.Language;
            if (string.IsNullOrEmpty(cultureName))
            {
                cultureName = "en-US";
                AppSession.Language = cultureName;
            }

            // Modify current thread's cultures
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            base.ExecuteCore();
        }

        /// <summary>
        /// Actions the denied.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The object of ActionResult.</returns>
        protected ActionResult ActionDenied(HttpRequestBase request)
        {
            if (request.IsAjaxRequest())
            {
                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        errorCode = 1,
                        errorMessage = "You are not authorized on this function",
                        isSuccess = false
                    }
                };
            }
            else
            {
                return new ViewResult
                {
                    ViewName = "~/Areas/CustomerSetting/Views/Security/AccessDenied.cshtml"
                };
            }
        }
    }
}