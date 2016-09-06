using CampBusinessLogic.DTO;
using CampDataAccess.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using CampBusinessLogic.Interfaces;
using CampDataAccess.Interfaces;
using System;
using System.Diagnostics;

namespace CampBusinessLogic.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task Create(RegisterDTO userDTO)
        {
            var user = await Database.UserManager.FindByNameAsync(userDTO.UserName);

            if (user == null)
            {
                user = new User { Email = userDTO.Email, UserName = userDTO.UserName };

                var result = await Database.UserManager.CreateAsync(user, userDTO.Password);

                if (result.Succeeded)
                {
                    var profile = new UserProfile
                    {
                        Id = user.Id,
                        FirstName = "Аноним",
                        LastName = "Анонимович",
                        BirthDateDay = "Не установлено",
                        BirthDateMounth = "Не установлено",
                        BirthDateYear = "Не установлено",
                        Address = "Не установлено",
                        Phone = "Не установлено",
                        Skype = "Не установлено",
                        AdditionalInformation = "Не установлено",
                    };

                    Database.UserProfileManager.Create(profile);
                    await Database.SaveAsync();
                }
                else
                    throw new Exception("Ошибка создания пользователя");
            }
            else
            {
                throw new Exception("Пользователь с таким именем уже существует");
            }          
        }

        public async Task<ClaimsIdentity> Authenticate(RegisterDTO userDTO)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            var user = await Database.UserManager.FindAsync(userDTO.UserName, userDTO.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task Delete(string name)
        {
            var user = await Database.UserManager.FindByNameAsync(name);
            await Database.UserManager.DeleteAsync(user);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
