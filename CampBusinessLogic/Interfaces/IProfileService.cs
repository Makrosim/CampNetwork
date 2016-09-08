using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using System.Collections.Generic;

namespace CampBusinessLogic.Interfaces
{
    public interface IProfileService : IDisposable
    {
        List<ProfileDTO> Search(string soughtName);
        Task<ProfileDTO> GetProfileData(string userName);
        Task SetProfileData(string userName, ProfileDTO profileDTO);
    }
}
