using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class GroupsController : ApiController
    {
        private IGroupService groupService;

        public GroupsController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet]
        [Authorize]
        public async Task<HttpResponseMessage> Users(string firstId)
        {
            var groupList = new List<GroupDTO>();

            try
            {
                groupList = await groupService.GetUsersGroups(firstId);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }            

            if(groupList.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            
            return Request.CreateResponse(HttpStatusCode.OK, groupList);
        }

        [Authorize]
        public HttpResponseMessage Get(int id)
        {
            var groupDTO = new GroupDTO();

            try
            {
                groupDTO = groupService.GetGroupData(id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }                

            if (groupDTO == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, groupDTO);
        }

        [Authorize]
        public async Task<HttpResponseMessage> Post([FromBody]GroupDTO groupDTO)
        {
            var userName = RequestContext.Principal.Identity.Name;

            try
            {
                await groupService.CreateGroup(userName, groupDTO);
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
            var userName = RequestContext.Principal.Identity.Name;

            try
            {
                await groupService.Delete(userName, id);
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

    }
}
