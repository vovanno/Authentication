using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationDAL.Entities;

namespace ApplicationDAL.Interfaces
{
    public interface IImageRepository : IDisposable
    {
        Task<bool> UploadImage(Image image);
        IEnumerable<Image> GetUserImages(string id);
        Task<bool> DeleteImage(string imageName);
        IEnumerable<Image> GetAllImages();
    }
}
