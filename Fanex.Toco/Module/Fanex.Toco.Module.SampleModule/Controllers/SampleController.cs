using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Core.Exceptions;
using Fanex.CustomerSetting.Module.Core.Security;

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Controllers
{
    public class SampleController : Controller
    {
        //
        // GET: /Home/

        [HandleActionError]
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
