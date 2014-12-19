// --------------------------------------------------------------------------
// <copyright file="AreaRegistrator.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.AppStart
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;

    /// <summary>
    /// AREA REGISTRATOR.
    /// </summary>
    public class AreaRegistrator : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Statement";
            }
        }
        
        public static void Register(HttpConfiguration config)
        {
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(AreaName, context);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Register(System.Web.Http.GlobalConfiguration.Configuration);
        }
    }
}
