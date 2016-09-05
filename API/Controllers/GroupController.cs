using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class GroupController : ApiController
    {
        private IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [Authorize]
        public async Task<HttpResponseMessage> Get(string userName)
        {
            var result = await groupService.GetAllGroups(userName);

            if(result != null)
            {
                return Request.CreateResponse<List<GroupDTO>>(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        public async Task<HttpResponseMessage> Get(string userName, int groupId)
        {
            var result = await groupService.GetGroupData(userName, groupId);

            if (result != null)
            {
                return Request.CreateResponse<GroupDTO>(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
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

        [Authorize]
        [HttpGet]
        [Route("api/Group/AddPost")]
        public async Task<HttpResponseMessage> AddPostToGroup(int groupId, int postId)
        {
            try
            {
                await groupService.AddPostToGroup(groupId, postId);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
