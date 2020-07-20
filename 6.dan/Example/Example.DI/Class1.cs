using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Repository;
using Example.Repository.Common;
using Example.Service;
using Example.Service.Common;
using Autofac;

namespace Example.DI
{
    public class DI
    {
        static DI()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<PersonRepository>().As<IPersonRepository>();
            containerBuilder.RegisterType<PersonService>().As<IPersonService>();
            var container = containerBuilder.Build();
        }
    }
}
