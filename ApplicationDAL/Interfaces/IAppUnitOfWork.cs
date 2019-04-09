using System;
using System.Threading.Tasks;

namespace ApplicationDAL.Interfaces
{
    public interface IAppUnitOfWork : IDisposable
    {
        IProfileRepository ClientManager { get; }
        IImageRepository ImageManager { get; }
        Task SaveAsync();
    }
}
