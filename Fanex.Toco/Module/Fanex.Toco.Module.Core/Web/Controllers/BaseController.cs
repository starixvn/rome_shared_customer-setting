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
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Web.Mvc;
    using Fanex.ExternalAccount.Controllers.Base;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
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
        /// Execute before each request, if Downtime then go to Downtime page
        /// Or not Authenticate will go to Login Page
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AppSession.CurrentActionName = filterContext.ActionDescriptor.ActionName;
            base.OnActionExecuting(filterContext);
        }

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

        /// <summary>
        /// Indicates whether the current site allows users login directly.
        /// If "0", users must login through NAP Hub
        /// </summary>
        public int IsSiteLogin
        {
            get
            {
                return 0;
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
                MaxJsonLength = Int32.MaxValue
            };
        }

        /// <summary>
        /// Get list actions of Controller
        /// </summary>
        /// <typeparam name="T">Controller</typeparam>
        /// <returns></returns>
        protected static List<string> GetActionListByController<T>()
        {
            var listAction = new List<string>();

            Type t = typeof(T);
            MethodInfo[] mi = t.GetMethods();
            foreach (MethodInfo m in mi)
            {
                if (m.IsPublic)
                    if (m.ReturnParameter != null && typeof(ActionResult).IsAssignableFrom(m.ReturnParameter.ParameterType))
                    {
                        listAction.Add(m.Name);
                    }
            }


            return listAction;

        }

        public virtual ActionResult JsonBase(object data, JsonSerializerSettings settings = null)
        {
            if (settings == null)
            {
                settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                //settings.Formatting = Formatting.Indented;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            return new JsonBase(data, settings);
        }
    }
}
