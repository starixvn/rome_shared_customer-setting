using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Core.Exceptions;
using Fanex.CustomerSetting.Module.Core.Security;
using Fanex.CustomerSetting.Module.ProgressiveModule.Configurations;
using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Interface;
using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Implement;
using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;
using Fanex.CustomerSetting.Module.Core.Web.Controllers;
using Fanex.CustomerSetting.Module.Core.Web;
using Fanex.CustomerSetting.Module.Core;

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Controllers
{
    public class CasinoController : BaseController
    {
        //
        // GET: /Home/

        IProgressiveService _progressiveService;
        public CasinoController()
        {
            _progressiveService = new ProgressiveService();
        }

        //[HandleActionError]
        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult Index()
        {
           
            return View("~/Areas/CustomerSetting/Views/ProgressiveBonus/Casino/Index.cshtml");
        }


        [HttpPost]
        public JsonResult GetProducts()
        {
            var products = _progressiveService.GetProducts();
            return this.Json(products);
        }

        [HttpPost]
        public JsonResult UpdateProgressive(Progressive progressive)
        {
            string ip = Utils.GetClientIP();
            var results = _progressiveService.UpdateProgressive(progressive, ip, progressive.AdminId, progressive.AdminUserName, 0);
            return this.Json(results);
        }
    }
}
