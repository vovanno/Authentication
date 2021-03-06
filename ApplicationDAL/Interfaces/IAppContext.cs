﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using ApplicationDAL.Entities;

namespace ApplicationDAL.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// Provides context access through an interface.
    /// </summary>
    public interface IAppContext : IDisposable
    {
        DbSet<Image> Images { get; set; }
        DbSet<ClientProfile> ClientProfiles { get; set; }
        Task<int> SaveChangesAsync();
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    }
}
