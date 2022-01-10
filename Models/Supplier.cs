using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Models
{
    public class Supplier
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Supplier Name")]
        [StringLength(50)]
        public string SupplierName { get; set; }
        [StringLength(70)]
        public string Adress { get; set; }
        public ICollection<CarSuppliers> CarSuppliers { get; set; }

    }
}
