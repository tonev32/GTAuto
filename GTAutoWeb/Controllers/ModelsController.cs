using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTAuto.Data.Models;
using GTAutoWeb.ViewModel;
using GTAuto.Data;

namespace GTAutoWeb.Controllers
{
    public class ModelsController : Controller
    {
        private readonly GTAutoDbContext _context;

        public ModelsController(GTAutoDbContext context)
        {
            _context = context;
        }

        // GET: Models
        public async Task<IActionResult> Index()
        {
            return View(await _context.Models.ToListAsync());
        }

        // GET: Models/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Models/Create
        [HttpGet]
        public IActionResult Create()
        {
            var brand = _context.Brands.FirstOrDefault();

            if (brand == null)
            {
                brand = new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "General"
                };
                _context.Brands.Add(brand);
                _context.SaveChanges();
            }

            ViewBag.BrandId = brand.Id;
            return View();
        }

        // POST: Models/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModelViewModel model)
        {
           
            ModelState.Remove("Brand");
            ModelState.Remove("Cars");
            ModelState.Remove("BrandId"); 

            if (ModelState.IsValid)
            {
                
                var defaultBrand = await _context.Brands.FirstOrDefaultAsync();
                if (defaultBrand == null)
                {
                    defaultBrand = new Brand { Id = Guid.NewGuid(), Name = "General" };
                    _context.Brands.Add(defaultBrand);
                    await _context.SaveChangesAsync(); 
                }

               
                Model newModel = new Model
                {
                    Id = Guid.NewGuid(),
                    BrandId = defaultBrand.Id, 
                    Name = model.Name 
                };

                _context.Models.Add(newModel);
                await _context.SaveChangesAsync(); 

                
                return RedirectToAction(nameof(Index));
            }

            
            return View(model);
        }

        // GET: Models/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Models/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Model model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Brand");
            ModelState.Remove("Cars");

            if (ModelState.IsValid)
            {
                try
                {
                    
                    var existingDbModel = await _context.Models.FindAsync(id);

                    if (existingDbModel == null)
                    {
                        return NotFound();
                    }

                    existingDbModel.Name = model.Name;

                    _context.Update(existingDbModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelExists(model.Id))
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
            return View(model);
        }

        // GET: Models/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model != null)
            {
                _context.Models.Remove(model);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelExists(Guid id)
        {
            return _context.Models.Any(e => e.Id == id);
        }
    }
}