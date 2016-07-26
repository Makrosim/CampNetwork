using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
using CampDataAccess.Entities;
using CampBusinessLogic.Interfaces;
using CampDataAccess.Interfaces;
using AutoMapper;

namespace CampBusinessLogic.Services
{
    class CampPlaceService : ICampPlaceService
    {
        IUnitOfWork Database { get; set; }

        public CampPlaceService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(CampPlaceDTO campDTO)
        {
                var camp = Mapper.Map<CampPlaceDTO, CampPlace>(campDTO);
                Database.CampPlaceManager.Create(camp);
                await Database.SaveAsync();

                return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public CampPlaceDTO GetCampData(int id)
        {
            var prof = Database.CampPlaceManager.Get(id);
            var campDTO = Mapper.Map<CampPlace, CampPlaceDTO>(Database.CampPlaceManager.Get(id));

            return campDTO;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
