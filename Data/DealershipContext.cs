using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dealership.Models;

namespace Dealership.Data
{
    public class DealershipContext: DbContext
    {
        public DealershipContext(DbContextOptions<DealershipContext> options):base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<CarSuppliers> CarSuppliers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Car>().ToTable("Car");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<CarSuppliers>().ToTable("CarSuppliers");

            modelBuilder.Entity<CarSuppliers>().HasKey(c => new { c.CarID, c.SupplierID });
        }
    }
}
