using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTAuto.Data; 
using GTAuto.Data.Models;
using GTAutoWeb.ViewModel;

namespace GTAutoWeb.Controllers
{
    public class CarsController : Controller
    {
        private readonly GTAutoDbContext _context;

        public CarsController(GTAutoDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars
                .Include(c => c.Model)
                .ToListAsync();
            return View(cars);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var car = await _context.Cars
                .Include(c => c.Model)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null) return NotFound();

            return View(car);
        }

        // GET: Cars/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Models"] = new SelectList(_context.Models.OrderBy(m => m.Name), "Id", "Name");
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var car = new Car
                {
                    Id = Guid.NewGuid(),
                    ModelId = vm.ModelId,
                    Year = vm.Year,
                    HorsePower = vm.HorsePower,
                    Price = vm.Price,
                    Mileage = vm.Mileage,
                    FuelType = vm.FuelType,
                    Transmission = vm.Transmission,
                    Color = vm.Color,
                    Description = vm.Description,
                    ImageUrl = vm.ImageUrl,
                    IsReserved = vm.IsReserved,
                    IsSold = vm.IsSold,
                    IsAutomatic = vm.IsAutomatic
                };

                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Models"] = new SelectList(_context.Models.OrderBy(m => m.Name), "Id", "Name", vm.ModelId);
            return View(vm);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            var vm = new CarViewModel
            {
                Id = car.Id,
                ModelId = car.ModelId,
                Year = car.Year,
                HorsePower = car.HorsePower,
                Price = car.Price,
                Mileage = car.Mileage,
                FuelType = car.FuelType,
                Transmission = car.Transmission,
                Color = car.Color,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                IsReserved = car.IsReserved,
                IsSold = car.IsSold,
                IsAutomatic = car.IsAutomatic
            };

            ViewData["Models"] = new SelectList(_context.Models.OrderBy(m => m.Name), "Id", "Name", car.ModelId);
            return View(vm);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CarViewModel vm)
        {
            if (id != vm.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var car = await _context.Cars.FindAsync(id);
                    if (car == null) return NotFound();

                    car.ModelId = vm.ModelId;
                    car.Year = vm.Year;
                    car.HorsePower = vm.HorsePower;
                    car.Price = vm.Price;
                    car.Mileage = vm.Mileage;
                    car.FuelType = vm.FuelType;
                    car.Transmission = vm.Transmission;
                    car.Color = vm.Color;
                    car.Description = vm.Description;
                    car.ImageUrl = vm.ImageUrl;
                    car.IsReserved = vm.IsReserved;
                    car.IsSold = vm.IsSold;
                    car.IsAutomatic = vm.IsAutomatic;

                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(vm.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Models"] = new SelectList(_context.Models.OrderBy(m => m.Name), "Id", "Name", vm.ModelId);
            return View(vm);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var car = await _context.Cars
                .Include(c => c.Model)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null) return NotFound();

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(Guid id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}