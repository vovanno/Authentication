using AuthenticationDAL.Interfaces;
using AuthenticationDAL.Managers;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AuthenticationDAL.Repositories
{
    /// <inheritdoc />
    /// <summary>
    /// Unit of work pattern for interaction with users and roles.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private IRoleManager _roleManager;
        private IUserManager _userManager;


        public UnitOfWork(IIdentiyContext context)
        {
            Context = context;
        }

        public IIdentiyContext Context { get; }
        public IRoleManager RoleManager => _roleManager ?? (_roleManager = new AppRoleManager(new RoleStore<IdentityRole>((DbContext)Context)));
        public IUserManager UserManager => _userManager ?? (_userManager = new AppUserManager(new UserStore<IdentityUser>((DbContext)Context)));

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
