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
    public class UserProfileService : IUserProfileService
    {
        IUnitOfWork Database { get; set; }

        public UserProfileService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<byte[]> GetAvatar(string email)
        {
            var user = await Database.UserManager.FindByEmailAsync(email);
            var prof = Database.UserProfileManager.Get(Convert.ToInt32(user.Id));

            return Database.MediaManager.Get(prof.Avatar).Bytes;
        }

        public async Task<OperationDetails> SetProfileData(string email, ProfileDTO profDTO)
        {
            User user = await Database.UserManager.FindByEmailAsync(email);

            if (user != null)
            {
                var profile = Mapper.Map<ProfileDTO, UserProfile>(profDTO);
                profile.User = user;

                byte[] imageData = null;

                if (profDTO.Image != null)
                {
                    using (var binaryReader = new BinaryReader(profDTO.Image))
                    {
                        imageData = binaryReader.ReadBytes((int)profDTO.Image.Length); //Конвертирование long в int траблы
                    }

                    var media = new Media
                    {
                        Type = "Image",
                        Bytes = imageData
                    };

                    Database.MediaManager.Create(media);
                    profile.Media.Add(media.Id);
                }

                Database.UserProfileManager.Create(profile);

                await Database.SaveAsync();

                await Database.UserManager.UpdateAsync(user); //Нужно ли?

                await Database.SaveAsync();

                return new OperationDetails(true, "Операция успешно завершена", "");
            }
            else
            {
                return new OperationDetails(false, "Ошибка", "");
            }
        }

        public async Task<ProfileDTO> GetProfileData(string name)
        {
            var user = await Database.UserManager.FindByEmailAsync(name);
            var prof = Database.UserProfileManager.Get(Convert.ToInt32(user.Id));
            var profDTO = Mapper.Map<UserProfile, ProfileDTO>(prof);

            return profDTO;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
