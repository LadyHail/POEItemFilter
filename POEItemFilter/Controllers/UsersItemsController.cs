using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using POEItemFilter.Models;
using POEItemFilter.Models.ItemsDB;
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

        public ActionResult Refresh(string id)
        {
            string[] ids = id.Split('|');
            int baseTypeId = int.Parse(ids[0]);
            int typeId = int.Parse(ids[1]);
            int attribute1Id = int.Parse(ids[2]);
            int attribute2Id = int.Parse(ids[3]);

            var baseTypes = _context.BaseTypes.ToList();
            bool isBaseTypeInDb = baseTypes.Any(i => i.Id == baseTypeId);

            bool isTypeIdInDb = _context.Types.Any(i => i.Id == typeId);
            var types = (List<ItemType>)null;

            if (isTypeIdInDb)
            {
                types = baseTypes
                    .SelectMany(i => i.Types)
                    .ToList();
            }
            else
            {
                types = baseTypes
                    .SelectMany(i => i.Types)
                    .Where(i => i.BaseTypeId == baseTypeId)
                    .ToList();

                if (types.Count() == 0)
                {
                    types = baseTypes
                    .SelectMany(i => i.Types)
                    .ToList();
                }
            }

            bool isAttr1InDb = _context.Attributes.Any(i => i.Id == attribute1Id);
            bool isAttr2InDb = _context.Attributes.Any(i => i.Id == attribute2Id);
            var attributes = baseTypes
                .SelectMany(i => i.Attributes)
                .ToList();

            var itemsList = _context.ItemsDB.ToList();
            var items = (List<ItemDB>)null;
            var itemsSecondFilter = (List<ItemAttribute>)null;

            if (isAttr1InDb && !isAttr2InDb)
            {
                items = attributes
                    .Where(a => a.Id == attribute1Id)
                    .SelectMany(b => b.Items)
                    .ToList();
            }

            else if (!isAttr1InDb && isAttr2InDb)
            {
                items = attributes
                    .Where(a => a.Id == attribute2Id)
                    .SelectMany(b => b.Items)
                    .ToList();
            }

            else if (isAttr1InDb && isAttr2InDb)
            {
                items = attributes
                    .Where(a => a.Id == attribute1Id)
                    .SelectMany(b => b.Items)
                    .ToList();

                itemsSecondFilter = items
                    .SelectMany(i => i.Attributes)
                    .Where(i => i.Id == attribute2Id)
                    .ToList();
            }
            else
            {
                if (isTypeIdInDb)
                {
                    items = itemsList
                        .Where(i => i.TypeId == typeId)
                        .Select(i => i)
                        .ToList();
                }
                else if (isBaseTypeInDb)
                {
                    items = itemsList
                        .Where(i => i.BaseTypeId == baseTypeId)
                        .Select(i => i)
                        .ToList();
                }
                else
                {
                    items = itemsList;
                }
            }

            var viewModel = new NewItemViewModel()
            {
                BaseTypes = baseTypes,
                Types = types,
                Attributes = attributes,
                Items = items
            };

            return View("NewItem", viewModel);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}