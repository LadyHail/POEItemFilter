using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using POEItemFilter.Library;
using POEItemFilter.Library.CustomFilters;
using POEItemFilter.Models;
using POEItemFilter.ViewModels;

namespace POEItemFilter.Controllers
{
    public class FiltersController : Controller
    {
        ApplicationDbContext _context;

        public FiltersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize]
        [HttpGet]
        public ActionResult MyFilters()
        {
            var currentUser = User.Identity.GetUserId();
            var viewModel = _context.Filters.Where(f => f.UserId == currentUser).ToList();
            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult AllFilters()
        {
            List<Models.Filters.Filter> viewModel = _context.Filters.ToList();
            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        [DeleteFile]
        public FileResult Download(string file)
        {
            file += ".filter";
            string fullPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Filters\" + file;
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = file,
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(fullPath, System.Net.Mime.MediaTypeNames.Application.Octet, file);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GenerateText(int id)
        {
            var filterInDb = _context.Filters.SingleOrDefault(f => f.Id == id);
            StreamWriter filterText = GenerateFilter.CreateTempFile(filterInDb.Name);
            filterText.WriteLine("#Description: " + filterInDb.Description);

            var itemsInDb = _context.UsersItems.Where(i => i.FilterId == id).Select(i => i).OrderBy(i => i.RowId);
            List<ItemUser> itemsList = new List<ItemUser>();
            itemsList.AddRange(itemsInDb);

            GenerateFilter.SaveItems(filterText, itemsList, filterInDb);

            return Json(new { fileName = filterInDb.Name }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var filterInDb = _context.Filters.SingleOrDefault(f => f.Id == id);
            var itemsInDb = _context.UsersItems.Where(i => i.FilterId == id).Select(i => i).ToList();

            if (filterInDb == null)
            {
                return HttpNotFound();
            }
            if (itemsInDb == null)
            {
                return HttpNotFound();
            }

            bool isAuthorized = filterInDb.UserId == User.Identity.GetUserId();
            if (!isAuthorized)
            {
                return HttpNotFound();
            }

            FilterViewModel viewModel = new FilterViewModel()
            {
                Dedicated = filterInDb.Dedicated,
                Description = filterInDb.Description,
                Id = filterInDb.Id,
                Name = filterInDb.Name,
                UserId = filterInDb.UserId,
            };
            viewModel.Items = itemsInDb.OrderBy(i => i.RowId).ToList();

            return View("Filter", viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var itemsInDb = _context.UsersItems.Where(i => i.FilterId == id).Select(i => i).OrderBy(i => i.RowId).ToList();
            if (itemsInDb == null)
            {
                return HttpNotFound();
            }
            return View(itemsInDb);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteFilter(int id)
        {
            var filterInDb = _context.Filters.SingleOrDefault(f => f.Id == id);
            if (filterInDb == null)
            {
                return HttpNotFound();
            }

            if (User.Identity.GetUserId() != filterInDb.UserId)
            {
                return HttpNotFound();
            }

            _context.Filters.Remove(filterInDb);
            _context.SaveChanges();

            return null;
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangeSessionItemOrderUp(int id)
        {
            if (Session["ItemsList"] != null)
            {
                List<ItemUser> sessionItems = Session["ItemsList"] as List<ItemUser>;
                if (sessionItems.Count < 1)
                {
                    return View("New");
                }

                if (sessionItems.Any(i => i.Id < id))
                {
                    var selectedItem = sessionItems.SingleOrDefault(i => i.Id == id);
                    int busyItemId = sessionItems.LastOrDefault(i => i.Id < id).Id;
                    var busyItem = sessionItems.SingleOrDefault(i => i.Id == busyItemId);

                    var selectedItemIndex = sessionItems.FindIndex(i => i.Id == id);
                    var busyItemIndex = sessionItems.FindIndex(i => i.Id == busyItemId);

                    busyItem.Id = selectedItem.Id;
                    selectedItem.Id = busyItemId;

                    sessionItems[busyItemIndex] = selectedItem;
                    sessionItems[selectedItemIndex] = busyItem;
                    Session["ItemsList"] = sessionItems;
                }

            }
            return RedirectToAction("New", "Filters");
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangeSessionItemOrderDown(int id)
        {
            if (Session["ItemsList"] != null)
            {
                List<ItemUser> sessionItems = Session["ItemsList"] as List<ItemUser>;
                if (sessionItems.Count < 1)
                {
                    return View("New");
                }

                if (sessionItems.Any(i => i.Id > id))
                {
                    var selectedItem = sessionItems.SingleOrDefault(i => i.Id == id);
                    int busyItemId = sessionItems.FirstOrDefault(i => i.Id > id).Id;
                    var busyItem = sessionItems.SingleOrDefault(i => i.Id == busyItemId);

                    var selectedItemIndex = sessionItems.FindIndex(i => i.Id == id);
                    var busyItemIndex = sessionItems.FindIndex(i => i.Id == busyItemId);

                    busyItem.Id = selectedItem.Id;
                    selectedItem.Id = busyItemId;

                    sessionItems[busyItemIndex] = selectedItem;
                    sessionItems[selectedItemIndex] = busyItem;
                    Session["ItemsList"] = sessionItems;
                }

            }
            return RedirectToAction("New", "Filters");
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangeDbItemOrderUp(int id)
        {
            var selectedItem = _context.UsersItems.SingleOrDefault(i => i.Id == id);
            var filter = _context.Filters.SingleOrDefault(i => i.Id == selectedItem.FilterId);
            var itemsList = _context.UsersItems.Where(i => i.FilterId == filter.Id).OrderBy(i => i.RowId).ToList();
            List<ItemUser> newList = new List<ItemUser>();

            if (itemsList.Any(i => i.RowId < selectedItem.RowId))
            {
                var busyItem = itemsList.LastOrDefault(i => i.RowId < selectedItem.RowId);
                int selectedItemIndex = itemsList.FindIndex(f => f.Id == selectedItem.Id);
                int busyItemIndex = itemsList.FindIndex(f => f.Id == busyItem.Id);

                for (int i = 0; i < itemsList.Count; i++)
                {
                    if (i == selectedItemIndex)
                    {
                        busyItem.RowId = selectedItemIndex;
                        newList.Add(busyItem);
                    }
                    else if (i == busyItemIndex)
                    {
                        selectedItem.RowId = busyItemIndex;
                        newList.Add(selectedItem);
                    }
                    else
                    {
                        itemsList[i].RowId = i;
                        newList.Add(itemsList[i]);
                    }
                }
                _context.SaveChanges();
            }

            return RedirectToAction("Edit", "Filters", new { id = filter.Id });
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangeDbItemOrderDown(int id)
        {
            var selectedItem = _context.UsersItems.SingleOrDefault(i => i.Id == id);
            var filter = _context.Filters.SingleOrDefault(i => i.Id == selectedItem.FilterId);
            var itemsList = _context.UsersItems.Where(i => i.FilterId == filter.Id).OrderBy(i => i.RowId).ToList();
            List<ItemUser> newList = new List<ItemUser>();

            if (itemsList.Any(i => i.RowId > selectedItem.RowId))
            {
                var busyItem = itemsList.FirstOrDefault(i => i.RowId > selectedItem.RowId);
                int selectedItemIndex = itemsList.FindIndex(f => f.Id == selectedItem.Id);
                int busyItemIndex = itemsList.FindIndex(f => f.Id == busyItem.Id);

                for (int i = 0; i < itemsList.Count; i++)
                {
                    if (i == selectedItemIndex)
                    {
                        busyItem.RowId = selectedItemIndex;
                        newList.Add(busyItem);
                    }
                    else if (i == busyItemIndex)
                    {
                        selectedItem.RowId = busyItemIndex;
                        newList.Add(selectedItem);
                    }
                    else
                    {
                        itemsList[i].RowId = i;
                        newList.Add(itemsList[i]);
                    }
                }
                _context.SaveChanges();
            }

            return RedirectToAction("Edit", "Filters", new { id = filter.Id });
        }

        [Authorize]
        [HttpGet]
        public ActionResult New()
        {
            var viewModel = new FilterViewModel();
            if (Session["ItemsList"] != null)
            {
                List<ItemUser> items = Session["ItemsList"] as List<ItemUser>;
                if (items.Count > 0)
                {
                    viewModel.Items.AddRange(items);
                }
            }

            return View("Filter", viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Save(Models.Filters.Filter model)
        {
            //TODO powinien zwrócić podany model

            if (!ModelState.IsValid)
            {
                return View("Filter", new FilterViewModel());
            }

            bool isAuthorized = model.UserId == User.Identity.GetUserId();
            if (!isAuthorized && model.UserId != null)
            {
                return HttpNotFound();
            }

            if (model.UserId == null)
            {
                var newFilter = model;
                int rowId = 0;
                newFilter.CreateDate = DateTime.UtcNow;
                newFilter.EditDate = DateTime.UtcNow;
                newFilter.UserId = User.Identity.GetUserId();
                foreach (var item in newFilter.Items)
                {
                    item.RowId = rowId;
                    rowId++;
                }
                _context.Filters.Add(model);
            }
            else
            {
                var filterInDb = _context.Filters.SingleOrDefault(f => f.Id == model.Id);
                filterInDb.Dedicated = model.Dedicated;
                filterInDb.Description = model.Description;
                filterInDb.EditDate = DateTime.UtcNow;
                filterInDb.Name = model.Name;
            }

            try
            {
                _context.SaveChanges();
                Session.Clear();
            }
            catch (Exception)
            {
                return View("Filter", new FilterViewModel());
            }

            return RedirectToAction("MyFilters", "Filters");
        }
    }
}