using CampBusinessLogic.DTO;
using CampBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/UserProfile")]
    public class UserProfileController : ApiController
    {
        private byte[] image;
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
        [HttpPost]
        [Route("PostImage")]
        public async Task<HttpResponseMessage> PostImage()
        {
            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data");
            var streamProvider = new MultipartFormDataStreamProvider(fileSaveLocation);
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            var response = Request.CreateResponse(HttpStatusCode.OK);

            return response;
        }

        [Authorize]
        [HttpPost]
        [Route("PostProfile")]
        public async Task<HttpResponseMessage> PostProfile([FromUri]string userName, [FromBody]ProfileDTO profileDTO)
        {
            await profileService.SetProfileData(userName, profileDTO);

            var response = Request.CreateResponse(HttpStatusCode.OK);

            return response;
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
