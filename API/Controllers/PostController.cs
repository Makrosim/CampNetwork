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
        public async Task<HttpResponseMessage> Get([FromUri]string userName) // Get all post
        {
            var result = await postService.GetAllUsersPosts(userName);

            return Request.CreateResponse<List<PostDTO>>(HttpStatusCode.OK, result);
        }

        [Authorize]
        public async Task<HttpResponseMessage> Post([FromUri]int campPlaceId, [FromBody]string postText)
        {
            var result = await postService.CreatePost(campPlaceId, postText);

            if (result.Succedeed)
                return Request.CreateResponse(HttpStatusCode.Created);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result.Message);
        }

        [Authorize]
        public async Task<HttpResponseMessage> Delete([FromUri]int postId) // Get all post
        {
            var result = await postService.DeletePost(postId);

            if(result.Succedeed)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result.Message);
            }
            
        }

    }
}
