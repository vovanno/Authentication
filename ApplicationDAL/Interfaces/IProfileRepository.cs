using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationDAL.Entities;

namespace ApplicationDAL.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// Provides access for interaction with user's profiles.
    /// </summary>
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
