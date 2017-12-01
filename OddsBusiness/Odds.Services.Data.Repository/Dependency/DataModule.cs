using Autofac;
using OddsBusiness.Repository.Interfaces;
using OddsBusiness.ORM.Context;
using OddsBusiness.Repository.Implementation;

namespace OddsBusiness.Repository.Dependency
{
    public class DataModule : Module
    {
        private string connStr;
        public DataModule(string connString)
        {
            this.connStr = connString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new EFContext(this.connStr)).As<IDbContext>().InstancePerRequest();
            builder.RegisterGeneric(typeof(SqlRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();           
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();         
            builder.RegisterType<Logger>().As<ILogger>().InstancePerRequest();
            builder.RegisterType<OddRepository>().As<IOddRepository>().InstancePerRequest();
            base.Load(builder);
        }
    }
}
