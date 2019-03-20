using AuthenticationDAL.Entities;
using Microsoft.AspNet.Identity;

namespace AuthenticationDAL.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {

        }
    }
}
