﻿using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
using CampDataAccess.Entities;
using CampBusinessLogic.Interfaces;
using CampDataAccess.Interfaces;
using AutoMapper;
using System.Collections.Generic;

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

        public async Task<OperationDetails> Create(string email, CampPlaceDTO campDTO)
        {
            var user = await Database.UserManager.FindByEmailAsync(email);
            var profile = Database.UserProfileManager.Get(user.Id);

            Mapper.Initialize(cfg => { cfg.CreateMap<CampPlaceDTO, CampPlace>(); });
            var camp = Mapper.Map<CampPlaceDTO, CampPlace>(campDTO);
            camp.UserProfile =  profile;

            Database.CampPlaceManager.Create(camp);
            await Database.SaveAsync();

            return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public async Task<OperationDetails> Update(CampPlaceDTO campDTO)
        {
            var campPlace = Database.CampPlaceManager.Get(campDTO.Id);

            Mapper.Initialize(cfg => { cfg.CreateMap<CampPlaceDTO, CampPlace>(); });
            Mapper.Map(campDTO, campPlace, typeof(CampPlaceDTO), typeof(CampPlace));

            Database.CampPlaceManager.Update(campPlace);
            await Database.SaveAsync();

            return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public OperationDetails Delete(int Id)
        {
            Database.CampPlaceManager.Delete(Id);

            return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public CampPlaceDTO GetCampData(int id)
        {
            Mapper.Initialize(cfg => { cfg.CreateMap<CampPlace, CampPlaceDTO>(); });
            var campDTO = Mapper.Map<CampPlace, CampPlaceDTO>(Database.CampPlaceManager.Get(id));
            
            return campDTO;
        }

        public async Task<List<CampPlaceDTO>> GetCampList(string email)
        {
            var user = await Database.UserManager.FindByEmailAsync(email);
            var profile = Database.UserProfileManager.Get(user.Id);

            var campPlaecDTOList = new List<CampPlaceDTO>();

            Mapper.Initialize(cfg => { cfg.CreateMap<CampPlace, CampPlaceDTO>(); });

            foreach (var cp in profile.CampPlaces)
            {
                var campPlace = Database.CampPlaceManager.Get(cp.Id);
                points.Add(campPlace.LocationX + " " + campPlace.LocationY + " " + campPlace.Name);
                var campDTO = Mapper.Map<CampPlace, CampPlaceDTO>(campPlace);

                campDTO.PostsCount = campPlace.Posts?.Count ?? 0;
                campPlaecDTOList.Add(campDTO);
            }

            return campPlaecDTOList;
        }

        public List<string> GetPointsList()
        {
            return points;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
