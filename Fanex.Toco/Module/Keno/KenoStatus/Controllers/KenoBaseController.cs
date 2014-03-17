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
    }
}
