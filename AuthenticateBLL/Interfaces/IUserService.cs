﻿using System.Collections.Generic;
using System.Globalization;
using AuthenticateBLL.DTO;
using System.Threading.Tasks;

namespace AuthenticateBLL.Interfaces
{
    public interface IUserService
    {
        Task<ProfileDTO> GetUserProfile(string id);
        Task<bool> ModifyUserProfile(ProfileDTO profile);
        IEnumerable<ProfileDTO> GetUsersList();
        Task<bool> UpdateAvatar(string id, string name);
        Task<UserDTO> GetUserByIdAsync(string id);

    }
}
