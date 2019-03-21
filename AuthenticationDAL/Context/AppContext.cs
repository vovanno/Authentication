using AuthenticationDAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace AuthenticationDAL.Context
{
    public class AppContext : IdentityDbContext<AppUser>
    {
        
        public AppContext(string connectionString) : base(connectionString)
        {
            
        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public static AppContext Create()
        {
            return new AppContext("DefaultConnectionS");
        }
    }
}
