using MenuGenerator.Data;
using MenuGenerator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MenuGenerator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GeneratorOptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GeneratorOptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GeneratorOptions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GeneratorOptions.Include(g => g.Generator).Include(g => g.MealKind).Include(g => g.Popularity).Include(g => g.WeekDay);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GeneratorOptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generatorOptions = await _context.GeneratorOptions
                .Include(g => g.Generator)
                .Include(g => g.MealKind)
                .Include(g => g.Popularity)
                .Include(g => g.WeekDay)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (generatorOptions == null)
            {
                return NotFound();
            }

            return View(generatorOptions);
        }

        // GET: GeneratorOptions/Create
        public IActionResult Create()
        {
            ViewData["GeneratorID"] = new SelectList(_context.Generator, "ID", "NazovProfilu");
            ViewData["MealKindID"] = new SelectList(_context.MealKind, "ID", "Nazov");
            ViewData["PopularityID"] = new SelectList(_context.Popularity, "ID", "Nazov");
            ViewData["DayID"] = new SelectList(_context.WeekDay, "ID", "Nazov");
            return View();
        }

        // POST: GeneratorOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DayID,GeneratorID,MealKindID,PopularityID,PoradieJedla")] GeneratorOptions generatorOptions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(generatorOptions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneratorID"] = new SelectList(_context.Generator, "ID", "NazovProfilu", generatorOptions.GeneratorID);
            ViewData["MealKindID"] = new SelectList(_context.MealKind, "ID", "Nazov", generatorOptions.MealKindID);
            ViewData["PopularityID"] = new SelectList(_context.Popularity, "ID", "Nazov", generatorOptions.PopularityID);
            ViewData["DayID"] = new SelectList(_context.WeekDay, "ID", "Nazov", generatorOptions.DayID);
            return View(generatorOptions);
        }

        // GET: GeneratorOptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generatorOptions = await _context.GeneratorOptions.FindAsync(id);
            if (generatorOptions == null)
            {
                return NotFound();
            }
            ViewData["GeneratorID"] = new SelectList(_context.Generator, "ID", "NazovProfilu", generatorOptions.GeneratorID);
            ViewData["MealKindID"] = new SelectList(_context.MealKind, "ID", "Nazov", generatorOptions.MealKindID);
            ViewData["PopularityID"] = new SelectList(_context.Popularity, "ID", "Nazov", generatorOptions.PopularityID);
            ViewData["DayID"] = new SelectList(_context.WeekDay, "ID", "Nazov", generatorOptions.DayID);
            return View(generatorOptions);
        }

        // POST: GeneratorOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DayID,GeneratorID,MealKindID,PopularityID,PoradieJedla")] GeneratorOptions generatorOptions)
        {
            if (id != generatorOptions.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generatorOptions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneratorOptionsExists(generatorOptions.ID))
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
            ViewData["GeneratorID"] = new SelectList(_context.Generator, "ID", "NazovProfilu", generatorOptions.GeneratorID);
            ViewData["MealKindID"] = new SelectList(_context.MealKind, "ID", "Nazov", generatorOptions.MealKindID);
            ViewData["PopularityID"] = new SelectList(_context.Popularity, "ID", "Nazov", generatorOptions.PopularityID);
            ViewData["DayID"] = new SelectList(_context.WeekDay, "ID", "Nazov", generatorOptions.DayID);
            return View(generatorOptions);
        }

        // GET: GeneratorOptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generatorOptions = await _context.GeneratorOptions
                .Include(g => g.Generator)
                .Include(g => g.MealKind)
                .Include(g => g.Popularity)
                .Include(g => g.WeekDay)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (generatorOptions == null)
            {
                return NotFound();
            }

            return View(generatorOptions);
        }

        // POST: GeneratorOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var generatorOptions = await _context.GeneratorOptions.FindAsync(id);
            _context.GeneratorOptions.Remove(generatorOptions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneratorOptionsExists(int id)
        {
            return _context.GeneratorOptions.Any(e => e.ID == id);
        }
    }
}
