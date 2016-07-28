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

        public async Task<byte[]> GetAvatar(string email)
        {
            var user = await Database.UserManager.FindByEmailAsync(email);
            var profile = Database.UserProfileManager.Get(user.Id);

            if (profile.Avatar != -1)
                return Database.MediaManager.Get(profile.Avatar).Bytes;
            else
                return null;
        }

        public async Task<ProfileDTO> GetProfileData(string name)
        {
            var user = await Database.UserManager.FindByEmailAsync(name);
            var prof = Database.UserProfileManager.Get(user.Id);
            Mapper.Initialize(cfg => { cfg.CreateMap<UserProfile, ProfileDTO>().ForMember("Avatar", c => c.Ignore()); });

            return Mapper.Map<UserProfile, ProfileDTO>(prof);
        }

        public async Task<OperationDetails> SetProfileData(string email, ProfileDTO profDTO)
        {
            var user = await Database.UserManager.FindByEmailAsync(email);

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
