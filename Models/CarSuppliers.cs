using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Models
{
    public class CarSuppliers
    {
        public int SupplierID { get; set; }
        public int CarID { get; set; }
        public Supplier Supplier { get; set; }
        public Car Car { get; set; }
    }
}
