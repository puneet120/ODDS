using Autofac;
//using Autofac.Integration.Mvc;
using System.Web.Mvc;
using OddsBusiness.ORM;
using OddsBusiness.Repository.Dependency;
using System.Configuration;
using Autofac.Integration.WebApi;
using System.Web.Http;
using System.Reflection;

namespace OddsBusiness.Web
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule(new DataModule(ConfigurationManager.ConnectionStrings["con"].ConnectionString));
            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
        //public static void ConfigureContainer()
        //{
        //    var builder = new ContainerBuilder();

        //    var configuration = GlobalConfiguration.Configuration;
             
        //     // Configure the container 
            
        //    // Register dependencies in controllers
        //    builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

        //    // Register dependencies in filter attributes
        //  //  builder.RegisterFilterProvider();

        //    // Register dependencies in custom views
        //  //  builder.RegisterSource(new ViewRegistrationSource());

        //    // Register our Data dependencies
        //    builder.RegisterModule(new DataModule(ConfigurationManager.ConnectionStrings["con"].ConnectionString));
            
        //    var container = builder.Build();

        //    var resolver = new AutofacWebApiDependencyResolver(container);
        //   configuration.se.SetResolver(resolver);

        //    // Set MVC DI resolver to use our Autofac container
        //    DependencyResolver.SetResolver(new AutofacWebApiDependencyResolver(container));
        //}
    }
}