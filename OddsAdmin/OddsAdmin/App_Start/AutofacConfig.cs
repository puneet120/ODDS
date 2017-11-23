using Autofac;
//using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Odds.Data;
using Odds.Services.Dependency;
using System.Configuration;
using Autofac.Integration.Mvc;

namespace OddsAdmin.Web
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            
            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());

            // Register our Data dependencies
            builder.RegisterModule(new DataModule(ConfigurationManager.ConnectionStrings["con"].ConnectionString));
            
            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}