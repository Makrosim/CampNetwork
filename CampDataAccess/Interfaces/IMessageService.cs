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
        Task<OperationDetails> CreateUsersMessage(MessageDTO DTO);
        Task<OperationDetails> DeleteUsersMessage(int Id);
    }
}
