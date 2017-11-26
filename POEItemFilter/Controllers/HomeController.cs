using System.Web.Mvc;
using POEItemFilter.ViewModels;

namespace POEItemFilter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                //    MailMessage mailMessage = new MailMessage();
                //    mailMessage.From = new MailAddress(viewModel.Email);
                //    mailMessage.To.Add();
                //    mailMessage.Subject = viewModel.Subject;
                //    mailMessage.Body = viewModel.Message;

                //    SmtpClient smtp = new SmtpClient();
                //    smtp.Host = "smtp.gmail.com";
                //    smtp.Port = 587;
                //    smtp.Credentials = new System.Net.NetworkCredential
                //        ();
                //    smtp.EnableSsl = true;
                //    smtp.Send(mailMessage);

                //    ModelState.Clear();
                //    ViewBag.Message = "Thank you for contacting us";
                //}
                //catch (Exception e)
                //{
                //    ModelState.Clear();
                //    ViewBag.Message = $"Sorry we're facing problem here {e.Message}";
                //}
            }
            return View();
        }

        public ActionResult HowTo()
        {
            return View();
        }
    }
}