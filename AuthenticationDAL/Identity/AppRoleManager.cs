using AuthenticationDAL.Entities;
using AuthenticationDAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace AuthenticationDAL.Identity
{
    public class AppRoleManager : RoleManager<AppRole>, IRoleManager
    {
        public AppRoleManager(IRoleStore<AppRole, string> store) : base(store)
        {

        }
    }
}
