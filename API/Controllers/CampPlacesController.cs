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
    public class CampPlacesController : ApiController
    {
        private ICampPlaceService campService;

        public CampPlacesController(ICampPlaceService campService)
        {
            this.campService = campService;
        }

        [Authorize]
        public HttpResponseMessage Get(int id)
        {
            var campPlaceId = id;

            var campPlace = new CampPlaceDTO();

            try
            {
                campPlace = campService.GetCampData(campPlaceId);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            if (campPlace == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);

           return Request.CreateResponse(HttpStatusCode.OK, campPlace);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage Search(string firstId)
        {
            var soughtName = firstId;

            var campPlaceList = new List<CampPlaceDTO>();

            try
            {
                campPlaceList = campService.Search(soughtName);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            if(campPlaceList.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, campPlaceList);
        }

        [HttpGet]
        [Authorize]
        public async Task<HttpResponseMessage> Users(string firstId)
        {
            var userName = firstId;

            var campList = new List<CampPlaceDTO>();

            try
            {
                campList = await campService.GetCampList(userName);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            if (campList.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, campList);
        }  

        [Authorize]
        public async Task<HttpResponseMessage> Post([FromBody]CampPlaceDTO campPlaceDTO)
        {
            var userName = RequestContext.Principal.Identity.Name;

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

        [Authorize]
        public async Task<HttpResponseMessage> Put(CampPlaceDTO campPlaceDTO)
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
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var campPlaceId = id;

            var userName = RequestContext.Principal.Identity.Name;

            try
            {
                await campService.Delete(userName, campPlaceId);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
