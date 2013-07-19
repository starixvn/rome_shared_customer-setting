using System.Web.Optimization;

namespace Fanex.CustomerSetting.Module.ProgressiveModule.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/areas/CustomerSetting/ProgressiveBonus").Include(
                "~/Areas/CustomerSetting/Views/ProgressiveBonus/Scripts/Progressive.js"
            ));
          
        }
    }
}