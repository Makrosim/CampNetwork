using System;
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
        IMediaService mediaService { get; set; }

        public ProfileService(IUnitOfWork uow, IMediaService mediaService)
        {
            Database = uow;
            this.mediaService = mediaService;
        }

        public List<ProfileDTO> Search(string soughtName)
        {
            if (string.IsNullOrEmpty(soughtName))
                throw new ArgumentNullException(soughtName);

            var profileList = Database.UserProfileManager.List(p => p.FirstName.Contains(soughtName) || p.LastName.Contains(soughtName)).ToArray();
            var profileDTOList = new List<ProfileDTO>();

            foreach(var profile in profileList)
            {
                var profileDTO = Mapper.Map<UserProfile, ProfileDTO>(profile);
                profileDTOList.Add(profileDTO);
            }

            return profileDTOList;
        }

        public async Task<ProfileDTO> GetProfileData(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);
            
            var user = await Database.UserManager.FindByNameAsync(name);

            if(user == null)
                throw new Exception("Запрашиваемый ресурс не найден");

            var profile = Database.UserProfileManager.Get(user.Id);

            var profDTO = Mapper.Map<UserProfile, ProfileDTO>(profile);

            profDTO.AvatarBase64 = mediaService.GetMediaBase64(profile.Avatar?.Id ?? -1);

            return profDTO;
        }

        public async Task SetProfileData(string name, ProfileDTO profDTO)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);
            
            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);

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
