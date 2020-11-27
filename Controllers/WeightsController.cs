using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MenuGenerator.Data;
using MenuGenerator.Models;
using Microsoft.AspNetCore.Authorization;

namespace MenuGenerator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WeightsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Weights
        public async Task<IActionResult> Index()
        {
            return View(await _context.Weight.ToListAsync());
        }

        // GET: Weights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weight = await _context.Weight
                .FirstOrDefaultAsync(m => m.ID == id);
            if (weight == null)
            {
                return NotFound();
            }

            return View(weight);
        }

        // GET: Weights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Weights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Hmotnost")] Weight weight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weight);
        }

        // GET: Weights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weight = await _context.Weight.FindAsync(id);
            if (weight == null)
            {
                return NotFound();
            }
            return View(weight);
        }

        // POST: Weights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Hmotnost")] Weight weight)
        {
            if (id != weight.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeightExists(weight.ID))
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
            return View(weight);
        }

        // GET: Weights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weight = await _context.Weight
                .FirstOrDefaultAsync(m => m.ID == id);
            if (weight == null)
            {
                return NotFound();
            }

            return View(weight);
        }

        // POST: Weights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weight = await _context.Weight.FindAsync(id);
            _context.Weight.Remove(weight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeightExists(int id)
        {
            return _context.Weight.Any(e => e.ID == id);
        }
    }
}
