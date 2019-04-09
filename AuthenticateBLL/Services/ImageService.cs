using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationDAL.Entities;
using ApplicationDAL.Interfaces;
using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using AutoMapper;

namespace AuthenticateBLL.Services
{
    public class ImageService : IImageService
    {
        private readonly IAppUnitOfWork _appUnit;
        private readonly IMapper _mapper;

        public ImageService(IAppUnitOfWork appUnit)
        {
            _appUnit = appUnit;
            _mapper = new MapperConfiguration(c => c.CreateMap<ImageDTO, Image>().ReverseMap()).CreateMapper();
        }
        public Task<bool> UploadImage(ImageDTO image)
        {
            return _appUnit.ImageManager.UploadImage(_mapper.Map<Image>(image));
        }

        public IEnumerable<ImageDTO> GetUserImages(string id)
        {
            var result = _appUnit.ImageManager.GetUserImages(id);
            return _mapper.Map<IEnumerable<ImageDTO>>(result);
        }

        public async Task<bool> DeleteImage(string imageName)
        {
            return await _appUnit.ImageManager.DeleteImage(imageName);
        }
    }
}
