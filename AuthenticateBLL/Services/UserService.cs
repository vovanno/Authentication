using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using AuthenticationDAL.Entities;
using AuthenticationDAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace AuthenticateBLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unit;

        public UserService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<IdentityResult> Create(UserDTO user)
        {
            var tempUser = await _unit.UserManager.FindByEmailAsync(user.Email);
            if (tempUser != null)
            {
                throw new Exception("User already exist");
            }
            tempUser = new AppUser() { Email = user.Email, UserName = user.UserName };
            await _unit.UserManager.CreateAsync(tempUser, user.Password);
            var profile = new ClientProfile() { Id = tempUser.Id, Address = user.Address, Name = user.Name };
            _unit.ClientManager.Create(profile);
            await _unit.SaveAsync();
            return await _unit.UserManager.AddToRoleAsync(tempUser.Id, user.Role);
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO user)
        {
            ClaimsIdentity claims = null;
            var tempUser = await _unit.UserManager.FindAsync(user.Email, user.Password);
            if(tempUser!=null)
                claims = await _unit.UserManager.CreateIdentityAsync(tempUser,DefaultAuthenticationTypes.ExternalBearer);
            return claims;
        }

        public async Task SetInitialData(UserDTO admin, List<string> roles)
        {
            foreach (var role in roles)
            {
                var roleOb = await _unit.RoleManager.FindByNameAsync(role);
                if (roleOb == null)
                {
                    roleOb = new AppRole();
                    await _unit.RoleManager.CreateAsync(roleOb);
                }
            }

            await Create(admin);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
