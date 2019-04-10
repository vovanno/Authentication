using AuthenticateBLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticateBLL.Interfaces
{
    public interface IImageService
    {
        Task<bool> UploadImage(ImageDTO image);
        IEnumerable<ImageDTO> GetUserImages(string id);
        IEnumerable<ImageDTO> SearchImages(string caption);
        Task<bool> DeleteImage(string imageName);
        IEnumerable<ImageDTO> GetAllImages(int page);
        int GetPages();

    }
}
