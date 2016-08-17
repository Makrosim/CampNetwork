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
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);
            
            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);

            Mapper.Initialize(cfg => { cfg.CreateMap<UserProfile, ProfileDTO>(); });
            var profDTO = Mapper.Map<UserProfile, ProfileDTO>(profile);

            if (profile.Avatar != -1)
                profDTO.AvatarId = Database.MediaManager.Get(profile.Avatar).Id; //Правильно ли?

            return profDTO;

        }

        public async Task<OperationDetails> SetProfileData(string name, ProfileDTO profDTO)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);
            
            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);

            Mapper.Initialize(cfg => { cfg.CreateMap<ProfileDTO, UserProfile>().ForMember("Id", c => c.Ignore()); });
            Mapper.Map(profDTO, profile, typeof(ProfileDTO), typeof(UserProfile));

            Database.UserProfileManager.Update(profile);
            await Database.SaveAsync();

            return new OperationDetails(true, "Операция успешно завершена", "");

        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
