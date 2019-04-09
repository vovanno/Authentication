using AuthenticationDAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace AuthenticationDAL.Context
{
    public class IdentityContext : IdentityDbContext<IdentityUser>, IIdentiyContext
    {
        private static string _connectionString = "DefaultConnection";

        public IdentityContext()
        {

        }

        public IdentityContext(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }

        public static IdentityContext Create()
        {
            return new IdentityContext(_connectionString);
        }
    }
}
