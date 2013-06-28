using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Fanex.CustomerSetting.Website.App_Start;

namespace Fanex.CustomerSetting.Website
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "CustomerSetting_Master",
                url: "CustomerSetting/Casino/{controller}/{action}/{id}/{uplineId}/{roleId}/{uplineRoleId}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, uplineId = UrlParameter.Optional, roleId = UrlParameter.Optional, uplineRoleId = UrlParameter.Optional });

            routes.MapRoute(
                name: "CustomerSetting_Customer",
                url: "CustomerSetting/Casino/{controller}/{action}/{id}/{uplineId}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, uplineId = UrlParameter.Optional });
        }
    }
}