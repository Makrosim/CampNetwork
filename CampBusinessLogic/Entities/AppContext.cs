using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CampBusinessLogic.Entities
{
    public class AppContext : IdentityDbContext<User>
    {
        public AppContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupSetting> GroupSettings { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<CampPlace> CampPlaces { get; set; }
        public DbSet<Message> Messages { get; set; }

        public static AppContext Create()
        {
            return new AppContext();
        }
    }
}