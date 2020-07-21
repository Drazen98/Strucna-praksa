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
    public class PersonRepository : IPersonRepository
    {
        private static string connetionString = "Data Source=(localdb)\\MSSQLLocalDB;";

        public async Task<List<Person>> GetPeople()
        {
            using (var cnn = new SqlConnection(PersonRepository.connetionString))
            {
                var persons = new List<Person>();
                SqlCommand command = new SqlCommand(
                  "SELECT ID, FirstName, LastName,Height,Weight FROM Person;",
                  cnn);
                cnn.Open();
                SqlDataReader reader = await command.ExecuteReaderAsync();

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
        public async Task<Person> GetPerson(int id)
        {
            using (var cnn = new SqlConnection(PersonRepository.connetionString))
            {
                SqlCommand command = new SqlCommand("SELECT ID, FirstName, LastName,Height,Weight FROM Person WHERE ID=@ID;", cnn);
                command.Parameters.AddWithValue("@ID", id);
                cnn.Open();
                SqlDataReader reader = await command.ExecuteReaderAsync();
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
    public async Task<Person> AddPerson(Person person)
        {
            try
            {
                using (var cnn = new SqlConnection(PersonRepository.connetionString))
                {
                    String query = "INSERT INTO Person (FirstName,LastName,Height,Weight) VALUES (@FirstName,@LastName, @Height,@Weight)";
                    cnn.Open();
                    SqlCommand command = new SqlCommand(query, cnn);
                    command.Parameters.AddWithValue("@FirstName", person.firstName);
                    command.Parameters.AddWithValue("@LastName", person.lastName);
                    command.Parameters.AddWithValue("@Height", person.height);
                    command.Parameters.AddWithValue("@Weight", person.weight);

                    await command.ExecuteNonQueryAsync();

                }
                return person;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<Person> UpdateName(int id, string newName)
        {
            if (this.Exists(id))
            {
                using (var cnn = new SqlConnection(PersonRepository.connetionString))
                {
                    String query = "UPDATE Person SET FirstName = @name WHERE ID=@ID;";
                    cnn.Open();
                    SqlCommand command = new SqlCommand(query, cnn);
                    command.Parameters.AddWithValue("@name", newName);
                    command.Parameters.AddWithValue("@ID", id);
                    await command.ExecuteNonQueryAsync();
                    return await this.GetPerson(id);
                }
            }
            else
            {
                return null;
            }
        }
        public async Task<Person> DeletePerson(int id)
        {
            using (var cnn = new SqlConnection(PersonRepository.connetionString))
            {
                if (this.Exists(id))
                {
                    String query = "DELETE FROM Person WHERE ID=@ID;";
                    cnn.Open();
                    var person = await this.GetPerson(id);
                    SqlCommand command = new SqlCommand(query, cnn);
                    command.Parameters.AddWithValue("@ID", id);
                    await command.ExecuteNonQueryAsync();
                    return person;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<List<Car>> GetPersonsCars(int id)
        {
            if (this.Exists(id)) {
                using (var cnn = new SqlConnection(PersonRepository.connetionString))
                {
                    SqlCommand command = new SqlCommand("SELECT Car.ID,Name,MaxSpeed,Color FROM Person RIGHT JOIN PersonCar ON Person.ID = PersonCar.PersonID RIGHT JOIN Car ON PersonCar.CarID = Car.ID WHERE Person.ID = @id;", cnn);
                    command.Parameters.AddWithValue("@ID", id);
                    var cars = new List<Car>();
                    cnn.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
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
            using (var cnn = new SqlConnection(PersonRepository.connetionString))
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
