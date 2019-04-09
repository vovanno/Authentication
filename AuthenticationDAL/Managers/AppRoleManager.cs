using AuthenticationDAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthenticationDAL.Managers
{
    public class AppRoleManager : RoleManager<IdentityRole>, IRoleManager
    {
        public AppRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {

        }
    }
}
