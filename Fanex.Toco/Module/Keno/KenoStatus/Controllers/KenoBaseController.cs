// -----------------------------------------------------------------------
// <copyright file="KenoBaseController.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace Fanex.CustomerSetting.Module.KenoStatus.Controllers
{
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;

    /// <summary>
    /// Keno Base Controller.
    /// </summary>
    public class KenoBaseController : BaseController
    {
        /// <summary>
        /// Convert language key.
        /// </summary>
        /// <param name="lang">The language.</param>
        /// <returns>Short English key.</returns>
        public static string TWLang(string lang)
        {
            switch (lang)
            {
                case "en-US": return "en";
                case "zh-TW": return "ch"; // Traditional.
                case "zh-CN": return "cs"; // Simplified.
                case "ja-JP": return "jp";
                case "th-TH": return "th"; // Thai.
                default: return "en";
            }
        }

        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}