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
        private IPersonRepository PersonRepositoryResolver;
        public  PersonService(IPersonRepository repositoryResolver)
        {
            this.PersonRepositoryResolver = repositoryResolver;
        }

        public async Task<List<Person>> GetPeople()
        {
            var configPerson = new MapperConfiguration(cfg => { cfg.CreateMap<Person, PersonNoID>(); });
            IMapper iMapper = configPerson.CreateMapper();
            return await this.PersonRepositoryResolver.GetPeople();
        }
        public async Task<Person> GetPerson(int id)
        {
            return await this.PersonRepositoryResolver.GetPerson(id);
        }
        public async Task<PersonNoID> AddPerson(Person person)
        {
            return this.MapPersonWithoutID(await this.PersonRepositoryResolver.AddPerson(person));
        }
        public async Task<Person> UpdateName(int id, string newName)
        {
            return await this.PersonRepositoryResolver.UpdateName(id, newName);
        }
        public async Task<Person> DeletePerson(int id)
        {
            return await this.PersonRepositoryResolver.DeletePerson(id);
        }
        public async Task<List<CarNoID>> GetPersonsCars(int id)
        {
            var configCar = new MapperConfiguration(cfg => { cfg.CreateMap<Car, CarNoID>(); });
            IMapper iMapper = configCar.CreateMapper();
            return iMapper.Map<List<Car>, List<CarNoID>>(await this.PersonRepositoryResolver.GetPersonsCars(id));
        }
        public PersonNoID MapPersonWithoutID(Person person) {
            var configPerson = new MapperConfiguration(cfg => { cfg.CreateMap<Person, PersonNoID>(); });
            IMapper iMapper = configPerson.CreateMapper();
            PersonNoID personNoID = iMapper.Map<Person, PersonNoID>(person);
            return personNoID;
        }

    }
}
