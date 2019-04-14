using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using AuthDAL.Infrastructure;

namespace AuthDAL.Interfaces
{
    public interface IIdentiyContext : IDisposable
    {
        IDbSet<ApplicationUser> Users { get; set; }
        Task<int> SaveChangesAsync();
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
