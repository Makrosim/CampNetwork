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
        void CreateUsersMessage(string name, MessageDTO messageDTO);
        List<MessageDTO> GetAllPostMessages(int postId);
        void DeleteUsersMessage(int messageId, int postId);
    }
}
