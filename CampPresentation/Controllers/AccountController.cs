using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using System.Security.Claims;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.Infrastructure;

namespace CamppPresentation.Controllers
{
    public class AccountController : Controller
    {
        private IUserService userService;
        
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            var userDto = new UserDTO { UserName = loginDTO.Login, Password = loginDTO.Password };
            var claim = await userService.Authenticate(userDto);

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(LoginDTO loginDTO)
        {
            UserDTO userDto = new UserDTO
            {
                UserName = loginDTO.Login,
                Email = loginDTO.Email,
                Password = loginDTO.Password,
                Role = "user"
            };
            OperationDetails operationDetails = await userService.Create(userDto);

            ClaimsIdentity claim = await userService.Authenticate(userDto);

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);

            return Redirect("/User/Index");
        }
    }
}