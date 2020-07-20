using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Models.Common;

namespace Example.Models
{
    public class Person : IPerson
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

    public class Car : ICar
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
    public class PersonNoID : IPersonNoID
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int height { get; set; }
        public double weight { get; set; }

        public PersonNoID(string firstName, string lastName, int height, double weight)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.height = height;
            this.weight = weight;
        }
    }
    public class CarNoID : ICarNoID
    {
        public string name { get; set; }
        public double maxSpeed { get; set; }
        public string color { get; set; }
        public CarNoID(string name, double maxSpeed, string color)
        {
            this.name = name;
            this.maxSpeed = maxSpeed;
            this.color = color;
        }
    }
}
