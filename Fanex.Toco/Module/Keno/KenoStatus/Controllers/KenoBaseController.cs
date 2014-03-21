// -----------------------------------------------------------------------
// <copyright file="KenoBaseController.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;
using System.Threading;
using Fanex.CustomerSetting.Module.Core.Web;
using Fanex.CustomerSetting.Module.Core.Web.Controllers;

namespace Fanex.CustomerSetting.Module.KenoStatus.Controllers
{
    public class KenoBaseController:BaseController
    {
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
           base.OnActionExecuting(filterContext);
        }

        public static string TWLang(string lang)
        {
            switch (lang)
            {
                case "en-US": return "en";
                case "zh-TW": return "ch"; // Traditional
                case "zh-CN": return "cs"; // Simplified
                case "ja-JP": return "jp";
                case "th-TH": return "th"; // Thai
                default: return "en";
            }
        }
    }
}
