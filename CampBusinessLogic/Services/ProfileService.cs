﻿using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampDataAccess.Entities;
using CampBusinessLogic.Interfaces;
using CampDataAccess.Interfaces;
using AutoMapper;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace CampBusinessLogic.Services
{
    public class ProfileService : IProfileService
    {
        IUnitOfWork Database { get; set; }

        public ProfileService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public List<ProfileDTO> SearchByName(string searchCriteria)
        {
            if (String.IsNullOrEmpty(searchCriteria))
                throw new ArgumentNullException(searchCriteria);

            Mapper.Initialize(cfg => { cfg.CreateMap<UserProfile, ProfileDTO>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.User.UserName)); });

            var profileList = Database.UserProfileManager.GetAll().ToArray();
            var profileDTOList = new List<ProfileDTO>();

            foreach(var profile in profileList)
            {
                var fullName = profile.FirstName + profile.LastName;

                if(fullName.Contains(searchCriteria))
                {
                    var profileDTO = Mapper.Map<UserProfile, ProfileDTO>(profile);
                    profileDTOList.Add(profileDTO);
                }
            }

            return profileDTOList;
        }

        public async Task<ProfileDTO> GetProfileData(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);
            
            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = await Database.UserProfileManager.GetAsync(user.Id);

            Mapper.Initialize(cfg => { cfg.CreateMap<UserProfile, ProfileDTO>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.User.UserName)); });

            var profDTO = Mapper.Map<UserProfile, ProfileDTO>(profile);

            if (profile.AvatarId != -1)
                profDTO.AvatarId = Database.MediaManager.Get(profile.AvatarId).Id; //Правильно ли?

            return profDTO;
        }

        public async void SetProfileData(string name, ProfileDTO profDTO)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);
            
            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);

            Mapper.Initialize(cfg => { cfg.CreateMap<ProfileDTO, UserProfile>().ForMember("Id", c => c.Ignore()); });
            Mapper.Map(profDTO, profile, typeof(ProfileDTO), typeof(UserProfile));

            Database.UserProfileManager.Update(profile);
            await Database.SaveAsync();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
