using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BitBookApp.Models;

namespace BitBookApp.Controllers
{
    public class RegistrationController : Controller
    {
        private BitBookDbContext db = new BitBookDbContext();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,EmailAddress,Password,ConfirmPassword")] UserRegistration userregistration)
        {
            if (ModelState.IsValid)
            {
                if (db.Registrations.Where(x => x.EmailAddress == userregistration.EmailAddress).Select(x=>x.EmailAddress).FirstOrDefault() == null)
                {
                    db.Registrations.Add(userregistration);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Login");
                }
                ViewBag.Error = "Email address already exists";
            }
            return View(userregistration);
        }

        public ActionResult Edit()
        {
            int id = Convert.ToInt32(Session["UserId"]);

            UserRegistration userRegistration = db.Registrations.FirstOrDefault(x=> x.UserId == id);
            if (userRegistration == null)
            {
                return HttpNotFound();
            }
            return View("ChangePassword", userRegistration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserRegistration userRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRegistration).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Success = "Password updated successfully";
            }
            return View("ChangePassword");
        }
    }
}
