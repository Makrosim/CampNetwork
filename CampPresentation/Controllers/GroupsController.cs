using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CampAuth.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace CampAuth.Controllers
{
    public class GroupsController : Controller
    {
        AppContext db = new AppContext();

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Groups = db.Groups.ToList();

            return View();
        }

        [HttpGet]
        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Open(int Id)
        {
            var group = db.Groups.Find(Id);
            ViewBag.Group = group;
            ViewBag.GroupPosts = group.Posts.ToList();

            return View();
        }

        [HttpPost]
        public RedirectResult Open(int grId, int ArtId)
        {
            Group gr = db.Groups.Find(grId);
            gr.Posts.Add(db.Posts.Find(ArtId));
            db.SaveChanges();

            return Redirect($"/Groups/Open/{grId}");
        }

        [HttpPost]
        public async Task<RedirectResult> CreateGroup(Group gr)
        {
            User currUser = await UserManager.FindByEmailAsync(User.Identity.Name);
            gr.Creator = currUser;
            gr.Members.Add(currUser);
            db.Groups.Add(gr);
            db.SaveChanges();

            return Redirect("/Groups/Index");
        }

        [HttpPost]
        public async Task<RedirectResult> Comment(int ArtId, string Text)
        {
            User tmpUser = await UserManager.FindByEmailAsync(User.Identity.Name);
            var currUser = db.Users.Find(tmpUser.Id);

            var mes = new Message
            {
                Author = currUser,
                Text = Text,
                Date = DateTime.Now
            };

            db.Messages.Add(mes);
            db.SaveChanges();

            db.Posts.Find(1).Messages.Add(mes.Id);

            db.SaveChanges();

            return Redirect($"/Groups/Open/{ArtId}");
        }

        [HttpGet]
        public RedirectResult DeleteComment(int ArtId, int Id)
        {
            var com = db.Messages.Find(Id);
            db.Messages.Remove(com);
            db.SaveChanges();

            return Redirect($"/Groups/Open/{ArtId}");
        }
    }
}