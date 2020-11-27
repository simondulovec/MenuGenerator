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
    public class WeekDaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeekDaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WeekDays
        public async Task<IActionResult> Index()
        {
            return View(await _context.WeekDay.ToListAsync());
        }

        // GET: WeekDays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weekDay = await _context.WeekDay
                .FirstOrDefaultAsync(m => m.ID == id);
            if (weekDay == null)
            {
                return NotFound();
            }

            return View(weekDay);
        }

        // GET: WeekDays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeekDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nazov")] WeekDay weekDay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weekDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weekDay);
        }

        // GET: WeekDays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weekDay = await _context.WeekDay.FindAsync(id);
            if (weekDay == null)
            {
                return NotFound();
            }
            return View(weekDay);
        }

        // POST: WeekDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nazov")] WeekDay weekDay)
        {
            if (id != weekDay.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weekDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeekDayExists(weekDay.ID))
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
            return View(weekDay);
        }

        // GET: WeekDays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weekDay = await _context.WeekDay
                .FirstOrDefaultAsync(m => m.ID == id);
            if (weekDay == null)
            {
                return NotFound();
            }

            return View(weekDay);
        }

        // POST: WeekDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weekDay = await _context.WeekDay.FindAsync(id);
            _context.WeekDay.Remove(weekDay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeekDayExists(int id)
        {
            return _context.WeekDay.Any(e => e.ID == id);
        }
    }
}
