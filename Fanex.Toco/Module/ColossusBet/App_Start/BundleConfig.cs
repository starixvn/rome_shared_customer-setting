namespace Fanex.CustomerSetting.Module.ColossusBetSetting.App_Start
{
    using System.Web.Optimization;

    /// <summary>
    /// Register bundles.
    /// </summary>
    public static class BundleConfig
    {
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterBundleCore(bundles);
            RegisterCustomerSetting(bundles);
        }

        internal static void RegisterBundleCore(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/core").Include(
                "~/Areas/CustomerSetting/Scripts/jquery.js",
                "~/Areas/CustomerSetting/Scripts/jquery.blockUI.1.33.js",
                "~/Areas/CustomerSetting/Scripts/jquery-ui-1.9.1.min.js",
                "~/Areas/CustomerSetting/Scripts/angular.min.js"));

            bundles.Add(new StyleBundle("~/css/core").Include(
                "~/Areas/CustomerSetting/Styles/jquery-ui.css"));
        }

        /// <summary>
        /// Registers the customer setting.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        internal static void RegisterCustomerSetting(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/ColossusBetSetting").Include());

            bundles.Add(new StyleBundle("~/css/ColossusBetSetting").Include());
        }
    }
}