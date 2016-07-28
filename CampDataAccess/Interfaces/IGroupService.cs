using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;

namespace CampBusinessLogic.Interfaces
{
    public interface IGroupService : IDisposable
    {
        Task<ProfileDTO> GetGroupData(string id);
        Task<OperationDetails> SetGroupData(string email, ProfileDTO us);
        Task<byte[]> GetAvatar(string email);
    }
}
