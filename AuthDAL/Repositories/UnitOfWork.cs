using AuthDAL.Infrastructure;
using AuthDAL.Interfaces;
using AuthDAL.Managers;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AuthDAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IIdentiyContext _db;

        //private IRoleManager _roleManager;
        private IUserManager _userManager;


        public UnitOfWork(IIdentiyContext context)
        {
            _db = context;
        }

        //public IRoleManager RoleManager => _roleManager ??
        //                                   (_roleManager =
        //                                       new AppRoleManager(new RoleStore<IdentityRole>((DbContext) _db)));

        public IUserManager UserManager => _userManager ?? (_userManager =
            new ApplicationUserManager(new UserStore<ApplicationUser>((DbContext) _db)));

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

    }
}
