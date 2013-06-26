using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.CustomerSetting.Module.Core.Web.App_Start
{
    public class AppRouteConfig
    {
        public static void RegisterRoutes(string areaName, AreaRegistrationContext context, List<string> controllers)
        {
            
            /*context.MapRoute(
               areaName  "_Default",
                "/{action}/{id}",
                new { controller = "Home", action = "Index", apiId = UrlParameter.Optional }, new[] { controllerName });
             * 
             * */

            /*
            var controllerName = Assembly.GetExecutingAssembly().GetName().Name + ".Controllers";
            
            context.MapRoute(
                areaName + "_Heaven",
                areaName + "/Heaven/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Fanex.Toco.Module.SampleModule.Controllers" });
            */
        }

        
    }
}