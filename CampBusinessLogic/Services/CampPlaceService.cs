﻿using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
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

        public async Task<OperationDetails> Create(string name, CampPlaceDTO campPlaceDTO)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);

            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);

            Mapper.Initialize(cfg => { cfg.CreateMap<CampPlaceDTO, CampPlace>(); });
            var camp = Mapper.Map<CampPlaceDTO, CampPlace>(campPlaceDTO);
            camp.UserProfile =  profile;

            Database.CampPlaceManager.Create(camp);
            await Database.SaveAsync();

            return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public List<CampPlaceDTO> GetCampList()
        {
            var campPlaceDTOList = new List<CampPlaceDTO>();

            Mapper.Initialize(cfg => { cfg.CreateMap<CampPlace, CampPlaceDTO>()
                .ForMember("PostsCount", c => c.Ignore())
                .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.UserProfile.User.UserName)); });

            points.Clear();

            var campPlaces = Database.CampPlaceManager.GetAll().ToArray();

            foreach (var cp in campPlaces)
            {
                points.Add(cp.LocationX + " " + cp.LocationY + " " + cp.Name);
                var campDTO = Mapper.Map<CampPlace, CampPlaceDTO>(cp);

                campDTO.PostsCount = cp.Posts?.Count ?? 0;
                campPlaceDTOList.Add(campDTO);
            }

            return campPlaceDTOList;
        }

        public async Task<List<CampPlaceDTO>> GetCampList(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);

            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);

            var campPlaceDTOList = new List<CampPlaceDTO>();

            Mapper.Initialize(cfg => { cfg.CreateMap<CampPlace, CampPlaceDTO>(); });

            points.Clear();

            foreach (var cp in profile.CampPlaces)
            {
                var campPlace = Database.CampPlaceManager.Get(cp.Id);
                points.Add(campPlace.LocationX + " " + campPlace.LocationY + " " + campPlace.Name);
                var campDTO = Mapper.Map<CampPlace, CampPlaceDTO>(campPlace);

                campDTO.PostsCount = campPlace.Posts?.Count ?? 0;
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
            Mapper.Initialize(cfg => { cfg.CreateMap<CampPlace, CampPlaceDTO>(); });
            var campDTO = Mapper.Map<CampPlace, CampPlaceDTO>(Database.CampPlaceManager.Get(campPlaceId));

            return campDTO;
        }

        public List<CampPlaceDTO> SearchByName(string campPlaceName)
        {
            Mapper.Initialize(cfg => { cfg.CreateMap<CampPlace, CampPlaceDTO>()
                .ForMember("PostsCount", c => c.Ignore())
                .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.UserProfile.User.UserName)); });

            var campPlaceList = Database.CampPlaceManager.GetAll().ToArray();
            var matchList = new List<CampPlace>();
            var campDTOList = new List<CampPlaceDTO>();

            foreach (var cp in campPlaceList)
            {
                if (cp.Name == campPlaceName)
                {
                    var campDTO = Mapper.Map<CampPlace, CampPlaceDTO>(cp);
                    campDTO.PostsCount = cp.Posts?.Count ?? 0;
                    campDTOList.Add(campDTO);
                }
            }
            
            return campDTOList;
        }

        public async Task<OperationDetails> Update(CampPlaceDTO campPlaceDTO)
        {
            var campPlace = Database.CampPlaceManager.Get(campPlaceDTO.Id);

            Mapper.Initialize(cfg => { cfg.CreateMap<CampPlaceDTO, CampPlace>(); });
            Mapper.Map(campPlaceDTO, campPlace, typeof(CampPlaceDTO), typeof(CampPlace));

            Database.CampPlaceManager.Update(campPlace);
            await Database.SaveAsync();

            return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public OperationDetails Delete(int campPlaceId)
        {
            Database.CampPlaceManager.Delete(campPlaceId);

            return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}