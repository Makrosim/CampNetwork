using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;

namespace CampBusinessLogic.Interfaces
{
    public interface IUserProfileService : IDisposable
    {
        Task<ProfileDTO> GetProfileData(string id);
        Task<OperationDetails> SetProfileData(string email, ProfileDTO us);
        Task<byte[]> GetAvatar(string name);
    }
}
