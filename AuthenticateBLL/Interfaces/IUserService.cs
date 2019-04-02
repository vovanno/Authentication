using System.Collections.Generic;
using AuthenticateBLL.DTO;
using System.Threading.Tasks;

namespace AuthenticateBLL.Interfaces
{
    public interface IUserService
    {
        Task<ProfileDTO> GetUserProfile(string id);
        Task<bool> ModifyUserProfile(ProfileDTO profile);
        IEnumerable<ProfileDTO> GetUsersList();
    }
}
