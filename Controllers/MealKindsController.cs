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
    public class MealKindsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MealKindsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MealKinds
        public async Task<IActionResult> Index()
        {
            return View(await _context.MealKind.ToListAsync());
        }

        // GET: MealKinds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealKind = await _context.MealKind
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mealKind == null)
            {
                return NotFound();
            }

            return View(mealKind);
        }

        // GET: MealKinds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MealKinds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nazov")] MealKind mealKind)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mealKind);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mealKind);
        }

        // GET: MealKinds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealKind = await _context.MealKind.FindAsync(id);
            if (mealKind == null)
            {
                return NotFound();
            }
            return View(mealKind);
        }

        // POST: MealKinds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nazov")] MealKind mealKind)
        {
            if (id != mealKind.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mealKind);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealKindExists(mealKind.ID))
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
            return View(mealKind);
        }

        // GET: MealKinds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealKind = await _context.MealKind
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mealKind == null)
            {
                return NotFound();
            }

            return View(mealKind);
        }

        // POST: MealKinds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mealKind = await _context.MealKind.FindAsync(id);
            _context.MealKind.Remove(mealKind);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealKindExists(int id)
        {
            return _context.MealKind.Any(e => e.ID == id);
        }
    }
}
