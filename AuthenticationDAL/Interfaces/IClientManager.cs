using AuthenticationDAL.Entities;
using System;

namespace AuthenticationDAL.Interfaces
{
    public interface IClientManager: IDisposable
    {
        void Create(ClientProfile profile);
    }
}
