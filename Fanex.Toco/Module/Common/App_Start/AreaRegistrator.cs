namespace Fanex.CustomerSetting.Module.Common.App_Start
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;

    public class AreaRegistrator : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Common";
            }
        }

        public static void Register(HttpConfiguration config)
        {
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(this.AreaName, context);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Register(System.Web.Http.GlobalConfiguration.Configuration);
        }
    }
}