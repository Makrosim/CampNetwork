using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;

namespace CampBusinessLogic.Interfaces
{
    public interface IProfileService : IDisposable
    {
        Task<ProfileDTO> GetProfileData(string name);
        Task<OperationDetails> SetProfileData(string name, ProfileDTO profileDTO);
    }
}
