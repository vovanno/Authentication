using AuthenticationDAL.Entities;
using AuthenticationDAL.Identity;
using AuthenticationDAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AuthenticationDAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private readonly IApplicationContext _db;
        private IClientManager _clientManager;
        private IRoleManager _roleManager;
        private IUserManager _userManager;

        public IdentityUnitOfWork(IApplicationContext context)
        {
            _db = context;
        }

        public IClientManager ClientManager => _clientManager ?? (_clientManager = new ClientManager(_db));
        public IRoleManager RoleManager => _roleManager ?? (_roleManager = new AppRoleManager(new RoleStore<AppRole>((DbContext)_db)));
        public IUserManager UserManager => _userManager ?? (_userManager = new AppUserManager(new UserStore<AppUser>((DbContext)_db)));

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
