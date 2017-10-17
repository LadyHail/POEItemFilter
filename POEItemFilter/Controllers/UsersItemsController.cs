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
            var baseTypes = _context.BaseTypes.ToList();

            var itemsList = baseTypes
                .SelectMany(i => i.Items)
                .ToList();

            string[] ids = id.Split('|');
            int baseTypeId, typeId, attribute1Id, attribute2Id;

            if (!int.TryParse(ids[0], out baseTypeId))
            {
                baseTypeId = baseTypes.Count() + 1;
            }

            if (!int.TryParse(ids[1], out typeId))
            {
                typeId = baseTypes.SelectMany(i => i.Types).Count() + 1;
            }

            if (!int.TryParse(ids[2], out attribute1Id))
            {
                attribute1Id = baseTypes.SelectMany(i => i.Attributes).Count() + 1;
            }

            if (!int.TryParse(ids[3], out attribute2Id))
            {
                attribute2Id = baseTypes.SelectMany(i => i.Attributes).Count() + 1;
            }

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

            bool isBaseTypeArmour = baseTypes.SelectMany(i => i.Attributes).Any(i => i.BaseTypeId == baseTypeId);
            bool isTypeArmour = baseTypes.Where(i => i.Name == "Armour").SelectMany(i => i.Types).Any(i => i.Id == typeId);

            var attributes = (List<ItemAttribute>)null;
            var items = (List<ItemDB>)null;

            if (isBaseTypeArmour || isTypeArmour || !isBaseTypeInDb && !isTypeIdInDb)
            {
                bool isAttr1InDb = baseTypes
                    .SelectMany(i => i.Attributes)
                    .Any(i => i.Id == attribute1Id);

                bool isAttr2InDb = baseTypes
                    .SelectMany(i => i.Attributes)
                    .Any(i => i.Id == attribute2Id);

                attributes = baseTypes
                    .SelectMany(i => i.Attributes)
                    .ToList();

                if (isAttr1InDb && !isAttr2InDb)
                {
                    var firstFilter = attributes
                        .Where(i => i.Id == attribute1Id)
                        .SelectMany(i => i.Items)
                        .ToList();

                    var secondFilter = attributes
                        .Where(i => i.Id != attribute1Id)
                        .SelectMany(i => i.Items)
                        .ToList();

                    items = new List<ItemDB>();
                    foreach (var item in firstFilter)
                    {
                        bool match = secondFilter.Any(i => i.Id == item.Id);
                        if (!match)
                        {
                            items.Add(item);
                        }
                    }
                }

                else if (!isAttr1InDb && isAttr2InDb)
                {
                    var firstFilter = attributes
                        .Where(i => i.Id == attribute2Id)
                        .SelectMany(i => i.Items)
                        .ToList();

                    var secondFilter = attributes
                        .Where(i => i.Id != attribute2Id)
                        .SelectMany(i => i.Items)
                        .ToList();

                    items = new List<ItemDB>();
                    foreach (var item in firstFilter)
                    {
                        bool match = secondFilter.Any(i => i.Id == item.Id);
                        if (!match)
                        {
                            items.Add(item);
                        }
                    }
                }

                else if (isAttr1InDb && isAttr2InDb)
                {
                    if (attribute1Id != attribute2Id)
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
                        var firstFilter = attributes
                        .Where(i => i.Id == attribute1Id)
                        .SelectMany(i => i.Items)
                        .ToList();

                        var secondFilter = attributes
                            .Where(i => i.Id != attribute2Id)
                            .SelectMany(i => i.Items)
                            .ToList();

                        items = new List<ItemDB>();
                        foreach (var item in firstFilter)
                        {
                            bool match = secondFilter.Any(i => i.Id == item.Id);
                            if (!match)
                            {
                                items.Add(item);
                            }
                        }
                    }
                }

                if (isTypeIdInDb && items != null)
                {
                    items = items.Where(i => i.TypeId == typeId).ToList();
                }
                else if (isBaseTypeInDb && items != null)
                {
                    items = items.Where(i => i.BaseTypeId == baseTypeId).ToList();
                }
                else if (isTypeIdInDb)
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
                else if (attributes.Any(i => i.Id == attribute1Id) || attributes.Any(i => i.Id == attribute2Id))
                {

                }
                else
                {
                    items = itemsList;
                }
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

            return View("Refresh", viewModel);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}