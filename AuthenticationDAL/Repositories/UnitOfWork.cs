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
        private readonly IIdentiyContext _db;
        private IRoleManager _roleManager;
        private IUserManager _userManager;


        public UnitOfWork(IIdentiyContext context)
        {
            _db = context;
        }

        public IRoleManager RoleManager => _roleManager ?? (_roleManager = new AppRoleManager(new RoleStore<IdentityRole>((DbContext)_db)));
        public IUserManager UserManager => _userManager ?? (_userManager = new AppUserManager(new UserStore<IdentityUser>((DbContext)_db)));

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
