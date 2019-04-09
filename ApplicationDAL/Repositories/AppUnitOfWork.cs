using System.Threading.Tasks;
using ApplicationDAL.Interfaces;

namespace ApplicationDAL.Repositories
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly IAppContext _appContext;
        private IImageRepository _imageManager;
        private IProfileRepository _clientManager;

        public AppUnitOfWork(IAppContext context)
        {
            _appContext = context;
        }

        public IProfileRepository ClientManager => _clientManager ?? (_clientManager = new ProfileRepository(_appContext));
        public IImageRepository ImageManager => _imageManager ?? (_imageManager = new ImageRepository(_appContext));

        public async Task SaveAsync()
        {
            await _appContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }


    }
}
