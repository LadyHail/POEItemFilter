using System;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using POEItemFilter.Models;
using POEItemFilter.ViewModels;

namespace POEItemFilter.Controllers
{
    public class FiltersController : Controller
    {
        // GET: Filters
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewFilter()
        {
            return View();
        }

        public ActionResult AddItem(ItemUser item)
        {
            if (Session["ItemsList"] == null)
            {
                Session["ItemsList"] = new ItemUserList();
                Session.Timeout = 30;
            }

            ItemUserList viewModel = Session["ItemsList"] as ItemUserList;
            viewModel.UsersItems.Add(item);

            return View("NewFilter", viewModel);
        }

        [HttpPost]
        public JsonResult SaveFilter(string filterName, string description)
        {
            Models.Filters.Filter newFilter = new Models.Filters.Filter();
            if (filterName != null)
            {
                //Create filter model
                newFilter.Name = filterName;
                newFilter.Description = description;
                newFilter.CreateDate = DateTime.UtcNow;
                newFilter.EditDate = newFilter.CreateDate;

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
                                if (property.Name != "Id" && property.Name != "Show")
                                {
                                    output = "    " + (property.Name + " " + property.GetValue(item, null).ToString()).Trim();
                                    filterText.WriteLine(output);
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
    }
}