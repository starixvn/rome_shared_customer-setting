// ************************************************************************************
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) BundleConfig.cs. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="kan.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

namespace Fanex.CustomerSetting.Module.Bingo.App_Start
{
    using System.Web.Optimization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bingosetting").Include(
                "~/Areas/CustomerSetting/Views/Bingo/Scripts/Common.js",
                "~/Areas/CustomerSetting/Views/Bingo/Scripts/CommonPT.js",
                "~/Areas/CustomerSetting/Views/Bingo/Scripts/Setting/BingoHelper.js",
                "~/Areas/CustomerSetting/Views/Bingo/Scripts/Setting/BingoSetting.js"));

            bundles.Add(new StyleBundle("~/bundles/bingostyle").Include(
                "~/Areas/CustomerSetting/Views/Bingo/Styles/*.css"));
        }
    }
}
