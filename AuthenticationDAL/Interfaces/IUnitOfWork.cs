using System;
using System.Threading.Tasks;

namespace AuthenticationDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRoleManager RoleManager { get; }
        IUserManager UserManager { get; }
        Task SaveAsync();
    }
}
