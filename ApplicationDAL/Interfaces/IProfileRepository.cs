using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationDAL.Entities;

namespace ApplicationDAL.Interfaces
{
    public interface IProfileRepository: IDisposable
    {
        Task<ClientProfile> CreateProfileAsync(ClientProfile profile);
        Task<ClientProfile> GetProfileAsync(string id);
        Task<ClientProfile> FindProfileAsync(string id);
        Task<bool> ModifyProfileAsync(ClientProfile profile);
        IEnumerable<ClientProfile> GetUsersList();
        Task<bool> UpdateAvatar(string id, string name);
        Task<bool> DeleteProfileAsync(ClientProfile profile);
    }
}
