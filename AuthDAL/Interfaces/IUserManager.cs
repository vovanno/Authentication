using System;
using System.Threading.Tasks;
using AuthDAL.Infrastructure;
using Microsoft.AspNet.Identity;

namespace AuthDAL.Interfaces
{
    public interface IUserManager : IDisposable
    {
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByIdAsync(string email);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> AddToRoleAsync(string userId, string role);
        Task<IdentityResult> DeleteAsync(ApplicationUser user);
    }
}
