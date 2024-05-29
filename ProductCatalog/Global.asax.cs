using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using ProductCatalog.Repository;
using ProductCatalog.Service;
using System.Web.Http;

namespace ProductCatalog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Register dependencies
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register API controllers using assembly scanning.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register ProductDbContext
            builder.RegisterType<ProductDbContext>().AsSelf().InstancePerRequest();

            // Register your service dependencies.
            builder.RegisterType<ProductDbRepository>().As<IProductRepository>().InstancePerRequest();

            // Build the container.
            var container = builder.Build();

            // Set the dependency resolver for MVC.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Set the dependency resolver for Web API.
            var configuration = GlobalConfiguration.Configuration;
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Register MVC areas
            AreaRegistration.RegisterAllAreas();

            // Register global filters
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Register MVC routes
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Register bundles
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
