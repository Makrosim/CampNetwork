using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;

namespace CampBusinessLogic.Interfaces
{
    public interface ICampPlaceService
    {
        Task<OperationDetails> Create(CampPlaceDTO Dto);
        CampPlaceDTO GetCampData(int id);
    }
}
