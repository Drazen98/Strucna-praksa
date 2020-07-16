using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Service.Common;
using Example.Repository;
using Example.Models;
using AutoMapper;
namespace Example.Service
{
    public class Service : IService
    {
        private Repository.Repository repository = new Repository.Repository();
        public List<PersonNoID> GetPeople()
        {
            var configPerson = new MapperConfiguration(cfg => { cfg.CreateMap<Person, PersonNoID>(); });
            IMapper iMapper = configPerson.CreateMapper();
            return iMapper.Map<List<Person>, List<PersonNoID>>(this.repository.GetPeople());
        }
        public PersonNoID GetPerson(int id)
        {
            return this.MapPersonWithoutID(this.repository.GetPerson(id));
        }
        public PersonNoID AddPerson(Person person)
        {
            return this.MapPersonWithoutID(repository.AddPerson(person));
        }
        public PersonNoID UpdateName(int id, string newName)
        {
            return MapPersonWithoutID(this.repository.UpdateName(id, newName));
        }
        public PersonNoID DeletePerson(int id)
        {
            return MapPersonWithoutID(this.repository.DeletePerson(id));
        }
        public List<CarNoID> GetPersonsCars(int id)
        {
            var configCar = new MapperConfiguration(cfg => { cfg.CreateMap<Car, CarNoID>(); });
            IMapper iMapper = configCar.CreateMapper();
            return iMapper.Map<List<Car>, List<CarNoID>>(this.repository.GetPersonsCars(id));
        }
        public PersonNoID MapPersonWithoutID(Person person) {
            var configPerson = new MapperConfiguration(cfg => { cfg.CreateMap<Person, PersonNoID>(); });
            IMapper iMapper = configPerson.CreateMapper();
            PersonNoID personNoID = iMapper.Map<Person, PersonNoID>(person);
            return personNoID;
        }

    }
}
