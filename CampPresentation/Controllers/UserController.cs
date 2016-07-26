using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;

namespace CampAuth.Controllers
{
    public class UserController : Controller
    {
        IUserProfileService profileService;

        [HttpGet]
        public ActionResult Index()
        {
            ProfileDTO us = profileService.GetProfileData(User.Identity.Name);
            ViewBag.User = us;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ProfileDTO us, HttpPostedFileBase image)
        {
            us.Image = image.InputStream;
            await profileService.SetProfileData(us);
        
            return View();
        }
    }
}