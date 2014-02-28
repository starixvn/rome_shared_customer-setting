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
    public class BundleConfig
    {
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            ////bundles.Add(new ScriptBundle("~/bundles/KenoSetting").Include(
            ////    "~/Areas/CustomerSetting/Views/Keno/Scripts/libs/*.js",
            ////    //"~/Areas/CustomerSetting/Views/Keno/Scripts/libs/ToolTip/wz_tooltip/wz_tooltip.js",
            ////    "~/Areas/CustomerSetting/Views/Keno/Scripts/common.js",
            ////    "~/Areas/CustomerSetting/Views/Keno/Scripts/CommonPT.js",
            ////    "~/Areas/CustomerSetting/Views/Keno/Scripts/Setting/RacingHelper.js",
            ////    "~/Areas/CustomerSetting/Views/Keno/Scripts/Setting/RacingSetting.js"));

            ////bundles.Add(new StyleBundle("~/bundles/KenoStyle").Include(
            ////    "~/Areas/CustomerSetting/Views/Keno/Styles/*.css"));
        }
    }
}