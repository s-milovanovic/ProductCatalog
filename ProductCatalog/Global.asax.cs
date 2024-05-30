﻿using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using ProductCatalog.Repository;
using ProductCatalog.Service;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProductCatalog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Initialize AutoMapper
            Mapper.Initialize(config => config.AddProfile<MappingProfile>());

            // Register dependencies
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register API controllers using assembly scanning.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register ProductDbContext
            builder.RegisterType<ProductDbContext>().AsSelf().InstancePerRequest();

            // Register your service dependencies.
            builder.RegisterType<ProductFileRepository>().As<IProductRepository>().InstancePerRequest();

            // Build the container.
            var container = builder.Build();

            // Set the dependency resolver for MVC.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Set the dependency resolver for Web API.
            var configuration = GlobalConfiguration.Configuration;
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Register MVC areas
            AreaRegistration.RegisterAllAreas();

            // Register Web API routes
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Register global filters
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Register MVC routes
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Register bundles
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
