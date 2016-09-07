using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using System.Collections.Generic;

namespace CampBusinessLogic.Interfaces
{
    public interface ICampPlaceService
    {
        Task Create(string name, CampPlaceDTO campPlaceDTO);
        Task<List<CampPlaceDTO>> GetCampList(string userName);
        List<string> GetPointsList();
        CampPlaceDTO GetCampData(int campPlaceId);
        List<CampPlaceDTO> Search(string soughtName);
        Task Update(CampPlaceDTO campPlaceDTO);
        Task Delete(string userName, int campPlaceId);
    }
}
