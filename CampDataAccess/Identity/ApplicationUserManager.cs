using CampDataAccess.Entities;
using Microsoft.AspNet.Identity;

namespace CampDataAccess.Identity
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store) : base(store)
        {
        }
    }
}
