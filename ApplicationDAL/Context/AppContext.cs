using System.Data.Entity;
using ApplicationDAL.Entities;
using ApplicationDAL.Interfaces;

namespace ApplicationDAL.Context
{
    /// <summary>
    /// Implements DbContext for providing interaction with main functionality database.
    /// </summary>
    public class AppContext : DbContext, IAppContext
    {
        public AppContext(string connection) : base(connection)
        {

        }

        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
