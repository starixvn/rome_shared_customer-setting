using System.Web.Optimization;

namespace Fanex.CustomerSetting.Module.Casino.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/common/js").Include(
                "~/Areas/CustomerSetting/Scripts/jquery.js",
                "~/Areas/CustomerSetting/Scripts/angular.min.js",
                "~/Areas/CustomerSetting/Scripts/Validators.js",
                "~/Areas/CustomerSetting/Scripts/jquery.blockUI.1.33.js",
                "~/Areas/CustomerSetting/Scripts/Fanex.CustomerSetting.Core.js",
                "~/Areas/CustomerSetting/Views/Casino/Scripts/Fanex.CustomerSetting.Casino.js"
            ));

            bundles.Add(new ScriptBundle("~/master/js").Include(
                "~/Areas/CustomerSetting/Views/Casino/Scripts/Fanex.CustomerSetting.Casino.EditMaster.js"
                ));

            bundles.Add(new ScriptBundle("~/agent/js").Include(
                "~/Areas/CustomerSetting/Views/Casino/Scripts/Fanex.CustomerSetting.Casino.EditAgent.js"
                ));

            bundles.Add(new ScriptBundle("~/member/js").Include(
                "~/Areas/CustomerSetting/Views/Casino/Scripts/Fanex.CustomerSetting.Casino.EditMember.js"
                ));

            bundles.Add(new ScriptBundle("~/member/super/js").Include(
                "~/Areas/CustomerSetting/Views/Casino/Scripts/Fanex.CustomerSetting.Casino.EditMemberBySuper.js"
                ));

            bundles.Add(new ScriptBundle("~/member/master/js").Include(
                "~/Areas/CustomerSetting/Views/Casino/Scripts/Fanex.CustomerSetting.Casino.EditMemberByMaster.js"
                ));

            bundles.Add(new StyleBundle("~/common/css").Include(
                "~/Areas/CustomerSetting/Styles/Common.css",
                "~/Areas/CustomerSetting/Views/Casino/Styles/Casino.css"
                ));
        }
    }
}