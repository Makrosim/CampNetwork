using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CampBusinessLogic.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            CampPlaces = new List<CampPlace>();
            Groups = new List<Group>();
            Dialogs = new List<Dialog>();
            Friends = new List<User>();
            Media = new List<int>();
        }

        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<int> Media { get; set; }
        public virtual ICollection<CampPlace> CampPlaces { get; set; }
        public ICollection<Group> Groups { get; set; }
        public UserSetting UserSettings { get; set; }
        public ICollection<User> Friends { get; set; }
        public ICollection<Dialog> Dialogs { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}