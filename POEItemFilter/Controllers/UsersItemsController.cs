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

        // GET: AllItems
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

        //GET: /UsersItems/SelectBaseType/id
        public ActionResult SelectBaseType(int id)
        {
            var basetypes = _context.BaseTypes.ToList();

            var types = _context.Types
                .Where(m => m.BaseTypeId == id)
                .Select(m => m)
                .ToList();

            if (types.Count() == 0)
            {
                types = _context.Types.ToList();
            }

            var attributes = _context.Attributes
                .Where(m => m.BaseTypeId == id)
                .Select(m => m)
                .ToList();

            var items = _context.ItemsDB
                .Where(i => i.BaseTypeId == id)
                .Select(i => i)
                .ToList();

            var viewModel = new NewItemViewModel()
            {
                Items = items,
                BaseTypes = basetypes,
                Types = types,
                Attributes = attributes
            };

            return View("NewItem", viewModel);
        }

        public ActionResult SelectType(int id)
        {
            var baseTypes = _context.BaseTypes.ToList();

            var types = _context.Types.ToList();

            var attribute = _context.Types.SingleOrDefault(t => t.Id == id);
            var attributes = _context.Attributes
                .Where(i => i.Types == attribute)
                .Select(i => i)
                .ToList();

            var items = _context.ItemsDB
                .Where(i => i.TypeId == id)
                .Select(i => i)
                .ToList();

            var viewModel = new NewItemViewModel()
            {
                BaseTypes = baseTypes,
                Types = types,
                Attributes = attributes,
                Items = items
            };

            return View("NewItem", viewModel);
        }

        public ActionResult ItemsFilter(int id)
        {
            var items = _context.ItemsDB
                .Where(i => i.BaseTypeId == id)
                .Select(i => i)
                .ToList();

            return View("ItemPartialView", items);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}