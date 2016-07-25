using System;
using System.Threading.Tasks;
using CampBusinessLogic.Identity;
using CampBusinessLogic.Entities;

namespace CampBusinessLogic.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IRepository<Post> PostManager { get; }
        IRepository<Message> MessageManager { get; }
        IRepository<Group> GroupManager { get; }
        IRepository<CampPlace> CampPlaceManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}