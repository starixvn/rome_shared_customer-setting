namespace Fanex.CustomerSetting.Module.ColossusBetSetting.App_Start
{
    using System.Web.Mvc;

    /// <summary>
    /// The FilterConfig class.
    /// </summary>
    public sealed class FilterConfig
    {
        private FilterConfig()
        { 
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}