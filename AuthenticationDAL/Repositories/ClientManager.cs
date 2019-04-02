using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationDAL.Entities;
using AuthenticationDAL.Interfaces;

namespace AuthenticationDAL.Repositories
{
    public class ClientManager : IClientManager
    {
        private IApplicationContext Context { get; }

        public ClientManager(IApplicationContext context)
        {
            Context = context;
        }

        public  IEnumerable<ClientProfile> GetUsersList()
        {
            return  Context.ClientProfiles.AsEnumerable();
        }

        public async Task<ClientProfile> CreateProfileAsync(ClientProfile profile)
        {
            var result = Context.ClientProfiles.Add(profile);
            await Context.SaveChangesAsync();
            return result;
        }

        public async Task<ClientProfile> GetProfileAsync(string id)
        {
            return await Context.ClientProfiles.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ClientProfile> FindProfileAsync(string id)
        {
            return await Context.ClientProfiles.FindAsync(id);
        }

        public async Task<bool> ModifyProfileAsync(ClientProfile profile)
        {
            var temp = Context.ClientProfiles.FirstOrDefault(p => p.Id == profile.Id);
            if (temp==null)
            {
                return false;
            }
            temp.Address = profile.Address;
            temp.FirstName = profile.FirstName;
            temp.LastName = profile.LastName;
            Context.Entry(temp).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return true;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
