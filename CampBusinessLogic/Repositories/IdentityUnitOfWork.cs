using CampBusinessLogic.EF;
using CampBusinessLogic.Entities;
using CampBusinessLogic.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using CampBusinessLogic.Identity;

namespace CampBusinessLogic.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private AppContext db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IRepository<Post> postManager;
        private IRepository<Message> messageManager;
        private IRepository<Group> groupManager;
        private IRepository<CampPlace> campPlaceManager;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new AppContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<User>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            postManager = (IRepository<Post>)new PostsRepository(db);
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IRepository<Post> PostManager
        {
            get { return postManager; }
        }

        public IRepository<Message> MessageManager
        {
            get { return messageManager; }
        }

        public IRepository<Group> GroupManager
        {
            get { return groupManager; }
        }

        public IRepository<CampPlace> CampPlaceManager
        {
            get { return campPlaceManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    postManager.Dispose();
                    messageManager.Dispose();
                    campPlaceManager.Dispose();
                    groupManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}