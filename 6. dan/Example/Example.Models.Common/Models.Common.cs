using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Models.Common
{
        public interface IPerson
    {
        string firstName { get; set; }
        string lastName { get; set; }
        int height { get; set; }
        double weight { get; set; }
        int personId { get; set; }
    }
    public interface ICar
    {
        int carID { get; set; }
        string name { get; set; }
        double maxSpeed { get; set; }
        string color { get; set; }
    }
    public interface IPersonNoID
    {
        string firstName { get; set; }
        string lastName { get; set; }
        int height { get; set; }
        double weight { get; set; }
    }
    public interface ICarNoID
    { 
        string name { get; set; }
        double maxSpeed { get; set; }
        string color { get; set; }
    }
}
