using ApplicationDAL.Entities;
using ApplicationDAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationDAL.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IAppContext _context;

        public ImageRepository(IAppContext context)
        {
            _context = context;
        }

        public async Task<bool> UploadImage(Image image)
        {
            var result = _context.Images.Add(image);
            if (result == null)
            {
                return false;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Image> GetUserImages(string id)
        {
            return _context.Images.AsEnumerable().Where(p => p.UserId == id);
        }

        public async Task<bool> DeleteImage(string imageName)
        {
            var test = _context.Images.FirstOrDefault(x => x.ImageName == imageName);
            if (test == null)
                return false;
            _context.Entry(test).State = EntityState.Deleted;
            var task = _context.Images.Remove(test);
            await _context.SaveChangesAsync();
            return task != null;
        }

        public IEnumerable<Image> GetAllImages()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
