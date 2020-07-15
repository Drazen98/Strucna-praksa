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
        public static List<Person> persons = new List<Person>();
        public static int idCounter = 0;

        [HttpGet]
        [Route("api/Example/Person")]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ExampleController.persons);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e);
            }
        }

        [HttpGet]
        [Route("api/Example/Person/{id:int}")]
        public HttpResponseMessage GetName(int id)
        {
            if (this.exists(id))
                return Request.CreateResponse(HttpStatusCode.OK, ExampleController.persons[id]);
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
        }

        [HttpPost]
        [Route("api/Example/Person")]
        public HttpResponseMessage JsonStringBody([FromBody]Person person)
        {
            try
            {
                person.addId(ExampleController.idCounter);
                ExampleController.idCounter++;
                ExampleController.persons.Add(person);
                return Request.CreateResponse(HttpStatusCode.OK, "Person added");
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e);
            }
        }

        [HttpPut]
        [Route("api/Example/Person/{id:int}")]
        public HttpResponseMessage updateName(int id,[FromBody] string newName) {
            if (this.exists(id))
            {
                ExampleController.persons[id].changeName(newName);
                return Request.CreateResponse<Person>(HttpStatusCode.OK, ExampleController.persons[id]);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
        }

        [HttpDelete]
        [Route("api/Example/Person/{id:int}")]
        public HttpResponseMessage DeletePerson(int id)
        {
            if (this.exists(id))
            {
                Person tmp = ExampleController.persons[id];
                ExampleController.persons.RemoveAt(id);
                return Request.CreateResponse<Person>(HttpStatusCode.OK, tmp);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
            }
        }
        public bool exists(int id)
        {
            return (id >= 0 && id < ExampleController.persons.Count);
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
