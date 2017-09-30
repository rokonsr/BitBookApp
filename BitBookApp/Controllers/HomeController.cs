using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitBookApp.Models;
using Microsoft.Ajax.Utilities;

namespace BitBookApp.Controllers
{
    public class HomeController : Controller
    {
        private BitBookDbContext db = new BitBookDbContext();

        // GET: /Home/
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            List<Post> posts = new List<Post>();
            List<Friend> friend = db.Friends.Where(x => x.UserId == userId && x.Status.Equals(true)).ToList();
            List<Friend> friend1 = db.Friends.Where(x => x.FriendId == userId && x.Status.Equals(true)).ToList();

            foreach (var frnd in friend)
            {
                posts.AddRange(db.Posts.Where(x => x.UserId == frnd.FriendId));
            }

            foreach (var frd in friend1)
            {
                posts.AddRange(db.Posts.Where(x => x.UserId == frd.UserId));
            }
            posts.AddRange(db.Posts.Where(x => x.UserId == userId));

            ViewBag.Profile = db.UserProfiles.ToList();

            List<Like> likes = new List<Like>();

            foreach (Like lk in db.Likes.ToList())
            {
                Like like = new Like();
                like.PostId = lk.PostId;

                int sts = db.Likes.Count(st => lk.PostId == st.PostId);
                like.Status = sts;
                likes.Add(like);
            }

            ViewBag.TotalLike = likes;

            ViewBag.Comment = db.Comments.ToList();

            return View(posts);
        }

        // GET: /Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: /Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post, HttpPostedFileBase image)
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
                post.ImagePost = bytes;
                post.UserId = Convert.ToInt32(Session["UserId"]);
                post.PostDate = DateTime.Now;

                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            if (post.TextPost != null && image == null)
            {
                post.UserId = Convert.ToInt32(Session["UserId"]);
                post.PostDate = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View("Index");
        }

        // GET: /Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: /Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post, HttpPostedFileBase image)
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
                post.ImagePost = bytes;
                post.PostDate = DateTime.Now;

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            if (post.TextPost != null && image == null)
            {
                post.PostDate = DateTime.Now;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View("Index");



            //if (ModelState.IsValid)
            //{
            //    db.Entry(post).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(post);
        }

        // GET: /Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: /Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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

        [HttpPost]
        public JsonResult CreateLike(int postId)
        {
            int userId = db.Posts.Where(x => x.PostId == postId).Select(x => x.UserId).FirstOrDefault();

            Like like = new Like();
            like.UserId = userId;
            like.PostId = postId;
            like.Status = 1;

            int exist = db.Likes.Count(x => x.UserId == userId && x.PostId == postId);

            if (exist == 0)
            {
                db.Likes.Add(like);
                db.SaveChanges();
            }

            int likeCounter = db.Likes.Count(x => x.PostId == postId);

            return Json(likeCounter, JsonRequestBehavior.AllowGet);
            //return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(int userId, int postId, string userComment)
        {
            Comment comment = new Comment();
            comment.UserId = userId;
            comment.PostId = postId;
            comment.UserComment = userComment;

            int exist = db.Comments.Count(x => x.UserId == userId && x.PostId == postId);

            if (exist == 0)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult BrowseLikers(int? postId)
        {
            var friendList =
                db.Likes.Join(db.UserProfiles, up => up.UserId, f => f.UserId, (up, f) => new { up, f })
                    .Where(x => x.up.PostId == postId).Select(m => new ViewFriendRequiest
                    {
                        FriendId = m.up.UserId,
                        FriendName = m.f.Name,
                    }).ToList();

            ViewBag.FriendList = friendList;

            return View(friendList);
        }
    }
}
