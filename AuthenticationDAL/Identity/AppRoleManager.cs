using AuthenticationDAL.Entities;
using Microsoft.AspNet.Identity;

namespace AuthenticationDAL.Identity
{
    public class AppRoleManager : RoleManager<AppRole>
    {
        public AppRoleManager(IRoleStore<AppRole, string> store) : base(store)
        {

        }
    }
}
