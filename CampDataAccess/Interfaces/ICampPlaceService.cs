using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
using System.Collections.Generic;

namespace CampBusinessLogic.Interfaces
{
    public interface ICampPlaceService
    {
        Task<OperationDetails> Create(string email, CampPlaceDTO Dto);
        OperationDetails Delete(int Id);
        CampPlaceDTO GetCampData(int id);
        Task<List<CampPlaceDTO>> GetCampList(string email);
        List<string> GetPointsList();
        Task<OperationDetails> Update(CampPlaceDTO Dto);
    }
}
