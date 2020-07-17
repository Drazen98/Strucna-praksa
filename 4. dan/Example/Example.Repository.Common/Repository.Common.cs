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
        List<Person> GetPeople();
        Person GetPerson(int id);
        Person AddPerson(Person person);
        Person UpdateName(int id, string newName);
        Person DeletePerson(int id);
        List<Car> GetPersonsCars(int id);
        bool Exists(int id);

    }
}
