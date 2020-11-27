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
    public class SoupKindsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SoupKindsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SoupKinds
        public async Task<IActionResult> Index()
        {
            return View(await _context.SoupKind.ToListAsync());
        }

        // GET: SoupKinds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soupKind = await _context.SoupKind
                .FirstOrDefaultAsync(m => m.ID == id);
            if (soupKind == null)
            {
                return NotFound();
            }

            return View(soupKind);
        }

        // GET: SoupKinds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SoupKinds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nazov")] SoupKind soupKind)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soupKind);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(soupKind);
        }

        // GET: SoupKinds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soupKind = await _context.SoupKind.FindAsync(id);
            if (soupKind == null)
            {
                return NotFound();
            }
            return View(soupKind);
        }

        // POST: SoupKinds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nazov")] SoupKind soupKind)
        {
            if (id != soupKind.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soupKind);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoupKindExists(soupKind.ID))
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
            return View(soupKind);
        }

        // GET: SoupKinds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soupKind = await _context.SoupKind
                .FirstOrDefaultAsync(m => m.ID == id);
            if (soupKind == null)
            {
                return NotFound();
            }

            return View(soupKind);
        }

        // POST: SoupKinds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soupKind = await _context.SoupKind.FindAsync(id);
            _context.SoupKind.Remove(soupKind);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoupKindExists(int id)
        {
            return _context.SoupKind.Any(e => e.ID == id);
        }
    }
}
