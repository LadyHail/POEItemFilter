using System;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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

        // GET: Filters
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewFilter()
        {
            if (Session["ItemsList"] != null)
            {
                ItemUserList viewModel = Session["ItemsList"] as ItemUserList;
                return View(viewModel);
            }
            return View();
        }

        public ActionResult AddItem(ItemUser item)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("NewItem", "UsersItems");
            }
            if (Session["ItemsList"] == null)
            {
                Session["ItemsList"] = new ItemUserList();
                Session.Timeout = 30;
            }

            ItemUserList viewModel = Session["ItemsList"] as ItemUserList;
            if (Session.IsNewSession)
            {
                item.Id = 0;
            }
            else
            {
                item.Id = viewModel.UsersItems.Count;
            }
            viewModel.UsersItems.Add(item);

            return View("NewFilter", viewModel);
        }

        [HttpPost]
        public JsonResult SaveFilter(string filterName, string description, string dedicated)
        {
            Models.Filters.Filter newFilter = new Models.Filters.Filter();
            if (filterName != null)
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
                var test = User.Identity.GetUserId();
                newFilter.UserId = test;
                _context.Filters.Add(newFilter);
                _context.SaveChanges();

                //Create file
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Filters");
                FileInfo filterFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Filters\" + newFilter.Name + ".filter");
                StreamWriter filterText = filterFile.AppendText();
                filterText.WriteLine("#Description: " + newFilter.Description);

                //Add items to file
                ItemUserList itemsList = (ItemUserList)Session["ItemsList"] as ItemUserList;
                if (itemsList != null)
                {
                    foreach (var item in itemsList.UsersItems)
                    {
                        if (item.Show == true)
                        {
                            filterText.WriteLine("Show");
                        }
                        else
                        {
                            filterText.WriteLine("Hide");
                        }

                        string output = "";
                        Type type = item.GetType();
                        PropertyInfo[] properties = type.GetProperties();

                        foreach (PropertyInfo property in properties)
                        {
                            if (property.GetValue(item, null) != null)
                            {
                                if (property.Name != "Id" &&
                                    property.Name != "Show" &&
                                    property.Name != "Attribute1" &&
                                    property.Name != "Attribute2")
                                {
                                    if (property.Name != "SetFontSize" && property.GetValue(item, null).ToString() != "32")
                                    {
                                        output = "    " + (property.Name + " " + property.GetValue(item, null).ToString()).Trim();
                                        filterText.WriteLine(output);
                                    }
                                }
                            }
                        }
                    }
                }
                filterText.Close();

                return Json(new { fileName = newFilter.Name }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public FileResult Download(string file)
        {
            file += ".filter";
            string fullPath = AppDomain.CurrentDomain.BaseDirectory + @"\Filters\" + file;
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = file,
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(fullPath, System.Net.Mime.MediaTypeNames.Application.Octet, file);
        }

        [HttpPost]
        public void ClearSession()
        {
            Session.Clear();
        }
    }
}