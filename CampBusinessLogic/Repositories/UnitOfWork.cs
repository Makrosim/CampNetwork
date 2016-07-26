using CampDataAccess.EF;
using CampDataAccess.Entities;
using CampDataAccess.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using CampDataAccess.Identity;

namespace CampDataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppContext db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IRepository<Post> postManager;
        private IRepository<Message> messageManager;
        private IRepository<Group> groupManager;
        private IRepository<CampPlace> campPlaceManager;
        private IRepository<UserProfile> userProfileManager;
        private IRepository<Media> mediaManager;

        public UnitOfWork(string connectionString)
        {
            db = new AppContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<User>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            postManager = new PostsRepository(db);
            messageManager = new MessagesRepository(db);
            groupManager = new GroupsRepository(db);
            userProfileManager = new UserProfilesRepository(db);
            campPlaceManager = new CampPlacesRepository(db);
            mediaManager = new MediasRepository(db);
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

        public IRepository<UserProfile> UserProfileManager
        {
            get { return userProfileManager; }
        }

        public IRepository<Media> MediaManager
        {
            get { return mediaManager; }
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
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}