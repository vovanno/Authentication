using System.Threading.Tasks;
using AuthenticationDAL.Context;
using AuthenticationDAL.Entities;
using AuthenticationDAL.Identity;
using AuthenticationDAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthenticationDAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private readonly AppContext _db;
        private IClientManager _clientManager;
        private AppRoleManager _roleManager;
        private AppUserManager _userManager;

        public IdentityUnitOfWork(string connection)
        {
            _db = new AppContext(connection);
        }

        public IClientManager ClientManager => _clientManager ?? (_clientManager = new ClientManager(_db));
        public AppRoleManager RoleManager => _roleManager ?? (_roleManager = new AppRoleManager(new RoleStore<AppRole>(_db)));
        public AppUserManager UserManager => _userManager ?? (_userManager = new AppUserManager(new UserStore<AppUser>(_db)));

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
