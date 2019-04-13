using System.Linq.Expressions;
using AuthenticationDAL.Context;
using AuthenticationDAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace AuthenticationDAL.Managers
{
    public class AppRoleManager : RoleManager<IdentityRole>, IRoleManager
    {
        public AppRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {

        }

        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            var appRoleManger = new AppRoleManager(new RoleStore<IdentityRole>(context.Get<IdentityContext>()));
            return appRoleManger;
        }
    }
}
