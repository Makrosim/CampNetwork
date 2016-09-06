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
        public async Task<HttpResponseMessage> Users(string id)
        {
            var groupList = new List<GroupDTO>();

            try
            {
                groupList = await groupService.GetAllGroups(id);
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
        public async Task<HttpResponseMessage> Get(int groupId)
        {
            var userName = RequestContext.Principal.Identity.Name;

            var groupDTO = new GroupDTO();

            try
            {
                groupDTO = await groupService.GetGroupData(userName, groupId);
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
        public async Task<HttpResponseMessage> Post(string userName, GroupDTO groupDTO)
        {
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

    }
}
