using AuthenticateBLL.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AuthenticateBLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<IdentityResult> Create(UserDTO user);
        Task<ClaimsIdentity> Authenticate(UserDTO user);
        Task SetInitialData(UserDTO admin, List<string> roles);
    }
}
