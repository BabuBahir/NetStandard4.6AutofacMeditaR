﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Owin;
using Owin;
using Sample4._6Api.Controllers;
using Sample4._6Api.Models;

[assembly: OwinStartup(typeof(Sample4._6Api.Startup1))]

namespace Sample4._6Api
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // In OWIN you create your own HttpConfiguration rather than
            // re-using the GlobalConfiguration.
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            var builder = new ContainerBuilder();

            // Register Web API controller in executing assembly.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
             
            // OPTIONAL - Register the filter provider if you have custom filters that need DI.
            // Also hook the filters up to controllers.
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterType<CustomActionFilter>()
                .AsWebApiActionFilterFor<TestController>()
                .InstancePerRequest();

            // Register a logger service to be used by the controller and middleware.
            builder.Register(c => new Logger()).As<ILogger>().InstancePerRequest();

            // Autofac will add middleware to IAppBuilder in the order registered.
            // The middleware will execute in the order added to IAppBuilder.
            builder.RegisterType<FirstMiddleware>().InstancePerRequest();
            //builder.RegisterType<SecondMiddleware>().InstancePerRequest();

            //builder.RegisterType(typeof(IRequestHandler<GetSampleDataQuery, string>))
            //        .As<IRequestHandler<IRequestHandler<GetSampleDataQuery, string>()
            //        .AsImplementedInterfaces();
            //builder.AddMediatR(typeof(Startup1).Assembly);
            //builder.RegisterType<Mediator>().As<IMediator>();
          //  builder.AddMediatR(this.GetType().Assembly);


            // Create and assign a dependency resolver for Web API to use.
            var container = builder.Build(); 

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // The Autofac middleware should be the first middleware added to the IAppBuilder.
            // If you "UseAutofacMiddleware" then all of the middleware in the container
            // will be injected into the pipeline right after the Autofac lifetime scope
            // is created/injected.
            //
            // Alternatively, you can control when container-based
            // middleware is used by using "UseAutofacLifetimeScopeInjector" along with
            // "UseMiddlewareFromContainer". As long as the lifetime scope injector
            // comes first, everything is good.
            app.UseAutofacMiddleware(container);

            // Again, the alternative to "UseAutofacMiddleware" is something like this:
            // app.UseAutofacLifetimeScopeInjector(container);
            // app.UseMiddlewareFromContainer<FirstMiddleware>();
            // app.UseMiddlewareFromContainer<SecondMiddleware>();

            // Make sure the Autofac lifetime scope is passed to Web API.
            app.UseAutofacWebApi(config);
            app.UseWebApi(config); 

        }
    }
}
