using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CampAuth.Models;

namespace CampAuth.Controllers
{
    public class ArticleController : Controller
    {
        AppContext db = new AppContext();

        [HttpGet]
        public ActionResult Index(int id)
        {
            ViewBag.CampPlaceID = id;

            return View();
        }

        [HttpGet]
        public RedirectResult Delete(int id)
        {
            var currart = db.Posts.Find(id);
            var list = new List<int>();

            foreach (var a in currart.Messages)
            {
                list.Add(a);
            }

            foreach (var a in list)
            {
                db.Messages.Remove(db.Messages.Find(a));
            }

            db.Posts.Remove(currart);
            db.SaveChanges();

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public RedirectResult Index(Post post)
        {
            post.CreationDate = DateTime.Now;
            db.Posts.Add(post);
            db.SaveChanges();
            post.CampPlace = db.CampPlaces.Find(post.CampPlaceID);
            db.SaveChanges();

            return Redirect("/Home/Index");
        }
    }
}