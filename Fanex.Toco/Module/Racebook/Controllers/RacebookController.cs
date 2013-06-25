using System.Web.Mvc;
using Fanex.Toco.Module.Core.Security;

namespace Fanex.Toco.Module.Racebook.Controllers
{
    public class RacebookController : Controller
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
