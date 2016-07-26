using CampBusinessLogic.Interfaces;
using CampDataAccess.Repositories;

namespace CampBusinessLogic.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new UnitOfWork(connection));
        }
    }
}
