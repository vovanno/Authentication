using System.Collections.Generic;
using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using AuthenticationDAL.Entities;
using AuthenticationDAL.Interfaces;
using System.Threading.Tasks;
using AutoMapper;

namespace AuthenticateBLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unit)
        {
            _unit = unit;
            _mapper = new MapperConfiguration(c=>c.CreateMap<ProfileDTO, ClientProfile>().ReverseMap()).CreateMapper();
        }

        public IEnumerable<ProfileDTO> GetUsersList()
        {
            return _mapper.Map<IEnumerable<ProfileDTO>>(_unit.ClientManager.GetUsersList());
        }

        public async Task<ProfileDTO> CreateProfile(ProfileDTO profile)
        {
            var result = await _unit.ClientManager.CreateProfileAsync(_mapper.Map<ClientProfile>(profile));
            return _mapper.Map<ProfileDTO>(result);
        }

        public async Task<ProfileDTO> GetUserProfile(string id)
        {
            var result = await _unit.ClientManager.GetProfileAsync(id);
            return result == null ? new ProfileDTO() { FirstName = "", LastName = "", Address = "" } : _mapper.Map<ProfileDTO>(result);
        }

        public async Task<bool> ModifyUserProfile(ProfileDTO profile)
        {
            return await _unit.ClientManager.ModifyProfileAsync(_mapper.Map<ClientProfile>(profile));
        }
    }
}
