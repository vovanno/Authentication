using AuthenticationDAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationDAL.Interfaces
{
    public interface IClientManager: IDisposable
    {
        Task<ClientProfile> CreateProfileAsync(ClientProfile profile);
        Task<ClientProfile> GetProfileAsync(string id);
        Task<ClientProfile> FindProfileAsync(string id);
        Task<bool> ModifyProfileAsync(ClientProfile profile);
        IEnumerable<ClientProfile> GetUsersList();
    }
}
