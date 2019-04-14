using ApplicationDAL.Entities;
using ApplicationDAL.Interfaces;
using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using AuthenticationDAL.Interfaces;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace AuthenticateBLL.Services
{
    /// <summary>
    /// Provides access to manage users and their accounts.
    /// </summary>
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unit;
        private readonly IAppUnitOfWork _appUnit;
        private readonly IMapper _mapper;

        public AdminService(IUnitOfWork unit, IAppUnitOfWork appUnit)
        {
            _unit = unit;
            _appUnit = appUnit;
            _mapper = new MapperConfiguration(c => c.CreateMap<UserDTO, IdentityUser>().ReverseMap()).CreateMapper();
        }
        public async Task<bool> DeleteAsync(UserDTO user)
        {
            var tempUser = _mapper.Map<IdentityUser>(user);
            var result = await _unit.UserManager.DeleteAsync(tempUser);
            return result.Succeeded;
        }

        public async Task<bool> DeleteProfile(ProfileDTO profile)
        {
            var tempProfile = _mapper.Map<ClientProfile>(profile);
            return await _appUnit.ClientManager.DeleteProfileAsync(tempProfile);
        }
    }
}
