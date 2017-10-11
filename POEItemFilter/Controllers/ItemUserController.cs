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
        public ActionResult SelectCategory()
        {
            var items = _context.ItemsDB.ToList();
            return View(items);
        }

        //public ActionResult SelectClass(string type)
        //{
        //    var items = _context.ItemsDB.Select(i => i.Type).ToList();
        //    return ("SelectCategory", items);
        //}
    }
}