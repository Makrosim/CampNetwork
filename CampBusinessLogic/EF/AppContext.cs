using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using CampDataAccess.Entities;

namespace CampDataAccess.EF
{
    public class AppContext : IdentityDbContext<User>
    {
        public AppContext(string conectionString) : base(conectionString) { }

        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupSetting> GroupSettings { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<CampPlace> CampPlaces { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}