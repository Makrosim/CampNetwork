using System.Web.Http;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;
using System.Threading.Tasks;

namespace API.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await userService.Create(userDTO);

            if (!result.Succedeed)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userService.Dispose();
            }

            base.Dispose(disposing);
        }

    }
}
