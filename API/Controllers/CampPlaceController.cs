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
    public class CampPlaceController : ApiController
    {
        private ICampPlaceService campService;

        public CampPlaceController(ICampPlaceService campService)
        {
            this.campService = campService;
        }

        [Authorize]
        public HttpResponseMessage Get(int campPlaceId)
        {
            var campPlace = campService.GetCampData(campPlaceId);

            HttpResponseMessage response;

            if (campPlace == null)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            else
                response = Request.CreateResponse(HttpStatusCode.OK, campPlace);

            return response;
        }

        [Authorize]
        public HttpResponseMessage Get(string campPlaceName)
        {
            HttpResponseMessage response;
            var campPlaceList = new List<CampPlaceDTO>();

            try
            {
                campPlaceList = campService.SearchByName(campPlaceName);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            if(campPlaceList.Count == 0)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, campPlaceList);
            }

            return response;
        }

        [HttpGet]
        [Authorize]
        [Route("api/CampPlace/GetList")]
        public HttpResponseMessage GetList()
        {
            HttpResponseMessage response;

            var campList = new List<CampPlaceDTO>();

            try
            {
                campList = campService.GetCampList();
            }
            catch(Exception ex)
            {
                return response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            if (campList == null)
                return response = Request.CreateResponse(HttpStatusCode.NoContent);

            return response = Request.CreateResponse(HttpStatusCode.OK, campList);
        }

        [HttpGet]
        [Authorize]
        [Route("api/CampPlace/GetList")]
        public async Task<HttpResponseMessage> GetList([FromUri]string userName)
        {
            var campList = await campService.GetCampList(userName);

            HttpResponseMessage response;

            if (campList == null)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            else
                response = Request.CreateResponse(HttpStatusCode.OK, campList);

            return response;
        }

        [Authorize]
        public async Task<HttpResponseMessage> Post([FromUri]string userName, [FromBody]CampPlaceDTO campPlaceDTO)
        {
            try
            {
                await campService.Create(userName, campPlaceDTO);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Authorize]
        [Route("api/CampPlace/GetPoints")]
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
            try
            {
               await campService.Update(campPlaceDTO);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Authorize]
        public HttpResponseMessage Delete(int Id) //Update
        {
            try
            {
                campService.Delete(Id);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);

        }

    }
}
