using System;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BitBookApp.Models;

namespace BitBookApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private BitBookDbContext db = new BitBookDbContext();

        // GET: /Profile/
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            var coverPictures = db.ProfileImages.Where(x => x.UserId == userId && x.ImageTypeId == 1).ToList();
            ViewBag.CoverPicture = coverPictures;

            var profilePicture = db.ProfileImages.Where(x => x.UserId == userId && x.ImageTypeId == 2).ToList();
            ViewBag.ProfilePicture = profilePicture;

            var education = db.Educations.Where(x => x.UserId == userId).ToList();
            ViewBag.Education = education;

            var experience = db.Experiences.Where(x => x.UserId == userId).ToList();
            ViewBag.Experience = experience;

            return View(db.UserProfiles.FirstOrDefault(x => x.UserId == userId));
        }

        public ActionResult ProfileView(int? id)
        {
            var coverPictures = db.ProfileImages.Where(x => x.UserId == id && x.ImageTypeId == 1).ToList();
            ViewBag.CoverPicture = coverPictures;

            var profilePicture = db.ProfileImages.Where(x => x.UserId == id && x.ImageTypeId == 2).ToList();
            ViewBag.ProfilePicture = profilePicture;

            var education = db.Educations.Where(x => x.UserId == id).ToList();
            ViewBag.Education = education;

            var experience = db.Experiences.Where(x => x.UserId == id).ToList();
            ViewBag.Experience = experience;

            return View("Index", db.UserProfiles.FirstOrDefault(x => x.UserId == id));
        }

        // GET: /Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        // GET: /Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ProfileId,UserId,Name,Gender,Contact,City,Country,AreaOfInterest,ProfilePhoto,CoverPhoto")] UserProfile userprofile)
        {
            userprofile.UserId = Convert.ToInt32(Session["UserId"]);

            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userprofile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userprofile);
        }

        // GET: /Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        // POST: /Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProfileId,UserId,Name,Gender,Contact,City,Country,AreaOfInterest,ProfilePhoto,CoverPhoto")] UserProfile userprofile)
        {
            userprofile.UserId = Convert.ToInt32(Session["UserId"]);
            if (ModelState.IsValid)
            {
                db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }

        // GET: /Profile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        // POST: /Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userprofile);
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

        public ActionResult SearchFreind(string searchFriend)
        {
            var profilePicture = db.ProfileImages.Where(x => x.ImageTypeId == 2).ToList();
            ViewBag.ProfilePicture = profilePicture;

            var friends = db.UserProfiles.Where(x => x.Name.Contains(searchFriend)).ToList();

            return View(friends);
        }

        public ActionResult SendFriendRequest(int? id)
        {
            return View(db.UserProfiles.FirstOrDefault(x => x.UserId == id));
        }

        [HttpPost]
        public ActionResult SendFriendRequest(UserProfile userProfile)
        {
            Friend friend = new Friend();
            friend.UserId = Convert.ToInt32(Session["UserId"]);
            friend.FriendId = userProfile.UserId;

            if (ModelState.IsValid)
            {
                if (db.Friends.Count(x=> x.FriendId == friend.FriendId && x.UserId == friend.UserId) == 0)
                {
                    db.Friends.Add(friend);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Exist = "Already sent friend request";
            }
            return View();
        }

        public ActionResult ViewFriendRequest()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            var profilePicture = db.ProfileImages.Where(x => x.ImageTypeId == 2).ToList();
            ViewBag.ProfilePicture = profilePicture;

            var friendList =
                db.Friends.Join(db.UserProfiles, up => up.UserId, f => f.UserId, (up, f) => new {up, f})
                    .Where(x => x.up.FriendId == userId && x.up.Status.Equals(false)).Select (m => new ViewFriendRequiest
                    {
                        FriendId = m.up.UserId,
                        FriendName = m.f.Name,
                    }).ToList();

            ViewBag.FriendList = friendList;

            return View(friendList);
        }

        public ActionResult ConfirmRequest(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userprofile = db.UserProfiles.FirstOrDefault(x=>x.UserId == id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        [HttpPost]
        public ActionResult ConfirmRequest(int id)
        {
            foreach (var friend in db.Friends.Where(x => x.UserId == id).ToList())
            {
                friend.Status = true;
            }
            db.SaveChanges();

            UserProfile userprofile = db.UserProfiles.FirstOrDefault(x => x.UserId == id);

            return View("Confirmation", userprofile);
        }

        public ActionResult ViewFriend()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            var profilePicture = db.ProfileImages.Where(x => x.ImageTypeId == 2).ToList();
            ViewBag.ProfilePicture = profilePicture;

            var friendList =
                db.Friends.Join(db.UserProfiles, up => up.UserId, f => f.UserId, (up, f) => new { up, f })
                    .Where(x => x.up.FriendId == userId && x.up.Status.Equals(true) || x.up.UserId == userId && x.up.Status.Equals(true)).Select(m => new ViewFriendRequiest
                    {
                        FriendId = m.up.UserId,
                        FriendName = m.f.Name,
                    }).ToList();

            ViewBag.FriendList = friendList;

            return View(friendList);
        }

        public ActionResult Unfriend(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userprofile = db.UserProfiles.FirstOrDefault(x => x.UserId == id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        [HttpPost]
        public ActionResult Unfriend(int id)
        {
            foreach (var friend in db.Friends.Where(x => x.UserId == id).ToList())
            {
                friend.Status = false;
            }
            db.SaveChanges();

            UserProfile userprofile = db.UserProfiles.FirstOrDefault(x => x.UserId == id);

            return View("UnfriendConfirmation", userprofile);
        }
    }
}
