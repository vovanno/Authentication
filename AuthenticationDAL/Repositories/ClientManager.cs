using AuthenticationDAL.Context;
using AuthenticationDAL.Entities;
using AuthenticationDAL.Interfaces;

namespace AuthenticationDAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public AppContext Context { get; set; }

        public ClientManager(AppContext context)
        {
            Context = context;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void Create(ClientProfile profile)
        {
            Context.ClientProfiles.Add(profile);
            Context.SaveChanges();
        }
    }
}
