using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dealership.Data;
using Dealership.Models;
using Dealership.Models.DealershipViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Dealership.Controllers
{
    [Authorize(Policy = "DoarAdministratie")]
    public class SuppliersController : Controller
    {
        private readonly DealershipContext _context;

        public SuppliersController(DealershipContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index(int? id, int? CarID)
        {
            var viewModel = new SupplierIndexData();
            viewModel.Suppliers = await _context.Suppliers
                .Include(i => i.CarSuppliers)
                .ThenInclude(i => i.Car)
                .ThenInclude(i => i.Orders)
                .ThenInclude(i => i.Customer)
                .AsNoTracking()
                .OrderBy(i => i.SupplierName)
                .ToListAsync();
            if (id != null)
            {
                ViewData["SupplierID"] = id.Value;
                Supplier supplier = viewModel.Suppliers.Where(
                i => i.ID == id.Value).Single();
                viewModel.Cars = supplier.CarSuppliers.Select(s => s.Car);
            }
            if (CarID != null)
            {
                ViewData["CarID"] = CarID.Value;
                viewModel.Orders = viewModel.Cars.Where(
                x => x.ID == CarID).Single().Orders;
            }
            return View(viewModel);
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SupplierName,Adress")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
            .Include(i => i.CarSuppliers).ThenInclude(i => i.Car)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (supplier == null)
            {
                return NotFound();
            }
            PopulateCarSuppliersData(supplier);
            return View(supplier);
        }
        private void PopulateCarSuppliersData(Supplier supplier)
        {
            var allCars = _context.Cars;
            var supplierCars = new HashSet<int>(supplier.CarSuppliers.Select(c => c.CarID));
            var viewModel = new List<CarSuppliersData>();
            foreach (var car in allCars)
            {
                viewModel.Add(new CarSuppliersData
                {
                    CarID = car.ID,
                    Model = car.Model,
                    IsSupplied = supplierCars.Contains(car.ID)
                });
            }
            ViewData["Cars"] = viewModel;
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedCars)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierToUpdate = await _context.Suppliers
                .Include(i => i.CarSuppliers)
                .ThenInclude(i => i.Car)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Supplier>(supplierToUpdate,"",i => i.SupplierName, i => i.Adress))
            {
                UpdateCarSuppliers(selectedCars, supplierToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateCarSuppliers(selectedCars, supplierToUpdate);
            PopulateCarSuppliersData(supplierToUpdate);
            return View(supplierToUpdate);
        }
        private void UpdateCarSuppliers(string[] selectedCars, Supplier supplierToUpdate)
        {
            if (selectedCars == null)
            {
                supplierToUpdate.CarSuppliers = new List<CarSuppliers>();
                return;
            }
            var selectedCarsHS = new HashSet<string>(selectedCars);
            var publishedCars = new HashSet<int>
            (supplierToUpdate.CarSuppliers.Select(c => c.Car.ID));
            foreach (var car in _context.Cars)
            {
                if (selectedCarsHS.Contains(car.ID.ToString()))
                {
                    if (!publishedCars.Contains(car.ID))
                    {
                        supplierToUpdate.CarSuppliers.Add(new CarSuppliers
                        {
                            SupplierID =supplierToUpdate.ID,
                            CarID = car.ID
                        });
                    }
                }
                else
                {
                    if (publishedCars.Contains(car.ID))
                    {
                        CarSuppliers carToRemove = supplierToUpdate.CarSuppliers.FirstOrDefault(i
                       => i.CarID == car.ID);
                        _context.Remove(carToRemove);
                    }
                }
            }
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.ID == id);
        }
    }
}
