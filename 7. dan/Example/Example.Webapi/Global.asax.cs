using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Reflection;
using Autofac;
using Example.Service;
using Example.Service.Common;
using Example.Models;
using Autofac.Integration.WebApi;
using Example.Webapi.Controllers;
using Example.Repository;
using Example.Repository.Common;

namespace Example.Webapi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            containerBuilder.RegisterModule(new DIService());
            containerBuilder.RegisterModule(new DIRepository());

            var container = containerBuilder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
           
        }
    }
}
