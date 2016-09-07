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

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        [Authorize]
        public async Task<HttpResponseMessage> Users(string firstId)
        {
            var postList = new List<PostDTO>();

            try
            {
                postList = await postService.GetAllUsersPosts(firstId);
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
        public HttpResponseMessage Groups(int firstId)
        {
            var postList = new List<PostDTO>();

            try
            {
                postList = postService.GetAllGroupPosts(firstId);
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
        public async Task<HttpResponseMessage> Groups(int firstId, int secondId)
        {
            try
            {
                await postService.AttachPostToGroup(firstId, secondId);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage CampPlaces(int firstId)
        {
            var postList = new List<PostDTO>();

            try
            {
                postList = postService.GetAllCampPlacePosts(firstId);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            if (postList.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, postList);
        }

        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> Post([FromBody]PostDTO postDTO)
        {
            try
            {
                await postService.CreatePost(postDTO);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.Created); 
        }

        [Authorize]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var userName = RequestContext.Principal.Identity.Name;

            try
            {
                await postService.DeletePost(userName, id);
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
