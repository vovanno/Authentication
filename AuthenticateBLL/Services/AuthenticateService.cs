using ApplicationDAL.Entities;
using ApplicationDAL.Interfaces;
using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using AuthenticationDAL.Interfaces;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticateBLL.Services
{
    /// <summary>
    /// Service for registering new users and set initial data in database.
    /// </summary>
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUnitOfWork _unit;
        private readonly IAppUnitOfWork _appUnit;
        private readonly IMapper _mapper;

        public AuthenticateService(IUnitOfWork unit, IAppUnitOfWork appUnit)
        {
            _mapper = new MapperConfiguration(c => c.CreateMap<ProfileDTO, ClientProfile>().ReverseMap()).CreateMapper();
            _unit = unit;
            _appUnit = appUnit;
        }

        public async Task<IdentityResult> Create(UserDTO user)
        {
            var tempUser = await _unit.UserManager.FindByEmailAsync(user.Email);
            if (tempUser != null)
            {
                throw new Exception("User already exist");
            }
            tempUser = new IdentityUser() { Email = user.Email, UserName = user.UserName };
            var result = await _unit.UserManager.CreateAsync(tempUser, user.Password);
            if (result.Errors.Any())
                return result;
            await _appUnit.ClientManager.CreateProfileAsync(_mapper.Map<ClientProfile>(new ProfileDTO()
            {
                Id = tempUser.Id,
                AvatarImage = "DefaultUser.jpg"
            }));
            if (result.Succeeded)
            {
                var getId = await _unit.UserManager.FindByEmailAsync(user.Email);
                await _unit.UserManager.AddToRoleAsync(getId.Id, "user");
            }
            return result;
        }

        public async Task SetInitialData(UserDTO admin)
        {
            var temp = await _unit.UserManager.FindByEmailAsync(admin.Email);
            if (temp != null)
                return;
            var adm = new IdentityUser() { Email = admin.Email, UserName = admin.UserName };
            var roles = new List<string>() { "admin", "user" };
            foreach (var role in roles)
            {
                var roleOb = await _unit.RoleManager.FindByNameAsync(role);
                if (roleOb == null)
                {
                    roleOb = new IdentityRole() { Name = role };
                    await _unit.RoleManager.CreateAsync(roleOb);
                }
            }
            const string password = "Admin123_";
            var result = await _unit.UserManager.CreateAsync(adm, password);
            if (result.Succeeded)
            {
                await _appUnit.ClientManager.CreateProfileAsync(_mapper.Map<ClientProfile>(new ProfileDTO()
                { Id = adm.Id, AvatarImage = "DefaultUser.jpg" }));
                var userId = await _unit.UserManager.FindByEmailAsync(admin.Email);
                await _unit.UserManager.AddToRoleAsync(userId.Id, "admin");
            }
        }

        public void Dispose()
        {
            _unit.Dispose();
        }
    }
}
