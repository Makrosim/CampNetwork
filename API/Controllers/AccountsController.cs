using System.Web.Http;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System;

namespace API.Controllers
{
    public class AccountsController : ApiController
    {
        private IUserService userService;

        public AccountsController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        public async Task<HttpResponseMessage> Post(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                await userService.Create(registerDTO);
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
