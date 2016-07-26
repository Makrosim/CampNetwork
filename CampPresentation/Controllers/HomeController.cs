using System.Web.Mvc;
using System.Threading.Tasks;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;

namespace CampPresentation.Controllers
{

    public class HomeController : Controller
    {
        private IUserProfileService profileService;
        private IPostService postService;
        private IMessageService messageService;

        public HomeController(IUserProfileService profileService, IPostService postService, IMessageService messageService)
        {
            this.profileService = profileService;
            this.postService = postService;
            this.messageService = messageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if(Request.IsAuthenticated)
            {
                ViewBag.User = profileService.GetProfileData(User.Identity.Name);
                ViewBag.Posts = postService.GetAllUsersPosts(User.Identity.Name);

                return View();
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }

        [HttpPost]
        public async Task<RedirectResult> Index(int PostId, string Text)
        {
            await messageService.CreateUsersMessage(new MessageDTO
            {
                Email = User.Identity.Name,
                PostId = PostId,
                Text = Text
            });

            return Redirect("/Home/Index");
        }

        [HttpGet]
        public RedirectResult DeleteComment(int Id)
        {
            messageService.DeleteUsersMessage(Id);

            return Redirect("/Home/Index");
        }
    }
}