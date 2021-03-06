﻿using ApplicationDAL.Entities;
using ApplicationDAL.Exceptions;
using ApplicationDAL.Interfaces;
using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticateBLL.Services
{
    /// <summary>
    /// Service for interaction with images through image repository.
    /// </summary>
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

        public IEnumerable<ImageDTO> SearchImages(string caption)
        {
            var result = _appUnit.ImageManager.SearchImages(caption);
            return _mapper.Map<IEnumerable<ImageDTO>>(result);
        }

        public async Task<bool> DeleteImage(string imageName)
        {
            return await _appUnit.ImageManager.DeleteImage(imageName);
        }

        public IEnumerable<ImageDTO> GetAllImages(int page)
        {
            IEnumerable<Image> result;
            try
            {
                 result = _appUnit.ImageManager.GetAllImages(page);
            }
            catch (WrongPageException e)
            {
                throw new WrongPageException("Wrong page number",e);
            }
            return _mapper.Map<IEnumerable<ImageDTO>>(result);
        }

        public int GetPages()
        {
            return _appUnit.ImageManager.GetPages();
        }
    }
}
