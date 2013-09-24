// ************************************************************************************
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) BundleConfig.cs. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.Racebook.App_Start
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
            bundles.Add(new ScriptBundle("~/bundles/racingsetting").Include(
                "~/Areas/CustomerSetting/Views/Racebook/Scripts/*.js",
                "~/Areas/CustomerSetting/Views/Racebook/Scripts/libs/*.js"));

            bundles.Add(new StyleBundle("~/bundles/racingstyle").Include(
                "~/Areas/CustomerSetting/Views/Racebook/Styles/*.css"));

            bundles.Add(
                new ScriptBundle("~/bundles/racingsetting/super").Include(
                    "~/Areas/CustomerSetting/Views/Racebook/Super/Scripts/*.js"));
        }
    }
}