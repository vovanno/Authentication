using AuthenticationDAL.Context;
using AuthenticationDAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace AuthenticationDAL.Managers
{
    /// <summary>
    /// Manager for interaction with users.
    /// </summary>
    public class AppUserManager : UserManager<IdentityUser>, IUserManager
    {           
        public AppUserManager(IUserStore<IdentityUser> store): base(store)
        {

        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager(new UserStore<IdentityUser>(context.Get<IdentityContext>()));
            manager.UserValidator = new UserValidator<IdentityUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            return manager;
        }

    }
}
