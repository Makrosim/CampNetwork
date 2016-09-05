using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using System.Collections.Generic;

namespace CampBusinessLogic.Interfaces
{
    public interface IProfileService : IDisposable
    {
        List<ProfileDTO> SearchByName(string searchCriteria);
        Task<ProfileDTO> GetProfileData(string name);
        Task SetProfileData(string name, ProfileDTO profileDTO);
    }
}
