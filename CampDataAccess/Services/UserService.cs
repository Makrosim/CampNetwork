using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
using CampDataAccess.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using CampBusinessLogic.Interfaces;
using CampDataAccess.Interfaces;
using System;

namespace CampBusinessLogic.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDTO)
        {
            var user = await Database.UserManager.FindByNameAsync(userDTO.Email);

            if (user == null)
            {
                user = new User { Email = userDTO.Email, UserName = userDTO.UserName };
                
                var result =  await Database.UserManager.CreateAsync(user, userDTO.Password);

                await Database.SaveAsync();
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
                        Avatar = -1
                    };

                    Database.UserProfileManager.Create(profile);
                    await Database.SaveAsync();
                }
                else
                    return new OperationDetails(false, "Ошибка создания пользователя", "");

                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDTO)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            var user = await Database.UserManager.FindAsync(userDTO.UserName, userDTO.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task<OperationDetails> Delete(string name)
        {
            try
            {
                var user = await Database.UserManager.FindByNameAsync(name);
                await Database.UserManager.DeleteAsync(user);
            }
            catch(Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }

            return new OperationDetails(true, "User successfully deleted", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
