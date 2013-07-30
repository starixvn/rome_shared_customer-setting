using System.Web.Mvc;
using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;
using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Implement;
using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Interface;
using Fanex.CustomerSetting.Module.Core.Web.Controllers;
using Fanex.CustomerSetting.Module.Core.Web;

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Controllers
{
	public class CasinoController : BaseController
	{
		//
		// GET: /Home/
		private IProgressiveService _progressiveService;

		public CasinoController()
		{
			_progressiveService = new ProgressiveService();
		}

		//[HandleActionError]
		//[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
		[HttpGet]
		public ActionResult Index()
		{
			return View("~/Areas/CustomerSetting/Views/ProgressiveBonus/Casino/Index.cshtml");
		}

		/*[HttpPost]
		public JsonResult GetProducts()
		{
			var products = _progressiveService.GetProducts();
			return this.Json(products);
		}*/

		[HttpPost]
		public JsonResult GetProducts(string username, int roleid)
		{
			AppSession.RequestUsername = username;
			AppSession.RequestRoleId = roleid;

			var products = _progressiveService.GetProducts();
			return this.Json(products);
		}

		[HttpPost]
		public JsonResult UpdateProgressive(Progressive progressive)
		{
			var results = _progressiveService.UpdateProgressive(progressive);
			return this.Json(results);
		}
	}
}