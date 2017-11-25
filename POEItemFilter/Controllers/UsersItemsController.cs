using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using POEItemFilter.Library;
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

        public ActionResult NewItemSession()
        {
            ItemUser viewModel = new ItemUser();
            return View(viewModel);
        }

        public ActionResult NewItemDb(int id)
        {
            ItemUser viewModel = new ItemUser()
            {
                FilterId = id
            };

            return View(viewModel);
        }

        /// <summary>
        /// The method is filtering database depending on received data.
        /// </summary>
        /// <param name="id">Represents data entered by the user. Format: baseType|type|attribute1|attribute2. If user don't select one of the parameters, then it's null.</param>
        /// <returns>Refresh view with new data.</returns>
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

            // This part of code applies only to items with attributes (armour and it's types)
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

            // If item doesn't have attribute.
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
                Items = items,
            };

            return PartialView("Refresh", viewModel);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeleteItemSession(int id)
        {
            List<ItemUser> viewModel = Session["ItemsList"] as List<ItemUser>;
            var item = viewModel.SingleOrDefault(i => i.Id == id);
            if (item == null)
            {
                return View("NewFilter", "Filters");
            }
            viewModel.Remove(item);
            Session["ItemsList"] = viewModel;

            return RedirectToAction("NewFilter", "Filters");
        }

        public ActionResult DeleteItemDb(int id)
        {
            var itemInDb = _context.UsersItems.SingleOrDefault(i => i.Id == id);
            int filterId = itemInDb.FilterId;
            if (itemInDb != null)
            {
                _context.UsersItems.Remove(itemInDb);
                _context.SaveChanges();
            }

            return RedirectToAction("EditFilter", "Filters", new { id = filterId });
        }

        public ActionResult ItemSession(int id)
        {
            List<ItemUser> viewModel = Session["ItemsList"] as List<ItemUser>;
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            var item = viewModel.SingleOrDefault(i => i.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View("EditItemSession", item);
        }

        public ActionResult ItemDb(int id)
        {
            var itemInDb = _context.UsersItems.SingleOrDefault(i => i.Id == id);
            if (itemInDb == null)
            {
                return HttpNotFound();
            }

            return View("EditItemDb", itemInDb);
        }

        [HttpPost]
        public ActionResult AddItemSession(ItemUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("NewItemSession", "UsersItems");
            }

            ItemUser item = ItemUserModelMap.ViewModelToItemUser(model);

            List<ItemUser> sessionModel = Session["ItemsList"] as List<ItemUser>;
            if (sessionModel == null)
            {
                item.Id = 1;
                Session["ItemsList"] = new List<ItemUser>();
                Session.Timeout = 30;
            }
            else if (item.Id != 0)
            {
                sessionModel[item.Id - 1] = item;
                return View("NewFilter", "Filters", sessionModel);
            }
            else
            {
                item.Id = sessionModel.Count + 1;
            }
            List<ItemUser> viewModel = Session["ItemsList"] as List<ItemUser>;
            viewModel.Add(item);

            return RedirectToAction("NewFilter", "Filters");
        }

        [HttpPost]
        public ActionResult AddItemDb(ItemUserViewModel model, int filterId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("MyFilters", "UsersItems");
            }

            ItemUser item = ItemUserModelMap.ViewModelToItemUser(model);

            _context.UsersItems.Add(item);
            _context.SaveChanges();

            return RedirectToAction("EditFilter", "Filters", new { id = item.FilterId });
        }

        [HttpPost]
        public ActionResult EditItemSession(ItemUserViewModel model, int itemId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("NewItemSession", "UsersItems");
            }

            ItemUser item = ItemUserModelMap.ViewModelToItemUser(model);

            List<ItemUser> sessionModel = Session["ItemsList"] as List<ItemUser>;
            if (sessionModel == null)
            {
                item.Id = 1;
                Session["ItemsList"] = new List<ItemUser>();
                Session.Timeout = 30;
            }
            else if (item.Id != 0)
            {
                int id = sessionModel.FindIndex(i => i.Id == item.Id);
                sessionModel[id] = item;
                return RedirectToAction("NewFilter", "Filters");
            }
            else
            {
                item.Id = sessionModel.Count + 1;
            }
            List<ItemUser> viewModel = Session["ItemsList"] as List<ItemUser>;
            viewModel.Add(item);

            return RedirectToAction("NewFilter", "Filters");
        }

        [HttpPost]
        public ActionResult EditItemDb(ItemUserViewModel model, int filterId, int itemId)
        {
            ItemUser item = ItemUserModelMap.ViewModelToItemUser(model);

            if (!ModelState.IsValid)
            {
                return RedirectToAction("MyFilters", "UsersItems");
            }

            ItemUser itemInDb = new ItemUser();
            itemInDb = _context.UsersItems.SingleOrDefault(i => i.Id == item.Id);
            itemInDb.BaseType = item.BaseType;
            itemInDb.Attribute1 = item.Attribute1;
            itemInDb.Attribute2 = item.Attribute2;
            itemInDb.Class = item.Class;
            itemInDb.Corrupted = item.Corrupted;
            itemInDb.DropLevel = item.DropLevel;
            itemInDb.Height = item.Height;
            itemInDb.Identified = item.Identified;
            itemInDb.ItemLevel = item.ItemLevel;
            itemInDb.LinkedSockets = item.LinkedSockets;
            itemInDb.MainCategory = item.MainCategory;
            itemInDb.PlayAlertSound = item.PlayAlertSound;
            itemInDb.Quality = item.Quality;
            itemInDb.Rarity = item.Rarity;
            itemInDb.SetBackgroundColor = item.SetBackgroundColor;
            itemInDb.SetBorderColor = item.SetBorderColor;
            itemInDb.SetFontSize = item.SetFontSize;
            itemInDb.SetTextColor = item.SetTextColor;
            itemInDb.Show = item.Show;
            itemInDb.Sockets = item.Sockets;
            itemInDb.SocketsGroup = item.SocketsGroup;
            itemInDb.Width = item.Width;

            _context.SaveChanges();

            return RedirectToAction("EditFilter", "Filters", new { id = item.FilterId });
        }
    }
}