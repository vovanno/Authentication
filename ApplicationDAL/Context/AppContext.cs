using System.Data.Entity;
using ApplicationDAL.Entities;
using ApplicationDAL.Interfaces;

namespace ApplicationDAL.Context
{
    public class AppContext : DbContext, IAppContext
    {
        public AppContext()
        {

        }

        public AppContext(string connection = "AppDataConnection") : base(connection)
        {

        }

        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
