using AuthenticationDAL.Identity;
using System;
using System.Threading.Tasks;

namespace AuthenticationDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IClientManager ClientManager { get; }
        AppRoleManager RoleManager { get; }
        AppUserManager UserManager { get; }
        Task SaveAsync();
    }
}
