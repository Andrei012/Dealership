using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Models.DealershipViewModel
{
    public class SupplierIndexData
    {
        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
