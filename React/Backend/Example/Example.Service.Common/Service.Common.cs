using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Models;

namespace Example.Service.Common
{
    public interface IPersonService
    {
        Task<List<Person>> GetPeople();
        Task<Person> GetPerson(int id);
        Task<PersonNoID> AddPerson(Person person);
        Task<Person> UpdateName(int id, string newName);
        Task<Person> DeletePerson(int id);
        Task<List<CarNoID>> GetPersonsCars(int id);
    }
}
