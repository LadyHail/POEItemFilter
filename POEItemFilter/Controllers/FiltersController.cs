using System.Web.Mvc;
using POEItemFilter.Models;

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

        public ActionResult AddItem(ItemUser item)
        {
            return View("NewFilter");
        }
    }
}