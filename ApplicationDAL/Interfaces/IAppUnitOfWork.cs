using System;
using System.Threading.Tasks;

namespace ApplicationDAL.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// Unit of work for application logic.
    /// </summary>
    public interface IAppUnitOfWork : IDisposable
    {
        IProfileRepository ClientManager { get; }
        IImageRepository ImageManager { get; }
        Task SaveAsync();
    }
}
