using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BitBookApp.Models;

namespace BitBookApp.Controllers
{
    public class LoginController : Controller
    {
        private BitBookDbContext db = new BitBookDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            if (ModelState.IsValid)
            {
                if (db.Registrations.FirstOrDefault(x => x.EmailAddress.Contains(login.EmailAddress) && x.Password.Contains(login.Password)) != null)
                {
                    FormsAuthentication.SetAuthCookie(login.EmailAddress, true);
                    Session["Username"] = login.EmailAddress;
                    Session["UserId"] = db.Registrations.Where(x => x.EmailAddress == login.EmailAddress).Select(x => x.UserId).FirstOrDefault();
                    
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Error = "Login Failed";
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
            }
            return Redirect("~/Login/Index");
        }
	}
}