using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
using System.Collections.Generic;

namespace CampBusinessLogic.Interfaces
{
    public interface ICampPlaceService
    {
        Task<OperationDetails> Create(string name, CampPlaceDTO campPlaceDTO);
        Task<List<CampPlaceDTO>> GetCampList(string name);
        List<string> GetPointsList();
        CampPlaceDTO GetCampData(int campPlaceId);
        Task<OperationDetails> Update(CampPlaceDTO campPlaceDTO);
        OperationDetails Delete(int campPlaceId);
    }
}
