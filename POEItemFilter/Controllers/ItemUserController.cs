using System.Linq;
using System.Web.Mvc;
using POEItemFilter.Models;

namespace POEItemFilter.Controllers
{
    public class ItemUserController : Controller
    {
        ApplicationDbContext _context;

        public ItemUserController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: ItemUser
        public ActionResult Index()
        {
            var items = _context.ItemsDB.ToList();
            return View(items);
        }
    }
}