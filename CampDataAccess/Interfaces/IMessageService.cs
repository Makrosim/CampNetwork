using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;

namespace CampBusinessLogic.Interfaces
{
    public interface IMessageService
    {
        Task<OperationDetails> CreateUsersMessage(string name, MessageDTO messageDTO);
        Task<OperationDetails> DeleteUsersMessage(int messageId, int postId);
        List<MessageDTO> GetAllPostMessages(int postId);
    }
}
