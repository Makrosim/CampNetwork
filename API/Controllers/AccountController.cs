using System.Web.Http;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System;

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
        public async Task<HttpResponseMessage> Register(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                await userService.Create(userDTO);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
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
