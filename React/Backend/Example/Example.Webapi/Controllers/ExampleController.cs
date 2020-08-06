using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Diagnostics;
using Example.Service;
using Example.Service.Common;
using Example.Models;
using System.Threading.Tasks;

namespace Example.Webapi.Controllers
{

    public class PersonController : ApiController
    {
        private IPersonService PersonServiceResolver { get; set; }
        public PersonController(IPersonService personService)
        {
            this.PersonServiceResolver = personService;
        }
        [HttpGet]
        [Route("api/Example/Person")]
        public async Task<HttpResponseMessage> Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.PersonServiceResolver.GetPeople());    
        }

        [HttpGet]
        [Route("api/Example/Person/{id:int}")]
        public async Task<HttpResponseMessage> GetName(int id)
        {
            var person = await this.PersonServiceResolver.GetPerson(id);
                if (person != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, person);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
                }
        }
        [HttpPost]
        [Route("api/Example/Person")]
        public async Task<HttpResponseMessage> AddPerson([FromBody]Person person)
        {
            var addedPerson = await this.PersonServiceResolver.AddPerson(person);
            if (addedPerson != null) { 
                return Request.CreateResponse(HttpStatusCode.OK, "Person added");
                }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Person not added");
            }
        }

        [HttpPut]
        [Route("api/Example/Person/{id:int}")]
        public async Task<HttpResponseMessage> UpdateName(int id,[FromBody] string newName) {
            var person = await this.PersonServiceResolver.UpdateName(id, newName);
            if (person!=null) { 
                    return Request.CreateResponse<String>(HttpStatusCode.OK, "Name updated");
            }
            else
            { 
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
            }
        }

        [HttpDelete]
        [Route("api/Example/Person/{id:int}")]
        public async Task<HttpResponseMessage> DeletePerson(int id)
        {
            var person = await this.PersonServiceResolver.DeletePerson(id);
            if (person!=null) { 
                    return Request.CreateResponse<String>(HttpStatusCode.OK, "Person deleted");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
                }
        }
        [HttpGet]
        [Route("api/Example/Person/Cars/{id:int}")]
        public async Task<HttpResponseMessage> GetCars(int id)
        {
            var cars = await this.PersonServiceResolver.GetPersonsCars(id);
                   if(cars!=null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, cars);
                    }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
                }
            }   
    }
}


    

