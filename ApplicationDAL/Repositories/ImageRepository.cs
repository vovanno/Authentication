using System;
using ApplicationDAL.Entities;
using ApplicationDAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ApplicationDAL.Exceptions;

namespace ApplicationDAL.Repositories
{
    /// <inheritdoc />
    /// <summary>
    /// Implements IImageRepository for interaction with images.
    /// </summary>
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

        public IEnumerable<Image> GetAllImages(int page)
        {
            var pageNumber=1;
            const int pageSize = 9;
            var totalItems = _context.Images.AsEnumerable().ToList().Count;
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            if (page > 0 && page < totalPages + 1)
            {
                pageNumber = page;
            }
            else
            {
                throw new WrongPageException("Wrong page number, it can not be less then 0 and more than "+totalPages,totalPages);
            }
                
            return _context.Images.AsEnumerable().ToList().Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Image> SearchImages(string caption)
        {
            return _context.Images.AsEnumerable().Where(p => p.Caption == caption);
        }

        public int GetPages()
        {
            const int pageSize = 9;
            var totalItems = _context.Images.AsEnumerable().ToList().Count;
            return (int)Math.Ceiling((decimal)totalItems / pageSize);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
