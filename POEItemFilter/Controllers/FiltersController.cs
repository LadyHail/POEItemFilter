using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using POEItemFilter.Library;
using POEItemFilter.Library.Enumerables;
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

        public ActionResult MyFilters()
        {
            var currentUser = User.Identity.GetUserId();
            var viewModel = _context.Filters.Where(f => f.UserId == currentUser).ToList();
            return View(viewModel);
        }

        public ActionResult NewFilter()
        {
            if (Session["ItemsList"] != null)
            {
                ItemUserList viewModel = Session["ItemsList"] as ItemUserList;
                if (viewModel.UsersItems.Count > 0)
                {
                    return View(viewModel);
                }
            }
            return View();
        }

        public ActionResult AddItem(ItemUser item)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("NewItem", "UsersItems");
            }

            ItemUserList model = Session["ItemsList"] as ItemUserList;
            if (model == null)
            {
                item.Id = 0;
                Session["ItemsList"] = new ItemUserList();
                Session.Timeout = 30;
            }
            else
            {
                item.Id = model.UsersItems.Count;
            }
            ItemUserList viewModel = Session["ItemsList"] as ItemUserList;
            viewModel.UsersItems.Add(item);
            return View("NewFilter", viewModel);
        }

        public ActionResult AddItemEdit(ItemUser item)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("MyFilters", "UsersItems");
            }

            _context.UsersItems.Add(item);
            _context.SaveChanges();

            return RedirectToAction("EditFilter", new { id = item.FilterId });
        }

        [HttpPost]
        public JsonResult SaveFilter(string filterName, string description, string dedicated, string id)
        {
            Models.Filters.Filter newFilter = new Models.Filters.Filter();
            if (id != "" && id != null)
            {
                int filterId = int.Parse(id);
                newFilter = _context.Filters.SingleOrDefault(f => f.Id == filterId);
            }
            if (filterName != null && (id == "" || id == null))
            {
                //Create filter model
                newFilter.Name = filterName;
                newFilter.Description = description;
                newFilter.CreateDate = DateTime.UtcNow;
                newFilter.EditDate = newFilter.CreateDate;
                if (dedicated != null)
                {
                    newFilter.Dedicated = (Classes)int.Parse(dedicated);
                }
                newFilter.UserId = User.Identity.GetUserId();

                //Create file
                StreamWriter filterText = GenerateFilter.CreateTempFile(newFilter.Name);
                filterText.WriteLine("#Description: " + newFilter.Description);

                //Add items to file
                ItemUserList itemsList = Session["ItemsList"] as ItemUserList;
                if (itemsList != null)
                {
                    foreach (var item in itemsList.UsersItems)
                    {
                        newFilter.Items.Add(item);
                    }
                    GenerateFilter.SaveItems(filterText, itemsList, newFilter);

                    _context.Filters.Add(newFilter);
                    _context.SaveChanges();
                }
                filterText.Close();

                return Json(new { fileName = newFilter.Name }, JsonRequestBehavior.AllowGet);
            }
            else if (filterName != null && id != "" && id != null)
            {
                //Update filter model
                newFilter.Name = filterName;
                newFilter.Description = description;
                newFilter.EditDate = DateTime.Now;
                if (dedicated != null)
                {
                    newFilter.Dedicated = (Classes)int.Parse(dedicated);
                }
                _context.SaveChanges();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        [DeleteFile]
        public FileResult Download(string file)
        {
            file += ".filter";
            string fullPath = AppDomain.CurrentDomain.BaseDirectory + @"Filters\" + file;
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = file,
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(fullPath, System.Net.Mime.MediaTypeNames.Application.Octet, file);
        }

        [HttpPost]
        public JsonResult GenerateText(int id)
        {
            var filterInDb = _context.Filters.SingleOrDefault(f => f.Id == id);
            StreamWriter filterText = GenerateFilter.CreateTempFile(filterInDb.Name);
            filterText.WriteLine("#Description: " + filterInDb.Description);

            var itemsInDb = filterInDb.Items.ToList();
            ItemUserList itemsList = new ItemUserList()
            {
                UsersItems = itemsInDb,
            };
            GenerateFilter.SaveItems(filterText, itemsList, filterInDb);

            return Json(new { fileName = filterInDb.Name }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void ClearSession(string fileName)
        {
            if (fileName != "")
            {
                GenerateFilter.DeleteTempFile(fileName);
            }
            Session.Clear();
        }

        public ActionResult EditFilter(int id)
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

            var viewModel = new EditFilterViewModel();
            viewModel.Filter = filterInDb;
            viewModel.ItemsList = itemsInDb;
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var itemsInDb = _context.UsersItems.Where(i => i.FilterId == id).Select(i => i).ToList();
            if (itemsInDb == null)
            {
                return HttpNotFound();
            }
            return View(itemsInDb);
        }

        public ActionResult DeleteFilter(int id)
        {
            var filterInDb = _context.Filters.SingleOrDefault(f => f.Id == id);
            if (filterInDb == null)
            {
                return HttpNotFound();
            }

            _context.Filters.Remove(filterInDb);
            _context.SaveChanges();

            return null;
        }
    }
}