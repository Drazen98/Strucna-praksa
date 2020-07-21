using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Example.Service.Common;
namespace Example.Service
{
    public class DIService : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PersonService>().As<IPersonService>();
        }
    }
}
