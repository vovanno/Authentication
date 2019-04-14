using System;

namespace AuthDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //IRoleManager RoleManager { get; }
        IUserManager UserManager { get; }
        //Task SaveAsync();
    }
}
