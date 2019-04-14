using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationDAL.Entities;

namespace ApplicationDAL.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// Provides access for interaction with images.
    /// </summary>
    public interface IImageRepository : IDisposable
    {
        Task<bool> UploadImage(Image image);
        IEnumerable<Image> GetUserImages(string id);
        Task<bool> DeleteImage(string imageName);
        IEnumerable<Image> GetAllImages(int page);
        IEnumerable<Image> SearchImages(string caption);
        int GetPages();
    }
}
