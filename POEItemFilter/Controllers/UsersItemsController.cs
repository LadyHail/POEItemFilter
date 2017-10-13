using System.Linq;
using System.Web.Mvc;
using POEItemFilter.Models;
using POEItemFilter.ViewModels;

namespace POEItemFilter.Controllers
{
    public class UsersItemsController : Controller
    {
        ApplicationDbContext _context;

        public UsersItemsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: ItemUser
        public ActionResult NewItem()
        {
            var items = _context.ItemsDB.ToList();
            var baseTypes = _context.BaseTypes.ToList();
            var types = _context.Types.ToList();
            var attributes = _context.Attributes.ToList();

            var viewModel = new NewItemViewModel()
            {
                Items = items,
                BaseTypes = baseTypes,
                Types = types,
                Attributes = attributes
            };

            return View(viewModel);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}