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
    public class PostController : ApiController
    {
        private IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [Authorize]
        public async Task<HttpResponseMessage> Get([FromUri]string userName) // Get all users post
        {
            var result = new List<PostDTO>();
            try
            {
                result = await postService.GetAllUsersPosts(userName);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Request.CreateResponse<List<PostDTO>>(HttpStatusCode.OK, result);
        }

        [Authorize]
        public HttpResponseMessage Get(int groupId) // Get all group post
        {
            var result = postService.GetAllGroupPosts(groupId);

            return Request.CreateResponse<List<PostDTO>>(HttpStatusCode.OK, result);
        }

        [Authorize]
        [Route("api/Post/GetCampPlacePosts")]
        public HttpResponseMessage GetCampPlacePosts(int campPlaceId) // Get all camp place post
        {
            var result = postService.GetAllCampPlacePosts(campPlaceId);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Authorize]
        public HttpResponseMessage Post([FromUri]int campPlaceId, [FromBody]string postText)
        {
            try
            {
                postService.CreatePost(campPlaceId, postText);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.Created); 
        }

        [Authorize]
        public HttpResponseMessage Delete(string userName, int postId)
        {
            try
            {
                postService.DeletePost(userName, postId);
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
