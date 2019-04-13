using AuthenticateBLL.DTO;
using System.Threading.Tasks;

namespace AuthenticateBLL.Interfaces
{
    public interface IAdminService
    {
        Task<bool> DeleteAsync(UserDTO user);
        Task<bool> DeleteProfile(ProfileDTO profile);
    }
}
