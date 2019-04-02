using AuthenticationDAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using AuthenticationDAL.Interfaces;

namespace AuthenticationDAL.Context
{
    public class AppContext : IdentityDbContext<AppUser>, IApplicationContext
    {
        private static string _connectionString = "DefaultConnection";

        public AppContext()
        {

        }

        public AppContext(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Images> Images { get; set; }

        public static AppContext Create()
        {
            return new AppContext(_connectionString);
        }
    }
}
