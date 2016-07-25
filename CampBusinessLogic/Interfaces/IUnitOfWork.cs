using System;
using System.Threading.Tasks;
using CampDataAccess.Identity;
using CampDataAccess.Entities;

namespace CampDataAccess.Interfaces
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