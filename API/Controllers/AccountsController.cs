using System.Web.Http;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System;
using Microsoft.AspNet.Identity;

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
                string errorString = "Ошибка создания пользователя: ";

                foreach (var modelState in ModelState.Values)
                    foreach(var error in modelState.Errors)
                        errorString += error.ErrorMessage;

                return Request.CreateResponse(HttpStatusCode.BadRequest, errorString);
            }

            try
            {
                await userService.Create(registerDTO);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
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
