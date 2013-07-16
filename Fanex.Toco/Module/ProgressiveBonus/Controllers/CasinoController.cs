using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Core.Exceptions;
using Fanex.CustomerSetting.Module.Core.Security;

namespace Fanex.CustomerSetting.Module.SampleModule.Controllers
{
    public class CasinoController : Controller
    {
        //
        // GET: /Home/

        [HandleActionError]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult Index()
        {
            return View("~/Areas/CustomerSetting/Views/ProgressiveBonus/Casino/Index.cshtml");
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

       
    }
}
