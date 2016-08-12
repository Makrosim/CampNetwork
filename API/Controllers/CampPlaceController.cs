using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Interfaces;
using System.Threading.Tasks;

namespace API.Controllers
{
    [RoutePrefix("api/CampPlace")]
    public class CampPlaceController : ApiController
    {
        private ICampPlaceService campService;

        public CampPlaceController(ICampPlaceService campService)
        {
            this.campService = campService;
        }

        [Authorize]
        [HttpPost]
        [Route("Save")]
        public async Task<HttpResponseMessage> Save([FromUri]string userName, [FromBody]CampPlaceDTO campPlaceDTO)
        {
            var result = await campService.Create(userName, campPlaceDTO);

            HttpResponseMessage response;

            if (result == null)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            else
                response = Request.CreateResponse(HttpStatusCode.OK);

            return response;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage Get(int campId)
        {
            var campPlace = campService.GetCampData(campId);

            HttpResponseMessage response;

            if (campPlace == null)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            else
                response = Request.CreateResponse<CampPlaceDTO>(HttpStatusCode.OK, campPlace);

            return response;
        }

        [HttpGet]
        [Authorize]
        [Route("GetList")]
        public async Task<HttpResponseMessage> GetList([FromUri]string userName)
        {
            var campList = await campService.GetCampList(userName);

            HttpResponseMessage response;

            if (campList == null)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            else
                response = Request.CreateResponse<List<CampPlaceDTO>>(HttpStatusCode.OK, campList);

            return response;
        }

        [HttpGet]
        [Authorize]
        [Route("GetPoints")]
        public HttpResponseMessage GetPoints()
        {
            var pointsList = campService.GetPointsList();

            HttpResponseMessage response;

            if (pointsList == null)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            else
                response = Request.CreateResponse<List<string>>(HttpStatusCode.OK, pointsList);

            return response;
        }

        [Authorize]
        public async Task<HttpResponseMessage> Put(CampPlaceDTO campPlaceDTO) //Update
        {
            var result = await campService.Update(campPlaceDTO);

            HttpResponseMessage response;

            if (result == null)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            else
                response = Request.CreateResponse(HttpStatusCode.OK);

            return response;
        }

        [Authorize]
        public HttpResponseMessage Delete(int Id) //Update
        {
            var result = campService.Delete(Id);

            HttpResponseMessage response;

            if (result == null)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            else
                response = Request.CreateResponse(HttpStatusCode.OK);

            return response;
        }

    }
}
