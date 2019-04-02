using System.Threading.Tasks;
using AuthenticationDAL.Entities;
using Microsoft.AspNet.Identity;

namespace AuthenticationDAL.Interfaces
{
    public interface IRoleManager
    {
        Task<AppRole> FindByNameAsync(string roleName);
        Task<IdentityResult> CreateAsync(AppRole role);
    }
}
