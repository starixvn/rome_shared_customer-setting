// ************************************************************************************
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) BundleConfig.cs. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

using System.Web.Optimization;

namespace Fanex.CustomerSetting.Module.SearchCopy.App_Start
{
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
            bundles.Add(new ScriptBundle("~/bundles/racingsetting").Include(
                "~/Areas/CustomerSetting/Views/Racebook/Scripts/libs/*.js",
                "~/Areas/CustomerSetting/Views/Racebook/Scripts/libs/ToolTip/wz_tooltip/wz_tooltip.js",
                "~/Areas/CustomerSetting/Views/Racebook/Scripts/common.js",
                "~/Areas/CustomerSetting/Views/Racebook/Scripts/CommonPT.js",
                "~/Areas/CustomerSetting/Views/Racebook/Scripts/Setting/RacingHelper.js",
                "~/Areas/CustomerSetting/Views/Racebook/Scripts/Setting/RacingSetting.js"));

            bundles.Add(new StyleBundle("~/bundles/racingstyle").Include(
                "~/Areas/CustomerSetting/Views/Racebook/Styles/*.css"));

           
        }
    }
}