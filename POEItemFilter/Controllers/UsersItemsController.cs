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
            var baseTypes = _context.BaseTypes.ToList();
            var items = baseTypes.SelectMany(i => i.Items).ToList();
            var types = baseTypes.SelectMany(i => i.Types).ToList();
            var attributes = baseTypes.SelectMany(i => i.Attributes).ToList();

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

            var itemsList = baseTypes
                .SelectMany(i => i.Items)
                .ToList();

            bool isBaseTypeInDb = baseTypes.Any(i => i.Id == baseTypeId);

            bool isTypeIdInDb = baseTypes
                .SelectMany(i => i.Types)
                .Any(i => i.Id == typeId);

            var types = (List<ItemType>)null;

            if (isTypeIdInDb)
            {
                if (isBaseTypeInDb)
                {
                    types = baseTypes
                        .Where(i => i.Id == baseTypeId)
                        .SelectMany(i => i.Types)
                        .ToList();
                }
                else
                {
                    types = baseTypes
                        .SelectMany(i => i.Types)
                        .ToList();
                }
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

            bool isAttr1InDb = baseTypes
                .SelectMany(i => i.Attributes)
                .Any(i => i.Id == attribute1Id);

            bool isAttr2InDb = baseTypes
                .SelectMany(i => i.Attributes)
                .Any(i => i.Id == attribute2Id);

            var attributes = baseTypes
                .SelectMany(i => i.Attributes)
                .ToList();

            var items = (List<ItemDB>)null;

            if (isAttr1InDb && !isAttr2InDb)
            {
                items = baseTypes
                    .SelectMany(i => i.Attributes)
                    .Where(i => i.Id == attribute1Id)
                    .SelectMany(i => i.Items)
                    .ToList();
            }

            else if (!isAttr1InDb && isAttr2InDb)
            {
                items = baseTypes
                    .SelectMany(i => i.Attributes)
                    .Where(i => i.Id == attribute2Id)
                    .SelectMany(i => i.Items)
                    .ToList();
            }

            else if (isAttr1InDb && isAttr2InDb)
            {
                var firstFilter = attributes
                    .Where(i => i.Id == attribute1Id)
                    .SelectMany(i => i.Items)
                    .ToList();

                var secondFilter = attributes
                    .Where(i => i.Id == attribute2Id)
                    .SelectMany(i => i.Items)
                    .ToList();

                items = firstFilter
                    .Join(secondFilter,
                     a => a.Id,
                     b => b.Id,
                    (a, b) => new ItemDB()
                    {
                        Attributes = b.Attributes,
                        BaseType = b.BaseType,
                        BaseTypeId = b.BaseTypeId,
                        Id = b.Id,
                        Level = b.Level,
                        Name = b.Name,
                        Type = b.Type,
                        TypeId = b.TypeId
                    })
                    .Select(i => i)
                    .ToList();

                // Alternative
                //items = (from a in firstFilter
                //         join b in secondFilter
                //         on a.Id
                //         equals b.Id
                //         select new ItemDB()
                //         {
                //             Attributes = b.Attributes,
                //             BaseType = b.BaseType,
                //             BaseTypeId = b.BaseTypeId,
                //             Id = b.Id,
                //             Level = b.Level,
                //             Name = b.Name,
                //             Type = b.Type,
                //             TypeId = b.TypeId
                //         }).ToList();
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