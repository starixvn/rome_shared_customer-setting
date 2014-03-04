// --------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.App_Start
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
            bundles.Add(new ScriptBundle("~/js/keno/customersetting").Include(
                "~/Areas/CustomerSetting/Views/Keno/Scripts/modules/module.js",
                "~/Areas/CustomerSetting/Views/Keno/Scripts/repositories/repository.js",
                "~/Areas/CustomerSetting/Views/Keno/Scripts/directives/directive.js",
                "~/Areas/CustomerSetting/Views/Keno/Scripts/services/common.service.js",
                "~/Areas/CustomerSetting/Views/Keno/Scripts/services/position-taking.service.js",
                "~/Areas/CustomerSetting/Views/Keno/Scripts/services/bet-setting.service.js",
                "~/Areas/CustomerSetting/Views/Keno/Scripts/services/commission-setting.service.js",
                "~/Areas/CustomerSetting/Views/Keno/Scripts/controllers/main.controller.js",
                "~/Areas/CustomerSetting/Views/Keno/Scripts/controllers/customer-information.controller.js",
                "~/Areas/CustomerSetting/Views/Keno/Scripts/controllers/bet-setting.controller.js",
                "~/Areas/CustomerSetting/Views/Keno/Scripts/controllers/position-taking.controller.js"));

            bundles.Add(new StyleBundle("~/css/keno/customersetting").Include(
                 "~/Areas/CustomerSetting/Views/Keno/Styles/keno-setting.less",
                "~/Areas/CustomerSetting/Views/Keno/Styles/theme-color.less"));
        }
    }
}