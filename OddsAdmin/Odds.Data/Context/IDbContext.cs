using Odds.Core.Entity;
using System;
using System.Data.Entity;
namespace Odds.Data.Context
{
    public interface IDbContext : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
    }
}
