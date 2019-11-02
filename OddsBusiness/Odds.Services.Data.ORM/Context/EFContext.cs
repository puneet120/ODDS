
using OddsBusiness.Core.Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace OddsBusiness.ORM.Context
{
    public class EFContext : DbContext, IDbContext
    {
        public EFContext(string connString)
            : base(connString)
        {
            Database.SetInitializer<EFContext>(new DBInitializer());
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();          
            modelBuilder.Entity<Login>();
            modelBuilder.Entity<Odd>();
            base.OnModelCreating(modelBuilder);
        }
    }
}