using AuthenticationDAL.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace AuthenticationDAL.Interfaces
{
    public interface IApplicationContext : IDisposable
    {
        IDbSet<AppUser> Users { get; set; }
        DbSet<ClientProfile> ClientProfiles { get; set; }
        Task<int> SaveChangesAsync();
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
