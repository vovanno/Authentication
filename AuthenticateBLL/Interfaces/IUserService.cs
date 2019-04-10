using System.Collections.Generic;
using System.Globalization;
using AuthenticateBLL.DTO;
using System.Threading.Tasks;

namespace AuthenticateBLL.Interfaces
{
    public interface IUserService
    {
        Task<ProfileDTO> GetUserProfile(string id);
        ProfileDTO FindByEmailAsync(string email);
        Task<bool> ModifyUserProfile(ProfileDTO profile);
        IEnumerable<ProfileDTO> GetUsersList();
        Task<bool> UpdateAvatar(string id, string name);
    }
}
