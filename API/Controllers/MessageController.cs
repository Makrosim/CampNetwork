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
    public class MessageController : ApiController
    {
        private IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [Authorize]
        public HttpResponseMessage Get(int postId)
        {
            var result = messageService.GetAllPostMessages(postId);

            if (result != null)
                return Request.CreateResponse<List<MessageDTO>>(HttpStatusCode.OK, result);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [Authorize]
        public async Task<HttpResponseMessage> Post([FromUri]string userName, [FromBody]MessageDTO messageDTO)
        {
            var result = await messageService.CreateUsersMessage(userName, messageDTO);

            if (result.Succedeed)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result.Message);
        }

        [Authorize]
        public async Task<HttpResponseMessage> Delete([FromUri]int messageId, [FromUri]int postId)
        {
            var result = await messageService.DeleteUsersMessage(messageId, postId);

            if (result.Succedeed)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result.Message);
        }
    }
}
