using System.Web.Mvc;

namespace POEItemFilter.Controllers
{
    public class FiltersController : Controller
    {
        // GET: Filters
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewFilter()
        {
            return View();
        }
    }
}