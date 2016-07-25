using CampBusinessLogic.Entities;
using Microsoft.AspNet.Identity;

namespace CampBusinessLogic.Identity
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store) : base(store)
        {
        }
    }
}
