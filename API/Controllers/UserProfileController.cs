using CampBusinessLogic.DTO;
using CampBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class UserProfileController : ApiController
    {
        private IProfileService profileService;

        public UserProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [Authorize]
        public async Task<HttpResponseMessage> Get([FromUri]string userName)
        {
            var userProfile = await profileService.GetProfileData(userName);

            HttpResponseMessage response;

            if (userProfile == null)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            else
                response = Request.CreateResponse<ProfileDTO>(HttpStatusCode.OK, userProfile);

            return response;
        }

        [Authorize]
        public void Post(string userName, UserDTO userDTO)
        {

        }

        [Authorize]
        public void Put(string userName, UserDTO userDTO)
        {

        }

        [Authorize]
        public void Delete(string userName, UserDTO userDTO)
        {

        }
    }
}
