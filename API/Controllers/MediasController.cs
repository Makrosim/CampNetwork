using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;
using System.Web;
using System.Threading.Tasks;
using System.IO;

namespace API.Controllers
{
    public class MediasController : ApiController
    {
        private IMediaService mediaService { get; set; }

        public MediasController(IMediaService mediaService)
        {
            this.mediaService = mediaService;
        }

        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> Profiles(string id) //Fix, IO in API = Bad
        {
            int mediaId;

            try
            {
                string fileSaveLocation = HttpContext.Current.Server.MapPath("~/content/images");
                var streamProvider = new MultipartFormDataStreamProvider(fileSaveLocation);
                await Request.Content.ReadAsMultipartAsync(streamProvider);

                var imgname = streamProvider.FileData[0].Headers.ContentDisposition.FileName.Trim(new char[] { '"', '/' });
                var newpath = fileSaveLocation + "/" + imgname;
                File.Delete(newpath);
                File.Move(streamProvider.FileData[0].LocalFileName, newpath);

                mediaId = await mediaService.SaveMedia(newpath);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, mediaId);
        }
    }
}
