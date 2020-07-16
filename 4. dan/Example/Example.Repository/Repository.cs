using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Repository.Common;
using Example.Models;
using System.Data.SqlClient;

namespace Example.Repository
{
    public class Repository : IRepository
    {
        private static string connetionString = "Data Source=(localdb)\\MSSQLLocalDB;";

        public List<Person> GetPeople()
        {
            using (var cnn = new SqlConnection(Repository.connetionString))
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
                    }
                    reader.Close();
                }
                return persons;
            }
        }
        public Person GetPerson(int id)
        {
            using (var cnn = new SqlConnection(Repository.connetionString))
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
                    return personToReturn;
                }
                else
                {
                    cnn.Close();
                    return null;
                }
            }
        }
    public Person AddPerson(Person person)
        {
            try
            {
                using (var cnn = new SqlConnection(Repository.connetionString))
                {
                    String query = "INSERT INTO Person (FirstName,LastName,Height,Weight) VALUES (@FirstName,@LastName, @Height,@Weight)";
                    cnn.Open();
                    SqlCommand command = new SqlCommand(query, cnn);
                    command.Parameters.AddWithValue("@FirstName", person.firstName);
                    command.Parameters.AddWithValue("@LastName", person.lastName);
                    command.Parameters.AddWithValue("@Height", person.height);
                    command.Parameters.AddWithValue("@Weight", person.weight);

                    command.ExecuteNonQuery();

                }
                return person;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Person UpdateName(int id, string newName)
        {
            if (this.Exists(id))
            {
                using (var cnn = new SqlConnection(Repository.connetionString))
                {
                    String query = "UPDATE Person SET FirstName = @name WHERE ID=@ID;";
                    cnn.Open();
                    SqlCommand command = new SqlCommand(query, cnn);
                    command.Parameters.AddWithValue("@name", newName);
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                    return this.GetPerson(id);
                }
            }
            else
            {
                return null;
            }
        }
        public Person DeletePerson(int id)
        {
            using (var cnn = new SqlConnection(Repository.connetionString))
            {
                if (this.Exists(id))
                {
                    String query = "DELETE FROM Person WHERE ID=@ID;";
                    cnn.Open();
                    var person = this.GetPerson(id);
                    SqlCommand command = new SqlCommand(query, cnn);
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                    return person;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<Car> GetPersonsCars(int id)
        {
            if (this.Exists(id)) {
                using (var cnn = new SqlConnection(Repository.connetionString))
                {
                    SqlCommand command = new SqlCommand("SELECT Car.ID,Name,MaxSpeed,Color FROM Person RIGHT JOIN PersonCar ON Person.ID = PersonCar.PersonID RIGHT JOIN Car ON PersonCar.CarID = Car.ID WHERE Person.ID = @id;", cnn);
                    command.Parameters.AddWithValue("@ID", id);
                    var cars = new List<Car>();
                    cnn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Car carToAdd = new Car(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetString(3));
                        cars.Add(carToAdd);
                    }
                    reader.Close();
                    return cars;
                }
            }
            else
            {
                return null;
            }
            
        }

        public bool Exists(int id)
        {
            using (var cnn = new SqlConnection(Repository.connetionString))
            {
                SqlCommand command = new SqlCommand("SELECT ID, FirstName, LastName,Height,Weight FROM Person WHERE ID=@ID;", cnn);
                command.Parameters.AddWithValue("@ID", id);
                cnn.Open();
                SqlDataReader reader = command.ExecuteReader();
                return reader.HasRows;
            }
        }
    }
}
