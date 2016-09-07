using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampDataAccess.Entities;
using CampBusinessLogic.Interfaces;
using CampDataAccess.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CampBusinessLogic.Services
{
    public class CampPlaceService : ICampPlaceService
    {
        private IUnitOfWork Database { get; set; }
        private List<string> points = new List<string>();

        public CampPlaceService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task Create(string name, CampPlaceDTO campPlaceDTO)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);

            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);

            var camp = Mapper.Map<CampPlaceDTO, CampPlace>(campPlaceDTO);
            camp.UserProfile =  profile;

            Database.CampPlaceManager.Create(camp);
            await Database.SaveAsync();
        }

        public async Task<List<CampPlaceDTO>> GetCampList(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);

            var user = await Database.UserManager.FindByNameAsync(name);
            var campPlaceDTOList = new List<CampPlaceDTO>();

            points.Clear();

            foreach (var cp in user.UserProfile.CampPlaces)
            {
                var campPlace = Database.CampPlaceManager.Get(cp.Id);
                points.Add(campPlace.LocationX + " " + campPlace.LocationY + " " + campPlace.Name);
                var campDTO = Mapper.Map<CampPlace, CampPlaceDTO>(campPlace);

                campPlaceDTOList.Add(campDTO);
            }

            return campPlaceDTOList;
        }

        public List<string> GetPointsList()
        {
            return points;
        }

        public CampPlaceDTO GetCampData(int campPlaceId)
        {
            var campPlace = Database.CampPlaceManager.Get(campPlaceId);

            var campDTO = Mapper.Map<CampPlace, CampPlaceDTO>(campPlace);

            return campDTO;
        }

        public List<CampPlaceDTO> Search(string soughtName)
        {
            var campPlaceList = Database.CampPlaceManager.List(cp =>cp.Name.Contains(soughtName)).ToArray();
            var campDTOList = new List<CampPlaceDTO>();

            foreach (var cp in campPlaceList)
            {
                var campDTO = Mapper.Map<CampPlace, CampPlaceDTO>(cp);
                campDTOList.Add(campDTO);
            }
            
            return campDTOList;
        }

        public async Task Update(CampPlaceDTO campPlaceDTO)
        {
            var campPlace = Database.CampPlaceManager.Get(campPlaceDTO.Id);

            Mapper.Map(campPlaceDTO, campPlace, typeof(CampPlaceDTO), typeof(CampPlace));

            Database.CampPlaceManager.Update(campPlace);
            await Database.SaveAsync();
        }

        public async Task Delete(string userName, int campPlaceId)
        {
            var user = Database.CampPlaceManager.Get(campPlaceId).UserProfile.User;

            if(user.UserName != userName)
                throw new UnauthorizedAccessException("У вас нет полномочий совершать это действие");

            Database.CampPlaceManager.Delete(campPlaceId);

            await Database.SaveAsync();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}