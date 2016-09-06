﻿using CampBusinessLogic.DTO;
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
    public class ProfilesController : ApiController
    {
        private IProfileService profileService;

        public ProfilesController(IProfileService profileService, IMediaService mediaService)
        {
            this.profileService = profileService;
        }

        [Authorize]
        public async Task<HttpResponseMessage> Get()
        {
            var userName = RequestContext.Principal.Identity.Name;

            var userProfile = new ProfileDTO(); ;

            try
            {
                userProfile = await profileService.GetProfileData(userName);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, userProfile);
        }

        [Authorize]
        public async Task<HttpResponseMessage> Get(string ownerName)
        {
            var userProfile = new ProfileDTO();;

            try
            {
                userProfile = await profileService.GetProfileData(ownerName);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, userProfile);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage Search(string soughtName)
        {
            var profileList = profileService.Search(soughtName);

            if (profileList.Count == 0)
                return Request.CreateResponse(HttpStatusCode.InternalServerError); 

            return Request.CreateResponse(HttpStatusCode.OK, profileList);
        }

        [Authorize]
        public async Task<HttpResponseMessage> Post([FromBody]ProfileDTO profileDTO)
        {
            var userName = RequestContext.Principal.Identity.Name;

            try
            {
                await profileService.SetProfileData(userName, profileDTO);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK); ;
        }

    }
}
