using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace AuthenticationDAL.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// Provides interaction with identity context through this interface.
    /// </summary>
    public interface IIdentiyContext : IDisposable
    {
        IDbSet<IdentityUser> Users { get; set; }
        Task<int> SaveChangesAsync();
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
