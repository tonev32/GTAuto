using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GTAuto.Data;
using GTAuto.Data.Models;

namespace GTAutoWeb.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly GTAutoDbContext _context;

        public WishlistController(GTAutoDbContext context)
        {
            _context = context;
        }

        // 1. ПОКАЗВА ЛЮБИМИТЕ КОЛИ (GARAGE)
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favorites = await _context.WishlistCars
                .Where(w => w.UserId == userId)
                .Include(w => w.Car)
                    .ThenInclude(c => c.Model)
                .Include(w => w.Car.Images)
                .ToListAsync();

            return View(favorites);
        }

        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Взимаме само колите, които текущият клиент е капарирал
            var myOrders = await _context.Cars
                .Where(c => c.IsReserved && c.ReservedByUserId == userId)
                .Include(c => c.Model)
                .Include(c => c.Images)
                .ToListAsync();

            return View(myOrders);
        }

        // 3. МЕТОД ЗА ПОКУПКА (BUY) - Добави го тук
        [HttpPost]
        public async Task<IActionResult> Buy(Guid carId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);

            if (car != null && !car.IsReserved)
            {
                car.IsReserved = true;
                car.ReservedByUserId = userId; // Записваме ID-то на купувача

                _context.Cars.Update(car);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Asset secured! Check your orders.";
            }

            return RedirectToAction("MyOrders");
        }

        // 4. ДОБАВЯНЕ В ЛЮБИМИ
        [HttpPost]
        public async Task<IActionResult> Add(Guid carId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exists = await _context.WishlistCars.AnyAsync(w => w.UserId == userId && w.CarId == carId);

            if (!exists)
            {
                var entry = new WishlistCar
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    CarId = carId
                };
                _context.WishlistCars.Add(entry);
                await _context.SaveChangesAsync();
            }

            string referer = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(referer) ? Redirect(referer) : RedirectToAction("Index", "Cars");
        }

        // 5. ПРЕМАХВАНЕ ОТ ЛЮБИМИ
        [HttpPost]
        public async Task<IActionResult> Remove(Guid carId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var entry = await _context.WishlistCars.FirstOrDefaultAsync(w => w.UserId == userId && w.CarId == carId);

            if (entry != null)
            {
                _context.WishlistCars.Remove(entry);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}