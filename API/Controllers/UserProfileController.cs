using CampBusinessLogic.DTO;
using CampBusinessLogic.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class UserProfileController : ApiController
    {
        private IProfileService profileService;
        private IMediaService mediaService;

        public UserProfileController(IProfileService profileService, IMediaService mediaService)
        {
            this.profileService = profileService;
            this.mediaService = mediaService;
        }

        [Authorize]
        public HttpResponseMessage Get(int mediaId)
        {
            var path = mediaService.GetMediaPath(mediaId);

            byte[] imageArray = File.ReadAllBytes(path);

            string baseImage = Convert.ToBase64String(imageArray);

            if (baseImage == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            else
                return Request.CreateResponse(HttpStatusCode.OK, baseImage);
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
        public async Task<HttpResponseMessage> Post()
        {
            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data");
            var streamProvider = new MultipartFormDataStreamProvider(fileSaveLocation);
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            var imgname = streamProvider.FileData[0].Headers.ContentDisposition.FileName.Trim(new char[] { '"', '/' });
            var newpath = fileSaveLocation + "/" + imgname;
            File.Delete(newpath);
            File.Move(streamProvider.FileData[0].LocalFileName, newpath);

            var mediaId = await mediaService.SaveMedia(newpath);

            var response = Request.CreateResponse(HttpStatusCode.OK, mediaId);

            return response;
        }

        [Authorize]
        public async Task<HttpResponseMessage> Post([FromUri]string userName, [FromBody]ProfileDTO profileDTO)
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
