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
    public class PostsController : ApiController
    {
        private IPostService postService;
        private IGroupService groupService;

        public PostsController(IPostService postService, IGroupService groupService)
        {
            this.postService = postService;
            this.groupService = groupService;
        }

        [HttpGet]
        [Authorize]
        public async Task<HttpResponseMessage> Users(string id)
        {
            var postList = new List<PostDTO>();

            try
            {
                postList = await postService.GetAllUsersPosts(id);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            if(postList.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, postList);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage Groups(int groupId)
        {
            var postList = new List<PostDTO>();

            try
            {
                postList = postService.GetAllGroupPosts(groupId);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            if (postList.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, postList);
        }

        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> Groups(int groupId, int postId)
        {
            try
            {
                await groupService.AddPost(groupId, postId);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage CampPlaces(int campPlaceId)
        {
            var postList = new List<PostDTO>();

            try
            {
                postList = postService.GetAllCampPlacePosts(campPlaceId);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            if (postList.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, postList);
        }

        [Authorize]
        public async Task<HttpResponseMessage> Post([FromUri]int campPlaceId, [FromBody]PostDTO postDTO)
        {
            try
            {
                await postService.CreatePost(campPlaceId, postDTO);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.Created); 
        }

        [Authorize]
        public async Task<HttpResponseMessage> Delete(int postId)
        {
            var userName = RequestContext.Principal.Identity.Name;

            try
            {
                await postService.DeletePost(userName, postId);
            }
            catch(UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, ex);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
