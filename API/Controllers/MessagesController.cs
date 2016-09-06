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
    public class MessagesController : ApiController
    {
        private IMessageService messageService;

        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage Posts(int id)
        {
            var result = new List<MessageDTO>();

            try
            {
                result = messageService.GetAllPostMessages(id);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            if (result.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Authorize]
        public async Task<HttpResponseMessage> Post([FromBody]MessageDTO messageDTO)
        {
            var userName = RequestContext.Principal.Identity.Name;

            var result = new MessageDTO();

            try
            {
               result = await messageService.CreateUsersMessage(userName, messageDTO);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        [Authorize]
        public async Task<HttpResponseMessage> Delete([FromUri]int messageId)
        {
            var userName = RequestContext.Principal.Identity.Name;

            try
            {
                await messageService.DeleteUsersMessage(userName, messageId);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
