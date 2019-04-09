using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace AuthenticationDAL.Interfaces
{
    public interface IRoleManager
    {
        Task<IdentityRole> FindByNameAsync(string roleName);
        Task<IdentityResult> CreateAsync(IdentityRole role);
    }
}
