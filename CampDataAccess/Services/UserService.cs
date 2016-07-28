using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
using CampDataAccess.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using CampBusinessLogic.Interfaces;
using CampDataAccess.Interfaces;

namespace CampBusinessLogic.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            var user = await Database.UserManager.FindByEmailAsync(userDto.Email);

            if (user == null)
            {
                user = new User { Email = userDto.Email, UserName = userDto.UserName };
                
                await Database.UserManager.CreateAsync(user, userDto.Password);
                await Database.SaveAsync();

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

                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            var user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
