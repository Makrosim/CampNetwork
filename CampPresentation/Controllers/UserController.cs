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
        IProfileService profileService;

        public UserController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var us = await profileService.GetProfileData(User.Identity.Name);

            if(us == null)
            {
                us = new ProfileDTO
                {
                    FirstName = "",
                    LastName = "",
                    BirthDateDay = "",
                    BirthDateMounth = "",
                    BirthDateYear = "",
                    Address = "",
                    Phone = "",
                    Skype = "",
                    AdditionalInformation = ""
                };
            }

            ViewBag.Profile = us;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ProfileDTO us, HttpPostedFileBase image)
        {
            if (image != null)
            {
                us.Avatar = new byte[image.InputStream.Length];
                image.InputStream.Read(us.Avatar, 0, (int)image.InputStream.Length);
            }
            await profileService.SetProfileData(User.Identity.Name, us);
        
            return Redirect("/Home/Index");
        }
    }
}