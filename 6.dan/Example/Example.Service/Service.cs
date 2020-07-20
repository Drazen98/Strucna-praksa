using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Service.Common;
using Example.Repository;
using Example.Repository.Common;
using Example.Models;
using AutoMapper;
using System.Threading.Tasks;
using Autofac;

namespace Example.Service
{
    public class PersonService : IPersonService
    {
        private static IPersonRepository PersonRepositoryResolver;
        private static void DependencyInjection()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<PersonRepository>().As<IPersonRepository>();
            var container = containerBuilder.Build();
            PersonRepositoryResolver = container.Resolve<IPersonRepository>();
        }

        public async Task<List<PersonNoID>> GetPeople()
        {
            DependencyInjection();
            var configPerson = new MapperConfiguration(cfg => { cfg.CreateMap<Person, PersonNoID>(); });
            IMapper iMapper = configPerson.CreateMapper();
            return iMapper.Map<List<Person>, List<PersonNoID>>(await PersonRepositoryResolver.GetPeople());
        }
        public async Task<PersonNoID> GetPerson(int id)
        {
            DependencyInjection();
            return this.MapPersonWithoutID(await PersonRepositoryResolver.GetPerson(id));
        }
        public async Task<PersonNoID> AddPerson(Person person)
        {
            DependencyInjection();
            return this.MapPersonWithoutID(await PersonRepositoryResolver.AddPerson(person));
        }
        public async Task<PersonNoID> UpdateName(int id, string newName)
        {
            DependencyInjection();
            return MapPersonWithoutID(await PersonRepositoryResolver.UpdateName(id, newName));
        }
        public async Task<PersonNoID> DeletePerson(int id)
        {
            DependencyInjection();
            return MapPersonWithoutID(await PersonRepositoryResolver.DeletePerson(id));
        }
        public async Task<List<CarNoID>> GetPersonsCars(int id)
        {
            DependencyInjection();
            var configCar = new MapperConfiguration(cfg => { cfg.CreateMap<Car, CarNoID>(); });
            IMapper iMapper = configCar.CreateMapper();
            return iMapper.Map<List<Car>, List<CarNoID>>(await PersonRepositoryResolver.GetPersonsCars(id));
        }
        public PersonNoID MapPersonWithoutID(Person person) {
            var configPerson = new MapperConfiguration(cfg => { cfg.CreateMap<Person, PersonNoID>(); });
            IMapper iMapper = configPerson.CreateMapper();
            PersonNoID personNoID = iMapper.Map<Person, PersonNoID>(person);
            return personNoID;
        }

    }
}
