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
        private ICampPlaceService campPlaceService;

        public CampPlacesController(ICampPlaceService campService)
        {
            this.campPlaceService = campService;
        }

        [Authorize]
        public HttpResponseMessage Get(int id)
        {
            var campPlace = new CampPlaceDTO();

            try
            {
                campPlace = campPlaceService.GetCampData(id);
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
            var campPlaceList = new List<CampPlaceDTO>();

            try
            {
                campPlaceList = campPlaceService.Search(firstId);
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
            var campList = new List<CampPlaceDTO>();

            try
            {
                campList = await campPlaceService.GetCampList(firstId);
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
                await campPlaceService.Create(userName, campPlaceDTO);
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
            var userName = RequestContext.Principal.Identity.Name;

            try
            {
               await campPlaceService.Update(userName, campPlaceDTO);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, ex);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Authorize]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var userName = RequestContext.Principal.Identity.Name;

            try
            {
                await campPlaceService.Delete(userName, id);
            }
            catch(UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, ex);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}