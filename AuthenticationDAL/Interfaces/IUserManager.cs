using AuthenticationDAL.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationDAL.Interfaces
{
    public interface IUserManager: IDisposable
    {
        Task<AppUser> FindByEmailAsync(string email);
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        Task<IdentityResult> AddToRoleAsync(string userId, string role);
        Task<AppUser> FindAsync(string userName, string password);
        
    }
}
