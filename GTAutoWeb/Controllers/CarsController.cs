using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Index(string searchString, decimal? minPrice, decimal? maxPrice, int? minHP, int? maxHP)
        {
            var carsQuery = _context.Cars
                .Include(c => c.Model)
                .Include(c => c.Images)
                .AsQueryable();

            ViewData["CurrentSearch"] = searchString;
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;
            ViewData["MinHP"] = minHP;
            ViewData["MaxHP"] = maxHP;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                bool isCyrillic = Regex.IsMatch(searchString, @"\p{IsCyrillic}");
                if (isCyrillic)
                {
                    ViewData["ErrorMessage"] = "Invalid characters detected. Please use English only.";
                    return View(new List<Car>());
                }

                var search = searchString.Trim().ToLower();
                carsQuery = carsQuery.Where(c => c.Model.Name.ToLower().Contains(search));
            }

            if (minPrice.HasValue) carsQuery = carsQuery.Where(c => c.Price >= minPrice.Value);
            if (maxPrice.HasValue) carsQuery = carsQuery.Where(c => c.Price <= maxPrice.Value);
            if (minHP.HasValue) carsQuery = carsQuery.Where(c => c.HorsePower >= minHP.Value);
            if (maxHP.HasValue) carsQuery = carsQuery.Where(c => c.HorsePower <= maxHP.Value);

            var cars = await carsQuery
                .OrderByDescending(c => c.IsFlashOffer)
                .ThenByDescending(c => c.Year)
                .ToListAsync();

            return View(cars);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var car = await _context.Cars
                .Include(c => c.Model)
                .Include(c => c.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null) return NotFound();

            car.Images = car.Images.OrderBy(i => i.Order).ToList();

            return View(car);
        }

        // GET: Cars/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Models"] = new SelectList(_context.Models.OrderBy(m => m.Name), "Id", "Name");
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
                    IsReserved = false,
                    IsSold = false,
                    IsAutomatic = vm.IsAutomatic,
                    IsFlashOffer = vm.IsFlashOffer
                };

                if (vm.FrontImage != null)
                    car.Images.Add(new CarImage { Id = Guid.NewGuid(), ImagePath = await ProcessUploadedImage(vm.FrontImage), Order = 1 });

                if (vm.BackImage != null)
                    car.Images.Add(new CarImage { Id = Guid.NewGuid(), ImagePath = await ProcessUploadedImage(vm.BackImage), Order = 2 });

                if (vm.InteriorImage != null)
                    car.Images.Add(new CarImage { Id = Guid.NewGuid(), ImagePath = await ProcessUploadedImage(vm.InteriorImage), Order = 3 });

                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Models"] = new SelectList(_context.Models.OrderBy(m => m.Name), "Id", "Name", vm.ModelId);
            return View(vm);
        }

        // GET: Cars/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var car = await _context.Cars.Include(c => c.Images).FirstOrDefaultAsync(c => c.Id == id);
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
                IsReserved = car.IsReserved,
                IsSold = car.IsSold,
                IsAutomatic = car.IsAutomatic,
                IsFlashOffer = car.IsFlashOffer
            };

            ViewData["Models"] = new SelectList(_context.Models.OrderBy(m => m.Name), "Id", "Name", car.ModelId);
            return View(vm);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CarViewModel vm)
        {
            if (id != vm.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var car = await _context.Cars.Include(c => c.Images).FirstOrDefaultAsync(c => c.Id == id);
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
                    car.IsReserved = vm.IsReserved;
                    car.IsSold = vm.IsSold;
                    car.IsAutomatic = vm.IsAutomatic;
                    car.IsFlashOffer = vm.IsFlashOffer;

                    if (vm.FrontImage != null)
                    {
                        var oldImg = car.Images.FirstOrDefault(i => i.Order == 1);
                        if (oldImg != null) _context.CarImages.Remove(oldImg);
                        car.Images.Add(new CarImage { Id = Guid.NewGuid(), ImagePath = await ProcessUploadedImage(vm.FrontImage), Order = 1 });
                    }

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var car = await _context.Cars
                .Include(c => c.Model)
                .Include(c => c.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null) return NotFound();

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var car = await _context.Cars.Include(c => c.Images).FirstOrDefaultAsync(c => c.Id == id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> ProcessUploadedImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0) return null;

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "cars");

            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/cars/" + uniqueFileName;
        }

        private bool CarExists(Guid id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

        

        [Authorize]
        public async Task<IActionResult> Checkout(Guid id)
        {
            var car = await _context.Cars
                .Include(c => c.Model)
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null) return NotFound();

            if (car.IsReserved || car.IsSold)
            {
                TempData["ErrorMessage"] = "This asset is already reserved or sold.";
                return RedirectToAction("Details", new { id = car.Id });
            }

            ViewBag.DepositAmount = car.Price * 0.05m;
            return View(car);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckoutConfirm(Guid id, string cardNumber)
        {
            var car = await _context.Cars.Include(c => c.Model).FirstOrDefaultAsync(c => c.Id == id);
            if (car == null) return NotFound();

            // ==========================================
            // 🔥 АНТИ-ДУБЛИРАНЕ (RACE CONDITION PROTECTION) 🔥
            // Ако някой е платил преди 1 секунда, спираме текущия потребител!
            // ==========================================
            if (car.IsReserved || car.IsSold)
            {
                TempData["ErrorMessage"] = "ОПИТЪТ ОТХВЪРЛЕН: Този актив току-що беше капариран от друг клиент!";
                return RedirectToAction("Details", new { id = car.Id });
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CarId = id,
                DepositPaid = car.Price * 0.05m,
                ReservationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(30)
            };

            // АВТОМАТИЧНО ЗАКЛЮЧВАНЕ НА КОЛАТА
            car.IsReserved = true;

            _context.Reservations.Add(reservation);
            _context.Update(car);

            // Запазваме всичко в базата
            await _context.SaveChangesAsync();

            return RedirectToAction("Confirmation", new { id = reservation.Id });
        }

        [Authorize]
        public async Task<IActionResult> Confirmation(Guid id)
        {
            var res = await _context.Reservations
                .Include(r => r.Car)
                .ThenInclude(c => c.Model)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (res == null) return NotFound();

            return View(res);
        }
       

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReservedCars()
        {
            // Тук правим Join с таблицата Reservations, за да вземем датите
            var reservedAssets = await _context.Reservations
                .Include(r => r.Car)
                .ThenInclude(c => c.Model)
                .Include(r => r.Car.Images)
                .OrderBy(r => r.ExpiryDate) // Подреждаме ги по най-скоро изтичащите
                .ToListAsync();

            return View(reservedAssets);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelReservation(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            // 1. Намираме резервацията в базата и я трием (за да не се трупат "мъртви" резервации)
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.CarId == id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            // 2. Освобождаваме колата
            car.IsReserved = false;

            // Запазваме промените
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "ASSET UNLOCKED: Резервацията е премахната успешно!";
            return RedirectToAction(nameof(ReservedCars));
        }
    }
}