using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Diagnostics;
using Example.Service;
using Example.Models;

namespace Example.Webapi.Controllers
{
    public class ExampleController : ApiController
    {
        public Service.Service service = new Service.Service();
       [HttpGet]
        [Route("api/Example/Person")]
        public HttpResponseMessage Get()
        {
           return Request.CreateResponse(HttpStatusCode.OK, this.service.GetPeople());    
        }

        [HttpGet]
        [Route("api/Example/Person/{id:int}")]
        public HttpResponseMessage GetName(int id)
        {
            var person = this.service.GetPerson(id);
            if (person != null) { 
                    return Request.CreateResponse(HttpStatusCode.OK, person);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
                }
    
        }
        [HttpPost]
        [Route("api/Example/Person")]
        public HttpResponseMessage AddPerson([FromBody]Person person)
        {
            var addedPerson = this.service.AddPerson(person);
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
        public HttpResponseMessage UpdateName(int id,[FromBody] string newName) {
            var person = this.service.UpdateName(id, newName);
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
        public HttpResponseMessage DeletePerson(int id)
        {
            var person = this.service.DeletePerson(id);
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
        public HttpResponseMessage GetCars(int id)
        {
            var cars = this.service.GetPersonsCars(id);
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


    

