using System.Web;
using System.Web.Mvc;

namespace ABMCloud
{
    public class HomeController : Controller
    {

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View();
        }
    }
}