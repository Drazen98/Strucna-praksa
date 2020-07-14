using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Example.Webapi.Controllers
{
    public class ExampleController : ApiController
    {
        [HttpGet]
        [Route("api/Example/Person")]
        public IEnumerable<Person> Get()
        {
            return People.listAll();
        }

        [HttpGet]
        [Route("api/Example/Person/{id:int}")]
        public HttpResponseMessage GetName(int id)
        {
            if (People.exists(id))
                return Request.CreateResponse(HttpStatusCode.OK, People.persons[id]);
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
        }

        [HttpPost]
        [Route("api/Example/Person")]
        public void JsonStringBody([FromBody]Person value)
        {
            People.addNewPerson(value);
        }

        [HttpPut]
        [Route("api/Example/Person/{id:int}")]
        public HttpResponseMessage updateName(int id,[FromBody] string newName) {
            if (People.exists(id))
            {
                People.persons[id].changeName(newName);
                return Request.CreateResponse<Person>(HttpStatusCode.OK, People.persons[id]);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
        }

        [HttpDelete]
        [Route("api/Example/Person/{id:int}")]
        public HttpResponseMessage DeletePerson(int id)
        {
            if (People.exists(id))
            {
                Person tmp = People.persons[id];
                People.persons.RemoveAt(id);
                return Request.CreateResponse<Person>(HttpStatusCode.OK, tmp);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
            }
        }
    }

    public static class People
    {
        public static List<Person> persons = new List<Person>();
        public static int idCounter = 0;

        static public void addNewPerson(Person person)
        {
            person.addId(People.idCounter);
            People.idCounter++;
            People.persons.Add(person);
        }
        static public void deletePerson(int id)
        {
            People.persons.RemoveAt(id);
        }
        static public bool exists(int id) { 
            return (id >= 0 && id < People.persons.Count) ;
                }
        static public List<Person> listAll()
        {
            return persons;
        }
    }

    public class Person 
    {
        public string firstName;
        public string lastName;
        public int height;
        public double weight;
        public int personId;
    
        public Person(string firstName, string lastName, int height, double weight)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.height = height;
            this.weight = weight;
        }
        public void addId(int id)
        {
            this.personId = id;
        }
        public void changeName(string newName) {
            this.firstName = newName;
        }
    }
}
