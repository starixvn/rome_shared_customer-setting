// --------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.AppStart
{
    using System.Reflection;
    using System.Web.Mvc;

    /// <summary>
    /// Route Config.
    /// </summary>
    public class RouteConfig
    {
        public static void RegisterRoutes(string areaName, AreaRegistrationContext context)
        {
            var controllerName = Assembly.GetExecutingAssembly().GetName().Name + ".Controllers";
            context.MapRoute(
                areaName + "_Default",
                areaName + "/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { controllerName });
        }
    }
}
