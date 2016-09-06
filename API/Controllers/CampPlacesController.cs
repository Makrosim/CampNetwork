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
        public async Task<HttpResponseMessage> Get()
        {
            var userName = RequestContext.Principal.Identity.Name;

            var campList = new List<CampPlaceDTO>();

            try
            {
                campList = await campService.GetCampList(userName);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            if (campList == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, campList);
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
        public HttpResponseMessage Search(string soughtName)
        {
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
        public async Task<HttpResponseMessage> Users(string id)
        {
            var campList = new List<CampPlaceDTO>();

            try
            {
                campList = await campService.GetCampList(id);
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
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                campService.Delete(id);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
