using System.Web.Mvc;
using System.Threading.Tasks;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;
using System;

namespace CampPresentation.Controllers
{

    public class HomeController : Controller
    {
        private IProfileService profileService;
        private IPostService postService;
        private IMessageService messageService;

        public HomeController(IProfileService profileService, IPostService postService, IMessageService messageService)
        {
            this.profileService = profileService;
            this.postService = postService;
            this.messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            if(Request.IsAuthenticated)
            {
                var profile = await profileService.GetProfileData(User.Identity.Name);

                if(profile == null)
                {
                    return Redirect("User/Index");
                }

                ViewBag.Profile = profile;

                var postsList = await postService.GetAllUsersPosts(User.Identity.Name);

                foreach (var post in postsList)
                    post.Messages = messageService.GetAllPostMessages(post.Id);

                ViewBag.Posts = postsList;      
                    
                return View();
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }

        [HttpPost]
        public async Task<RedirectResult> Index(int postId, string Text)
        {
            await messageService.CreateUsersMessage(User.Identity.Name, new MessageDTO
            {
                PostId = postId,
                Text = Text,
                Date = DateTime.Now
            });

            return Redirect("/Home/Index");
        }

        [HttpGet]
        public async Task<RedirectResult> DeleteComment(int messageId, int postId)
        {
            await messageService.DeleteUsersMessage(messageId, postId);

            return Redirect("/Home/Index");
        }
    }
}