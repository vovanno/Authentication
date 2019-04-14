using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace AuthenticationDAL.Interfaces
{
    /// <summary>
    /// Provide interaction with roles and access to RoleManager through this interface.
    /// </summary>
    public interface IRoleManager
    {
        Task<IdentityRole> FindByNameAsync(string roleName);
        Task<IdentityResult> CreateAsync(IdentityRole role);
    }
}
