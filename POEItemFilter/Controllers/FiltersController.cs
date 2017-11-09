using System.Web.Mvc;
using POEItemFilter.Models;
using POEItemFilter.ViewModels;

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
            if (Session["ItemsList"] == null)
            {
                Session["ItemsList"] = new ItemUserList();
                Session.Timeout = 30;
            }

            ItemUserList viewModel = Session["ItemsList"] as ItemUserList;
            viewModel.UsersItems.Add(item);

            return View("NewFilter", viewModel);
        }
    }
}