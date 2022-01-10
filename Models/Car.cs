using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dealership.Models
{
    public class Car
    {
        public int ID { get; set; }
        public string Marca { get; set; }
        public string Model { get; set; }
        public int An { get; set; }
        public double Motor { get; set; }
        public string Cutie { get; set; }
        public string Combustibil { get; set; }
        public int Pret { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<CarSuppliers> CarSuppliers { get; set; }

    }
}
