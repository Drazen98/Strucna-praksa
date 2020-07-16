using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Models;

namespace Example.Service.Common
{
    public interface IService
    {
        List<PersonNoID> GetPeople();
        PersonNoID GetPerson(int id);
        PersonNoID AddPerson(Person person);
        PersonNoID UpdateName(int id, string newName);
        PersonNoID DeletePerson(int id);
        List<CarNoID> GetPersonsCars(int id);
    }
}
