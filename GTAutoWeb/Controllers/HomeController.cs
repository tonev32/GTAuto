using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GTAuto.Data;
using GTAutoWeb.ViewModel;
using GTAutoWeb.Models; // Увери се, че това е твоят namespace за ErrorViewModel

namespace GTAutoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly GTAutoDbContext _context;

        public HomeController(GTAutoDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var featuredCars = await _context.Cars
                .Include(c => c.Model)
                .Take(3)
                .ToListAsync();

            return View(featuredCars);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}