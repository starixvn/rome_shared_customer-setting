using System.Web.Optimization;

namespace Fanex.CustomerSetting.Module.LeftPanel.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/common/css").Include(
                "~/Areas/CustomerSetting/Styles/Validators.css",
                "~/Areas/CustomerSetting/Styles/Common.css",
                "~/Areas/CustomerSetting/Views/Casino/Styles/Casino.css"
                ));
        }
    }
}