using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTAuto.Data.Models;

namespace GTAutoWeb.Controllers
{
    public class FuelsController : Controller
    {
        private readonly GTAutoDbContext _context;

        public FuelsController(GTAutoDbContext context)
        {
            _context = context;
        }

        // GET: Fuels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fuels.ToListAsync());
        }

        // GET: Fuels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuel = await _context.Fuels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fuel == null)
            {
                return NotFound();
            }

            return View(fuel);
        }

        // GET: Fuels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fuels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,FuelConsumption")] Fuel fuel)
        {
            if (ModelState.IsValid)
            {
                fuel.Id = Guid.NewGuid();
                _context.Add(fuel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fuel);
        }

        // GET: Fuels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuel = await _context.Fuels.FindAsync(id);
            if (fuel == null)
            {
                return NotFound();
            }
            return View(fuel);
        }

        // POST: Fuels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Type,FuelConsumption")] Fuel fuel)
        {
            if (id != fuel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fuel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuelExists(fuel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fuel);
        }

        // GET: Fuels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuel = await _context.Fuels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fuel == null)
            {
                return NotFound();
            }

            return View(fuel);
        }

        // POST: Fuels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fuel = await _context.Fuels.FindAsync(id);
            if (fuel != null)
            {
                _context.Fuels.Remove(fuel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuelExists(Guid id)
        {
            return _context.Fuels.Any(e => e.Id == id);
        }
    }
}
