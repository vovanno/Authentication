using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using AuthenticationDAL.Entities;
using AuthenticationDAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace AuthenticateBLL.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public AuthenticateService(IUnitOfWork unit)
        {
            _mapper = new MapperConfiguration(c => c.CreateMap<ProfileDTO, ClientProfile>().ReverseMap()).CreateMapper();
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
            var result = await _unit.UserManager.CreateAsync(tempUser, user.Password);
            await _unit.ClientManager.CreateProfileAsync(_mapper.Map<ClientProfile>(new ProfileDTO(){Id = tempUser.Id}));
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
            var adm = new AppUser() { Email = admin.Email, UserName = admin.UserName };
            var roles = new List<string>() { "admin", "user" };
            foreach (var role in roles)
            {
                var roleOb = await _unit.RoleManager.FindByNameAsync(role);
                if (roleOb == null)
                {
                    roleOb = new AppRole() { Name = role };
                    await _unit.RoleManager.CreateAsync(roleOb);
                }
            }
            const string password = "Admin123_";
            var result = await _unit.UserManager.CreateAsync(adm, password);
            if (result.Succeeded)
            {
                await _unit.ClientManager.CreateProfileAsync(_mapper.Map<ClientProfile>(new ProfileDTO()
                    {Id = adm.Id}));
                var getId = await _unit.UserManager.FindByEmailAsync(admin.Email);
                await _unit.UserManager.AddToRoleAsync(getId.Id, "admin");
            }
        }

        public void Dispose()
        {
            _unit.Dispose();
        }
    }
}
