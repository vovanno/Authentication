using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ApplicationDAL.Entities;
using ApplicationDAL.Interfaces;

namespace ApplicationDAL.Repositories
{
    /// <inheritdoc />
    /// <summary>
    /// Implements IProfileRepository for interaction with user's profiles.
    /// </summary>
    public class ProfileRepository : IProfileRepository
    {
        private readonly IAppContext _context;

        public ProfileRepository(IAppContext context)
        {
            _context = context;
        }

        public IEnumerable<ClientProfile> GetUsersList()
        {
            return _context.ClientProfiles.AsEnumerable();
        }

        public async Task<bool> UpdateAvatar(string id, string name)
        {
            var result = await _context.ClientProfiles.FirstOrDefaultAsync(p => p.Id == id);
            if (result == null)
                return false;
            result.AvatarImage = name;
            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProfileAsync(ClientProfile profile)
        {
            var result = await _context.ClientProfiles.FindAsync(profile);
            if (result == null)
                return false;
            _context.ClientProfiles.Remove(profile);
            _context.Entry(profile).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ClientProfile> CreateProfileAsync(ClientProfile profile)
        {
            var result = _context.ClientProfiles.Add(profile);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<ClientProfile> GetProfileAsync(string id)
        {
            return await _context.ClientProfiles.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ClientProfile> FindProfileAsync(string id)
        {
            return await _context.ClientProfiles.FindAsync(id);
        }

        public async Task<bool> ModifyProfileAsync(ClientProfile profile)
        {
            var temp = _context.ClientProfiles.FirstOrDefault(p => p.Id == profile.Id);
            if (temp == null)
            {
                return false;
            }
            temp.Address = profile.Address;
            temp.FirstName = profile.FirstName;
            temp.LastName = profile.LastName;
            _context.Entry(temp).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
