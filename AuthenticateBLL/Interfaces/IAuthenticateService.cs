using AuthenticateBLL.DTO;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace AuthenticateBLL.Interfaces
{
    public interface IAuthenticateService : IDisposable
    {
        Task<IdentityResult> Create(UserDTO user);
        Task SetInitialData(UserDTO admin);
        
    }
}
