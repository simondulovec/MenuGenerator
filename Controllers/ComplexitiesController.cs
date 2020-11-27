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
    public class ComplexitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplexitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Complexities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Complexity.ToListAsync());
        }

        // GET: Complexities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complexity = await _context.Complexity
                .FirstOrDefaultAsync(m => m.ID == id);
            if (complexity == null)
            {
                return NotFound();
            }

            return View(complexity);
        }

        // GET: Complexities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Complexities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nazov")] Complexity complexity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complexity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complexity);
        }

        // GET: Complexities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complexity = await _context.Complexity.FindAsync(id);
            if (complexity == null)
            {
                return NotFound();
            }
            return View(complexity);
        }

        // POST: Complexities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nazov")] Complexity complexity)
        {
            if (id != complexity.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complexity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplexityExists(complexity.ID))
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
            return View(complexity);
        }

        // GET: Complexities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complexity = await _context.Complexity
                .FirstOrDefaultAsync(m => m.ID == id);
            if (complexity == null)
            {
                return NotFound();
            }

            return View(complexity);
        }

        // POST: Complexities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complexity = await _context.Complexity.FindAsync(id);
            _context.Complexity.Remove(complexity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplexityExists(int id)
        {
            return _context.Complexity.Any(e => e.ID == id);
        }
    }
}
