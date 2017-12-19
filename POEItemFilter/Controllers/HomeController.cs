using System.Web.Mvc;

namespace POEItemFilter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult HowTo()
        {
            return View();
        }
    }
}