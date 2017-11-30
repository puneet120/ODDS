using OddsBusiness.Core.Entity;
using System;
using System.Data.Entity;
namespace OddsBusiness.ORM.Context
{
    public interface IDbContext : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
    }
}
