using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;

namespace CampBusinessLogic.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDTO> CreateUsersMessage(string userName, MessageDTO messageDTO);
        List<MessageDTO> GetAllPostMessages(int postId);
        Task DeleteUsersMessage(string userName, int postId, int messageId);
    }
}
