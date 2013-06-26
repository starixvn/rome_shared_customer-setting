using System.Web.Mvc;

namespace Fanex.CustomerSetting.Module.SampleModule.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}