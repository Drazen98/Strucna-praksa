using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Models;

namespace Example.Repository.Common
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetPeople();
        Task<Person> GetPerson(int id);
        Task<Person> AddPerson(Person person);
        Task<Person> UpdateName(int id, string newName);
        Task<Person> DeletePerson(int id);
        Task<List<Car>> GetPersonsCars(int id);
        bool Exists(int id);

    }
}
