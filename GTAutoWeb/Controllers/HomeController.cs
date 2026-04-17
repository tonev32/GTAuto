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

            var flashOffers = await _context.Cars
    .Include(c => c.Model)
    .Include(c => c.Images)
    .Where(c => c.IsFlashOffer == true && c.IsSold == false && c.IsReserved == false)
    .ToListAsync();

            return View(flashOffers);
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