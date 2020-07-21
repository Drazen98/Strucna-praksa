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
        Task<List<PersonNoID>> GetPeople();
        Task<PersonNoID> GetPerson(int id);
        Task<PersonNoID> AddPerson(Person person);
        Task<PersonNoID> UpdateName(int id, string newName);
        Task<PersonNoID> DeletePerson(int id);
        Task<List<CarNoID>> GetPersonsCars(int id);
    }
}
