using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
using CampDataAccess.Entities;
using CampBusinessLogic.Interfaces;
using CampDataAccess.Interfaces;
using AutoMapper;
using System.IO;
using System.Collections.Generic;

namespace CampBusinessLogic.Services
{
    public class ProfileService : IProfileService
    {
        IUnitOfWork Database { get; set; }

        public ProfileService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<ProfileDTO> GetProfileData(string name)
        {
            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);
                 
            Mapper.Initialize(cfg => { cfg.CreateMap<UserProfile, ProfileDTO>().ForMember("Avatar", c => c.Ignore()); });
            var profDTO = Mapper.Map<UserProfile, ProfileDTO>(profile);

            if (profile.Avatar != -1)
                profDTO.Avatar = Database.MediaManager.Get(profile.Avatar).Bytes;

            return profDTO;
        }

        public async Task<OperationDetails> SetProfileData(string email, ProfileDTO profDTO)
        {
            var user = await Database.UserManager.FindByNameAsync(email);

            var profile = Database.UserProfileManager.Get(user.Id);
            var profileId = profile.Id;

            Mapper.Initialize(cfg => { cfg.CreateMap<ProfileDTO, UserProfile>().ForMember("Avatar", c => c.Ignore()); });
            Mapper.Map(profDTO, profile, typeof(ProfileDTO), typeof(UserProfile));

            profile.Id = profileId;

            if (profDTO.Avatar != null)
            {
                var media = new Media { Type = "Image", Bytes = profDTO.Avatar };
                Database.MediaManager.Create(media);
                await Database.SaveAsync();

                profile.Media.Add(media.Id);
                profile.Avatar = media.Id;
            }

            Database.UserProfileManager.Update(profile);
            
            return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
