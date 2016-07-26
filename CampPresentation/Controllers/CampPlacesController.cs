using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CampAuth.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace CampAuth.Controllers
{
    public class CampPlacesController : Controller
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
        public async Task<ActionResult> Index()
        {
            User currUser = await UserManager.FindByEmailAsync(User.Identity.Name);
            var cps = currUser.CampPlaces.ToList();
            ViewBag.CP = cps;

            var points = new List<string>();

            foreach (var cp in cps)
            {
                points.Add(cp.LocationX + " " + cp.LocationY + " " + cp.Name);
            }

            ViewBag.Points = points;

            return View();
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            ViewBag.CP = db.CampPlaces.Find(id);

            return View();
        }

        [HttpPost]
        public RedirectResult Update(CampPlace cp)
        {
            db.Entry(cp).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/CampPlaces/Index");
        }

        [HttpGet]
        public ActionResult CreateCampPlace()
        {
            return View();
        }

        [HttpPost]
        public async Task<RedirectResult> CreateCampPlace(CampPlace place)
        {
            var tmpUser = await UserManager.FindByEmailAsync(User.Identity.Name); //Проблемы с контекстами?
            var currUser = db.Users.Find(tmpUser.Id);

            place.User = currUser;
            db.CampPlaces.Add(place);
            db.SaveChanges();
            ViewBag.CP = currUser.CampPlaces.ToList();

            return Redirect("/CampPlaces/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            db.CampPlaces.Remove(db.CampPlaces.Find(id));
            db.SaveChanges();

            return View("Index");
        }
    }
}