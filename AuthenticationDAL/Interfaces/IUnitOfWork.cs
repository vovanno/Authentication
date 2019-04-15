using System;
using System.Threading.Tasks;

namespace AuthenticationDAL.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// Provides access users and roles.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRoleManager RoleManager { get; }
        IUserManager UserManager { get; }
        IIdentiyContext Context { get; }
        Task SaveAsync();
    }
}
