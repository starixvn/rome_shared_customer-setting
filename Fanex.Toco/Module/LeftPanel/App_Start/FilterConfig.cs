using System.Web.Mvc;

namespace Fanex.CustomerSetting.Module.LeftPanel.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}