using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitBookApp.Models;

namespace BitBookApp.Controllers
{
    public class ProfileImageController : Controller
    {
        private BitBookDbContext db = new BitBookDbContext();

        // GET: /ProfileImage/
        public ActionResult Index()
        {
            return View(db.ProfileImages.ToList());
        }

        // GET: /ProfileImage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileImage profileimage = db.ProfileImages.Find(id);
            if (profileimage == null)
            {
                return HttpNotFound();
            }
            return View(profileimage);
        }

        // GET: /ProfileImage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ProfileImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfileImage profileimage, HttpPostedFileBase image)
        {
            bool status;

                try
                {

                    byte[] bytes;
                    int BytestoRead;
                    int numBytesRead;
                    if (image != null)
                    {
                        bytes = new byte[image.ContentLength];
                        BytestoRead = (int)image.ContentLength;
                        numBytesRead = 0;
                        while (BytestoRead > 0)
                        {
                            int n = image.InputStream.Read(bytes, numBytesRead, BytestoRead);
                            if (n == 0) break;
                            numBytesRead += n;
                            BytestoRead -= n;
                        }
                        profileimage.UserImage = bytes;
                        profileimage.UserId = Convert.ToInt32(Session["UserId"]);

                        db.ProfileImages.Add(profileimage);
                        db.SaveChanges();

                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
                catch (Exception)
                {
                    status = false;
                }

            if (status)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View(profileimage);
        }

        // GET: /ProfileImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileImage profileimage = db.ProfileImages.Find(id);
            if (profileimage == null)
            {
                return HttpNotFound();
            }
            return View(profileimage);
        }

        // POST: /ProfileImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProfileImageId,UserId,ImageTypeId,Image")] ProfileImage profileimage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profileimage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profileimage);
        }

        // GET: /ProfileImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileImage profileimage = db.ProfileImages.Find(id);
            if (profileimage == null)
            {
                return HttpNotFound();
            }
            return View(profileimage);
        }

        // POST: /ProfileImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProfileImage profileimage = db.ProfileImages.Find(id);
            db.ProfileImages.Remove(profileimage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
