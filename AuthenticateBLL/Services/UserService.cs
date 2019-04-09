using ApplicationDAL.Entities;
using ApplicationDAL.Interfaces;
using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticateBLL.Services
{
    public class UserService : IUserService
    {
        private readonly IAppUnitOfWork _appUnit;
        private readonly IMapper _mapper;

        public UserService(IAppUnitOfWork appUnit)
        {
            _appUnit = appUnit;
            _mapper = new MapperConfiguration(c=>c.CreateMap<ProfileDTO, ClientProfile>().ReverseMap()).CreateMapper();
        }

        public IEnumerable<ProfileDTO> GetUsersList()
        {
            return _mapper.Map<IEnumerable<ProfileDTO>>(_appUnit.ClientManager.GetUsersList());
        }

        public async Task<bool> UpdateAvatar(string id, string name)
        {
            return await _appUnit.ClientManager.UpdateAvatar(id, name);
        }

        public async Task<ProfileDTO> GetUserProfile(string id)
        {
            var result = await _appUnit.ClientManager.GetProfileAsync(id);
            if (result.FirstName != null || result.LastName != null || result.Address != null)
                return _mapper.Map<ProfileDTO>(result);
            result.FirstName = "";
            result.LastName = "";
            result.Address = "";
            return _mapper.Map<ProfileDTO>(result);
        }

        public async Task<bool> ModifyUserProfile(ProfileDTO profile)
        {
            return await _appUnit.ClientManager.ModifyProfileAsync(_mapper.Map<ClientProfile>(profile));
        }
    }
}
