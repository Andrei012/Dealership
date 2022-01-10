using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dealership.Models;

namespace Dealership.Data
{
    public class DbInitializer
    {
        public static void Initialize(DealershipContext context)
        {
            context.Database.EnsureCreated();
            if (context.Cars.Any())
            {
                return; // BD a fost creata anterior
            }

            var cars = new Car[]
            {
                new Car{Marca="Ford", Model="Fiesta", An=2009, Motor=1.6, Cutie="Manuala", Combustibil="Motorina", Pret=30000},
                new Car{Marca="Ford", Model="Focus", An=2008, Motor=1.6, Cutie="Manuala", Combustibil="Motorina", Pret=30000},
                new Car{Marca="Dacia", Model="Logdan", An=2015, Motor=1.2, Cutie="Manuala", Combustibil="Benzina", Pret=20000},
                new Car{Marca="Audi", Model="A4", An=2015, Motor=2.0, Cutie="Automata", Combustibil="Motorina", Pret=40000},
            };
            foreach (Car c in cars)
            {
                context.Cars.Add(c);
            }
            context.SaveChanges();

            var customers = new Customer[]
            {
                new Customer{CustomerID=1050,Name="Andrei Denis",Adress="Cluj Napoca, Traian Vuia 55", BirthDate=DateTime.Parse("1999-05-03"), Phone="0746922610"},
                new Customer{CustomerID=1045,Name="Mihailescu Cornel",Adress="Cluj Napoca, Dorobantilor 44", BirthDate=DateTime.Parse("1969-07-08"), Phone="0746124578"},
                new Customer{CustomerID=1060,Name="Broscoiu Marius",Adress="Cluj Napoca, Dorobantilor 67", BirthDate=DateTime.Parse("1979-08-09"), Phone="0746124578"},
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();

            var orders = new Order[]
            {
                new Order{CarID=1,CustomerID=1050},
                new Order{CarID=3,CustomerID=1045},
                new Order{CarID=1,CustomerID=1045},
                new Order{CarID=2,CustomerID=1060},
            };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();

            var suppliers = new Supplier[]
            {
                new Supplier{SupplierName="Ford", Adress="Romania, Strada Henry Ford 29, Craiova"},
                new Supplier{SupplierName="Dacia", Adress="Romania, Strada Uzinei 1, Mioveni"},
                new Supplier{SupplierName="Audi", Adress="Germania, Zwickau"},
            };
            foreach (Supplier s in suppliers)
            {
                context.Suppliers.Add(s);
            }
            context.SaveChanges();

            var carsuppliers = new CarSuppliers[]
            {
                new CarSuppliers{CarID=cars.Single(c => c.Marca == "Ford").ID, SupplierID = suppliers.Single(i => i.SupplierName == "Ford").ID},
                new CarSuppliers{CarID=cars.Single(c => c.Marca == "Dacia").ID, SupplierID = suppliers.Single(i => i.SupplierName == "Dacia").ID},
                new CarSuppliers{CarID=cars.Single(c => c.Marca == "Audi").ID, SupplierID = suppliers.Single(i => i.SupplierName == "Audi").ID},
            };
            foreach (CarSuppliers pb in carsuppliers)
            {
                context.CarSuppliers.Add(pb);
            }
            context.SaveChanges();
        }
    }
}
