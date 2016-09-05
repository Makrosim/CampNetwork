using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using System.Collections.Generic;

namespace CampBusinessLogic.Interfaces
{
    public interface ICampPlaceService
    {
        Task Create(string name, CampPlaceDTO campPlaceDTO);
        List<CampPlaceDTO> GetCampList();
        Task<List<CampPlaceDTO>> GetCampList(string name);
        List<string> GetPointsList();
        CampPlaceDTO GetCampData(int campPlaceId);
        List<CampPlaceDTO> SearchByName(string campPlaceName);
        Task Update(CampPlaceDTO campPlaceDTO);
        void Delete(int campPlaceId);
    }
}
