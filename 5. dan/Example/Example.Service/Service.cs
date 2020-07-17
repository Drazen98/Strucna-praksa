using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Service.Common;
using Example.Repository;
using Example.Models;
using AutoMapper;
using System.Threading.Tasks;


namespace Example.Service
{
    public class PersonService : IPersonService
    {
        private Repository.PersonRepository repository = new Repository.PersonRepository();
        public async Task<List<PersonNoID>> GetPeople()
        {
            var configPerson = new MapperConfiguration(cfg => { cfg.CreateMap<Person, PersonNoID>(); });
            IMapper iMapper = configPerson.CreateMapper();
            return iMapper.Map<List<Person>, List<PersonNoID>>(await this.repository.GetPeople());
        }
        public async Task<PersonNoID> GetPerson(int id)
        {
            return this.MapPersonWithoutID(await this.repository.GetPerson(id));
        }
        public async Task<PersonNoID> AddPerson(Person person)
        {
            return this.MapPersonWithoutID(await repository.AddPerson(person));
        }
        public async Task<PersonNoID> UpdateName(int id, string newName)
        {
            return MapPersonWithoutID(await this.repository.UpdateName(id, newName));
        }
        public async Task<PersonNoID> DeletePerson(int id)
        {
            return MapPersonWithoutID(await this.repository.DeletePerson(id));
        }
        public async Task<List<CarNoID>> GetPersonsCars(int id)
        {
            var configCar = new MapperConfiguration(cfg => { cfg.CreateMap<Car, CarNoID>(); });
            IMapper iMapper = configCar.CreateMapper();
            return iMapper.Map<List<Car>, List<CarNoID>>(await this.repository.GetPersonsCars(id));
        }
        public PersonNoID MapPersonWithoutID(Person person) {
            var configPerson = new MapperConfiguration(cfg => { cfg.CreateMap<Person, PersonNoID>(); });
            IMapper iMapper = configPerson.CreateMapper();
            PersonNoID personNoID = iMapper.Map<Person, PersonNoID>(person);
            return personNoID;
        }

    }
}
