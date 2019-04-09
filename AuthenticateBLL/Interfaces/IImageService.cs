using AuthenticateBLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticateBLL.Interfaces
{
    public interface IImageService
    {
        Task<bool> UploadImage(ImageDTO image);
        IEnumerable<ImageDTO> GetUserImages(string id);
        Task<bool> DeleteImage(string imageName);

    }
}
