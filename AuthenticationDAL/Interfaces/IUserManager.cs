using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace AuthenticationDAL.Interfaces
{
    public interface IUserManager: IDisposable
    {
        Task<IdentityUser> FindByEmailAsync(string email);
        Task<IdentityUser> FindByIdAsync(string email);
        Task<IdentityResult> CreateAsync(IdentityUser user, string password);
        Task<IdentityResult> AddToRoleAsync(string userId, string role);
        Task<IdentityResult> DeleteAsync(IdentityUser user);
    }
}
