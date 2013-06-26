using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Core.Security;

namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    public class CasinoController : Controller
    {
        //
        // GET: /Home/

       
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult Index()
        {
            return View();
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
