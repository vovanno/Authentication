using AuthenticationDAL.Identity;
using System;
using System.Threading.Tasks;

namespace AuthenticationDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IClientManager ClientManager { get; }
        IRoleManager RoleManager { get; }
        IUserManager UserManager { get; }
        Task SaveAsync();
    }
}
