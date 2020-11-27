using MenuGenerator.Data;
using MenuGenerator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuGenerator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GeneratorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GeneratorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Generators
        public async Task<IActionResult> Index()
        {
            return View(await _context.Generator.ToListAsync());
        }

        public IActionResult CreateProfile()
        {
            var weekDays = _context.WeekDay;
            var mealKind = _context.MealKind;
            var popularity = _context.Popularity;

            return View(new MealKindsAndWeekDaysViewModel() { Tyzden = weekDays, DruhJedla = mealKind, Oblubenost = popularity });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task SaveProfileAsync(int p_Pondelok1, int p_Pondelok2, int p_Pondelok3, int p_Pondelok4,
            List<string> Pondelok1, List<string> Pondelok2, List<string> Pondelok3, List<string> Pondelok4,
            //utorok
            int p_Utorok1, int p_Utorok2, int p_Utorok3, int p_Utorok4,
            List<string> Utorok1, List<string> Utorok2, List<string> Utorok3, List<string> Utorok4,
            //streda
            int p_Streda1, int p_Streda2, int p_Streda3, int p_Streda4,
            List<string> Streda1, List<string> Streda2, List<string> Streda3, List<string> Streda4,
            //stvrtok
            int p_Štvrtok1, int p_Štvrtok2, int p_Štvrtok3, int p_Štvrtok4,
            List<string> Štvrtok1, List<string> Štvrtok2, List<string> Štvrtok3, List<string> Štvrtok4,
            //piatok
            int p_Piatok1, int p_Piatok2, int p_Piatok3, int p_Piatok4,
            List<string> Piatok1, List<string> Piatok2, List<string> Piatok3, List<string> Piatok4,
            //sobota
            int p_Sobota,
            List<string> Sobota,
            //nedela
            int p_Nedeľa,
            List<string> Nedeľa,
            int uniqueMeals,
            string profileName)
        {
            //druhy jedal
            List<List<string>> monday = new List<List<string>>() { Pondelok1, Pondelok2, Pondelok3, Pondelok4 };
            List<List<string>> tuesday = new List<List<string>>() { Utorok1, Utorok2, Utorok3, Utorok4 };
            List<List<string>> wednesday = new List<List<string>>() { Streda1, Streda2, Streda3, Streda4 };
            List<List<string>> thursday = new List<List<string>>() { Štvrtok1, Štvrtok2, Štvrtok3, Štvrtok4 };
            List<List<string>> friday = new List<List<string>>() { Piatok1, Piatok2, Piatok3, Piatok4 };
            List<List<string>> saturday = new List<List<string>>() { Sobota };
            List<List<string>> sunday = new List<List<string>>() { Nedeľa };
            List<List<List<string>>> week = new List<List<List<string>>>() { monday, tuesday, wednesday, thursday, friday, saturday, sunday };

            //popularita jedal
            List<int> p_monday = new List<int>() { p_Pondelok1, p_Pondelok2, p_Pondelok3, p_Pondelok4 };
            List<int> p_tuesday = new List<int>() { p_Utorok1, p_Utorok2, p_Utorok3, p_Utorok4 };
            List<int> p_wednesday = new List<int>() { p_Streda1, p_Streda2, p_Streda3, p_Streda4 };
            List<int> p_thursday = new List<int>() { p_Štvrtok1, p_Štvrtok2, p_Štvrtok3, p_Štvrtok4 };
            List<int> p_friday = new List<int>() { p_Piatok1, p_Piatok2, p_Piatok3, p_Piatok4 };
            List<int> p_saturday = new List<int>() { p_Sobota };
            List<int> p_sunday = new List<int>() { p_Nedeľa };
            List<List<int>> p_week = new List<List<int>>() { p_monday, p_tuesday, p_wednesday, p_thursday, p_friday, p_saturday, p_sunday };

            //v prvom kroku vytvorim novy profil generatota
            Generator generator = new Generator() { NazovProfilu = profileName, PocetUnikatnych = uniqueMeals };
            await _context.Generator.AddAsync(generator);
            _context.SaveChanges();
            //index pre dni, len za predpokladu, ze su ulozene od pondelka do nedele
            int dayID = 1;
            int mealOrder;
            foreach (var day in week)
            {
                mealOrder = 1;
                foreach (var select in day)
                {

                    foreach (var option in select)
                    {
                        await _context.GeneratorOptions.AddAsync(new GeneratorOptions()
                        {
                            DayID = dayID,
                            GeneratorID = generator.ID,
                            MealKindID = Convert.ToInt32(option),
                            PoradieJedla = mealOrder,
                            PopularityID = p_week[dayID - 1][mealOrder - 1] //odpocitavam jednotku lebo oblubenost je indexovana od nuly
                        });
                    }
                    mealOrder += 1;
                }
                dayID += 1;
            }
            _context.SaveChanges();
        }

        // GET: Generators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generator = await _context.Generator
                .FirstOrDefaultAsync(m => m.ID == id);
            if (generator == null)
            {
                return NotFound();
            }

            return View(generator);
        }

        // GET: Generators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NazovProfilu,PocetUnikatnych")] Generator generator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(generator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(generator);
        }

        // GET: Generators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generator = await _context.Generator.FindAsync(id);
            if (generator == null)
            {
                return NotFound();
            }
            return View(generator);
        }

        // POST: Generators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NazovProfilu,PocetUnikatnych")] Generator generator)
        {
            if (id != generator.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneratorExists(generator.ID))
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
            return View(generator);
        }

        // GET: Generators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generator = await _context.Generator
                .FirstOrDefaultAsync(m => m.ID == id);
            if (generator == null)
            {
                return NotFound();
            }

            return View(generator);
        }

        // POST: Generators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var generator = await _context.Generator.FindAsync(id);
            _context.Generator.Remove(generator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneratorExists(int id)
        {
            return _context.Generator.Any(e => e.ID == id);
        }
    }
}
