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
            var result = new List<MessageDTO>();

            try
            {
                result = messageService.GetAllPostMessages(postId);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            if (result.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Authorize]
        public HttpResponseMessage Post([FromUri]string userName, [FromBody]MessageDTO messageDTO)
        {
            try
            {
               messageService.CreateUsersMessage(userName, messageDTO);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        [Authorize]
        public HttpResponseMessage Delete([FromUri]int messageId, [FromUri]int postId)
        {
            try
            {
                messageService.DeleteUsersMessage(messageId, postId);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
