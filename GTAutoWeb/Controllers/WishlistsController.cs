using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GTAuto.Data;
using GTAuto.Data.Models;

namespace GTAutoWeb.Controllers
{
    [Authorize] // Само логнати потребители могат да имат любими коли
    public class WishlistController : Controller
    {
        private readonly GTAutoDbContext _context;

        public WishlistController(GTAutoDbContext context)
        {
            _context = context;
        }

        // ПОКАЗВА ЛЮБИМИТЕ КОЛИ
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

        // ДОБАВЯНЕ В ЛЮБИМИ
        [HttpPost]
        public async Task<IActionResult> Add(Guid carId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Проверяваме дали вече я няма
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

                TempData["SuccessMessage"] = "Asset added to your garage!";
            }

            // Връщаме потребителя там, откъдето е кликнал
            string referer = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(referer) ? Redirect(referer) : RedirectToAction("Index", "Cars");
        }

        // ПРЕМАХВАНЕ ОТ ЛЮБИМИ
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