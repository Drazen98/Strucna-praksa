using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Example.Webapi.Controllers
{
    public class ExampleController : ApiController
    {
        public static List<Person> persons = new List<Person>();
        public static int idCounter = 0;
       
        private static string connetionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Praksa 3;Integrated Security=True";
        
       [HttpGet]
        [Route("api/Example/Person")]
        public HttpResponseMessage Get()
        {
            using (var cnn = new SqlConnection(ExampleController.connetionString))
            {
                var persons = new List<Person>();
                SqlCommand command = new SqlCommand(
                  "SELECT ID, FirstName, LastName,Height,Weight FROM Person;",
                  cnn);
                cnn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Person personToAdd = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDouble(4));
                        persons.Add(personToAdd);
                        Debug.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}",reader.GetInt32(0), reader.GetString(1),
                            reader.GetString(2),reader.GetInt32(3),reader.GetDouble(4));
                    }
                    reader.Close();
                }
                try
                {
                    return Request.CreateResponse(HttpStatusCode.OK,persons);
                }
                catch (Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e);
                }   
             
            }
            
        }

        [HttpGet]
        [Route("api/Example/Person/{id:int}")]
        public HttpResponseMessage GetName(int id)
        {
            using (var cnn = new SqlConnection(ExampleController.connetionString))
            {
                SqlCommand command = new SqlCommand("SELECT ID, FirstName, LastName,Height,Weight FROM Person WHERE ID=@ID;", cnn);
                command.Parameters.AddWithValue("@ID", id);
                cnn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Person personToReturn = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDouble(4));
                    cnn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, personToReturn);
                }
                else
                {
                    cnn.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
                }
            }
        }

        [HttpPost]
        [Route("api/Example/Person")]
        public HttpResponseMessage JsonStringBody([FromBody]Person person)
        {
            try
            {
                using (var cnn = new SqlConnection(ExampleController.connetionString)) {
                    String query = "INSERT INTO Person (FirstName,LastName,Height,Weight) VALUES (@FirstName,@LastName, @Height,@Weight)";
                    cnn.Open();
                    SqlCommand command = new SqlCommand(query, cnn);
                    command.Parameters.AddWithValue("@FirstName", person.firstName);
                    command.Parameters.AddWithValue("@LastName", person.lastName);
                    command.Parameters.AddWithValue("@Height", person.height);
                    command.Parameters.AddWithValue("@Weight", person.weight);

                    command.ExecuteNonQuery();

                        }
                return Request.CreateResponse(HttpStatusCode.OK, "Person added");
                }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e);
            }
        }

        [HttpPut]
        [Route("api/Example/Person/{id:int}")]
        public HttpResponseMessage UpdateName(int id,[FromBody] string newName) {
            if (this.Exists(id))
            {
                using (var cnn = new SqlConnection(ExampleController.connetionString))
                {
                    String query = "UPDATE Person SET FirstName = @name WHERE ID=@ID;";
                    cnn.Open();
                    SqlCommand command = new SqlCommand(query, cnn);
                    command.Parameters.AddWithValue("@name", newName);
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                    return Request.CreateResponse<String>(HttpStatusCode.OK, "Name updated");
                }
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
            using (var cnn = new SqlConnection(ExampleController.connetionString))
            {
                if (this.Exists(id))
                {
                    String query = "DELETE FROM Person WHERE ID=@ID;";
                    cnn.Open();
                    SqlCommand command = new SqlCommand(query, cnn);
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                    return Request.CreateResponse<String>(HttpStatusCode.OK, "Person deleted");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
                }
            }
        }
        [HttpGet]
        [Route("api/Example/Person/Cars/{id:int}")]
        public HttpResponseMessage GetCars(int id)
        {
            using (var cnn = new SqlConnection(ExampleController.connetionString))
            {
                SqlCommand command = new SqlCommand("SELECT Car.ID,Name,MaxSpeed,Color FROM Person RIGHT JOIN PersonCar ON Person.ID = PersonCar.PersonID RIGHT JOIN Car ON PersonCar.CarID = Car.ID WHERE Person.ID = @id;", cnn);
                command.Parameters.AddWithValue("@ID", id);
                var cars = new List<Car>();
                cnn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Car carToAdd = new Car(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetString(3));
                        cars.Add(carToAdd);
                    }
                    reader.Close();
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, cars);
                    }
                    catch (Exception e)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e);
                    }
                }
                else
                {
                    cnn.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person Not Found");
                }
            }
        }

        public bool Exists(int id)
        {
            using (var cnn = new SqlConnection(ExampleController.connetionString))
            {
                SqlCommand command = new SqlCommand("SELECT ID, FirstName, LastName,Height,Weight FROM Person WHERE ID=@ID;", cnn);
                command.Parameters.AddWithValue("@ID", id);
                cnn.Open();
                SqlDataReader reader = command.ExecuteReader();
                return reader.HasRows;
            }
        }
        
    }

    public class Person
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int height { get; set; }
        public double weight { get; set; }
        public int personId { get; set; }

        public Person(int id, string firstName, string lastName, int height, double weight)
        {
            this.personId = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.height = height;
            this.weight = weight;
        }
    }
    public class Car
    {
        public int carID { get; set; }
        public string name { get; set; }
        public double maxSpeed { get; set; }
        public string color { get; set; }
        public Car(int ID, string name, double maxSpeed, string color)
        {
            this.carID = ID;
            this.name = name;
            this.maxSpeed = maxSpeed;
            this.color = color;
        }
    }
}


    

