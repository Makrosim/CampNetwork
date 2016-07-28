using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
using System.Collections.Generic;

namespace CampBusinessLogic.Interfaces
{
    public interface ICampPlaceService
    {
        Task<OperationDetails> Create(string email, CampPlaceDTO Dto);
        OperationDetails Delete(int campPlaceId);
        CampPlaceDTO GetCampData(int campPlaceId);
        Task<List<CampPlaceDTO>> GetCampList(string name);
        List<string> GetPointsList();
        Task<OperationDetails> Update(CampPlaceDTO campPlaceDTO);
    }
}
