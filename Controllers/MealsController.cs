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
    public class MealsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MealsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //// GET: Meals
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Meal.Include(m => m.Complexity).Include(m => m.MealKind).Include(m => m.Popularity).Include(m => m.Price).Include(m => m.SoupKind).Include(m => m.Volume).Include(m => m.Weight);
        //    return View(await applicationDbContext.ToListAsync());
        //}
        public async Task<IActionResult> DisplayMealsAsync()
        {
            var meals = _context.Meal.Where(m => m.MealKindID != null).
                Include(m => m.Complexity).
                Include(m => m.MealKind).
                Include(m => m.Popularity).
                Include(m => m.Price).
                Include(m => m.Weight);
            return View(await meals.ToListAsync());
        }

        public async Task<IActionResult> DetailsMeal(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
                .Include(m => m.Complexity)
                .Include(m => m.MealKind)
                .Include(m => m.Popularity)
                .Include(m => m.Price)
                .Include(m => m.Weight)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: Meals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
                .Include(m => m.Complexity)
                .Include(m => m.MealKind)
                .Include(m => m.Popularity)
                .Include(m => m.Price)
                .Include(m => m.SoupKind)
                .Include(m => m.Volume)
                .Include(m => m.Weight)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        public IActionResult CreateMeal()
        {
            ViewData["ComplexityID"] = new SelectList(_context.Complexity, "ID", "Nazov");
            ViewData["MealKindID"] = new SelectList(_context.Set<MealKind>(), "ID", "Nazov");
            ViewData["PopularityID"] = new SelectList(_context.Set<Popularity>(), "ID", "Nazov");
            ViewData["PriceID"] = new SelectList(_context.Set<Price>(), "ID", "Cena");
            ViewData["WeightID"] = new SelectList(_context.Set<Weight>(), "ID", "Hmotnost");
            return View();
        }

        public IActionResult CreateSoup()
        {
            ViewData["ComplexityID"] = new SelectList(_context.Complexity, "ID", "Nazov");
            ViewData["PopularityID"] = new SelectList(_context.Set<Popularity>(), "ID", "Nazov");
            ViewData["PriceID"] = new SelectList(_context.Set<Price>(), "ID", "Cena");
            ViewData["SoupKindID"] = new SelectList(_context.Set<SoupKind>(), "ID", "Nazov");
            ViewData["VolumeID"] = new SelectList(_context.Set<Volume>(), "ID", "Objem");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMeal([Bind("ID,MealKindID,SoupKindID,Nazov,PopularityID,ComplexityID,PriceID,WeightID,VolumeID,Alergeny")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meal);
                await _context.SaveChangesAsync();
                return RedirectToAction("DisplayMeals", "Meals");
            }
            ViewData["ComplexityID"] = new SelectList(_context.Complexity, "ID", "Nazov", meal.ComplexityID);
            ViewData["MealKindID"] = new SelectList(_context.Set<MealKind>(), "ID", "Nazov", meal.MealKindID);
            ViewData["PopularityID"] = new SelectList(_context.Set<Popularity>(), "ID", "Nazov", meal.PopularityID);
            ViewData["PriceID"] = new SelectList(_context.Set<Price>(), "ID", "Cena", meal.PriceID);
            ViewData["WeightID"] = new SelectList(_context.Set<Weight>(), "ID", "Hmotnost", meal.WeightID);
            return View(meal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSoup([Bind("ID,MealKindID,SoupKindID,Nazov,PopularityID,ComplexityID,PriceID,WeightID,VolumeID,Alergeny")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meal);
                await _context.SaveChangesAsync();
                return RedirectToAction("DisplaySoups", "Meals");
            }
            ViewData["ComplexityID"] = new SelectList(_context.Complexity, "ID", "Nazov", meal.ComplexityID);
            ViewData["PopularityID"] = new SelectList(_context.Set<Popularity>(), "ID", "Nazov", meal.PopularityID);
            ViewData["PriceID"] = new SelectList(_context.Set<Price>(), "ID", "Cena", meal.PriceID);
            ViewData["SoupKindID"] = new SelectList(_context.Set<SoupKind>(), "ID", "Nazov", meal.SoupKindID);
            ViewData["VolumeID"] = new SelectList(_context.Set<Volume>(), "ID", "Objem", meal.VolumeID);
            return View(meal);
        }

        public async Task<IActionResult> EditMeal(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }
            ViewData["ComplexityID"] = new SelectList(_context.Complexity, "ID", "Nazov", meal.ComplexityID);
            ViewData["MealKindID"] = new SelectList(_context.Set<MealKind>(), "ID", "Nazov", meal.MealKindID);
            ViewData["PopularityID"] = new SelectList(_context.Set<Popularity>(), "ID", "Nazov", meal.PopularityID);
            ViewData["PriceID"] = new SelectList(_context.Set<Price>(), "ID", "Cena", meal.PriceID);
            ViewData["WeightID"] = new SelectList(_context.Set<Weight>(), "ID", "Hmotnost", meal.WeightID);
            return View(meal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMeal(int id, [Bind("ID,MealKindID,SoupKindID,Nazov,PopularityID,ComplexityID,PriceID,WeightID,VolumeID,Alergeny")] Meal meal)
        {
            if (id != meal.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("DisplayMeals", "meals");
            }
            ViewData["ComplexityID"] = new SelectList(_context.Complexity, "ID", "Nazov", meal.ComplexityID);
            ViewData["MealKindID"] = new SelectList(_context.Set<MealKind>(), "ID", "Nazov", meal.MealKindID);
            ViewData["PopularityID"] = new SelectList(_context.Set<Popularity>(), "ID", "Nazov", meal.PopularityID);
            ViewData["PriceID"] = new SelectList(_context.Set<Price>(), "ID", "Cena", meal.PriceID);
            ViewData["WeightID"] = new SelectList(_context.Set<Weight>(), "ID", "Hmotnost", meal.WeightID);
            return View(meal);
        }



        //// GET: Meals/Create
        //public IActionResult Create()
        //{
        //    ViewData["ComplexityID"] = new SelectList(_context.Complexity, "ID", "Nazov");
        //    ViewData["MealKindID"] = new SelectList(_context.Set<MealKind>(), "ID", "Nazov");
        //    ViewData["PopularityID"] = new SelectList(_context.Set<Popularity>(), "ID", "Nazov");
        //    ViewData["PriceID"] = new SelectList(_context.Set<Price>(), "ID", "ID");
        //    ViewData["SoupKindID"] = new SelectList(_context.Set<SoupKind>(), "ID", "Nazov");
        //    ViewData["VolumeID"] = new SelectList(_context.Set<Volume>(), "ID", "ID");
        //    ViewData["WeightID"] = new SelectList(_context.Set<Weight>(), "ID", "Hmotnost");
        //    return View();
        //}

        //// POST: Meals/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,MealKindID,SoupKindID,Nazov,PopularityID,ComplexityID,PriceID,WeightID,VolumeID,Alergeny")] Meal meal)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(meal);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ComplexityID"] = new SelectList(_context.Complexity, "ID", "Nazov", meal.ComplexityID);
        //    ViewData["MealKindID"] = new SelectList(_context.Set<MealKind>(), "ID", "Nazov", meal.MealKindID);
        //    ViewData["PopularityID"] = new SelectList(_context.Set<Popularity>(), "ID", "Nazov", meal.PopularityID);
        //    ViewData["PriceID"] = new SelectList(_context.Set<Price>(), "ID", "ID", meal.PriceID);
        //    ViewData["SoupKindID"] = new SelectList(_context.Set<SoupKind>(), "ID", "Nazov", meal.SoupKindID);
        //    ViewData["VolumeID"] = new SelectList(_context.Set<Volume>(), "ID", "ID", meal.VolumeID);
        //    ViewData["WeightID"] = new SelectList(_context.Set<Weight>(), "ID", "Hmotnost", meal.WeightID);
        //    return View(meal);
        //}

        // GET: Meals/Edit/5



        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var meal = await _context.Meal.FindAsync(id);
        //    if (meal == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ComplexityID"] = new SelectList(_context.Complexity, "ID", "Nazov", meal.ComplexityID);
        //    ViewData["MealKindID"] = new SelectList(_context.Set<MealKind>(), "ID", "Nazov", meal.MealKindID);
        //    ViewData["PopularityID"] = new SelectList(_context.Set<Popularity>(), "ID", "Nazov", meal.PopularityID);
        //    ViewData["PriceID"] = new SelectList(_context.Set<Price>(), "ID", "ID", meal.PriceID);
        //    ViewData["SoupKindID"] = new SelectList(_context.Set<SoupKind>(), "ID", "Nazov", meal.SoupKindID);
        //    ViewData["VolumeID"] = new SelectList(_context.Set<Volume>(), "ID", "ID", meal.VolumeID);
        //    ViewData["WeightID"] = new SelectList(_context.Set<Weight>(), "ID", "Hmotnost", meal.WeightID);
        //    return View(meal);
        //}

        //// POST: Meals/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,MealKindID,SoupKindID,Nazov,PopularityID,ComplexityID,PriceID,WeightID,VolumeID,Alergeny")] Meal meal)
        //{
        //    if (id != meal.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(meal);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MealExists(meal.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ComplexityID"] = new SelectList(_context.Complexity, "ID", "Nazov", meal.ComplexityID);
        //    ViewData["MealKindID"] = new SelectList(_context.Set<MealKind>(), "ID", "Nazov", meal.MealKindID);
        //    ViewData["PopularityID"] = new SelectList(_context.Set<Popularity>(), "ID", "Nazov", meal.PopularityID);
        //    ViewData["PriceID"] = new SelectList(_context.Set<Price>(), "ID", "ID", meal.PriceID);
        //    ViewData["SoupKindID"] = new SelectList(_context.Set<SoupKind>(), "ID", "Nazov", meal.SoupKindID);
        //    ViewData["VolumeID"] = new SelectList(_context.Set<Volume>(), "ID", "ID", meal.VolumeID);
        //    ViewData["WeightID"] = new SelectList(_context.Set<Weight>(), "ID", "Hmotnost", meal.WeightID);
        //    return View(meal);
        //}

        // GET: Meals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
                .Include(m => m.Complexity)
                .Include(m => m.MealKind)
                .Include(m => m.Popularity)
                .Include(m => m.Price)
                .Include(m => m.SoupKind)
                .Include(m => m.Volume)
                .Include(m => m.Weight)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meal = await _context.Meal.FindAsync(id);
            _context.Meal.Remove(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealExists(int id)
        {
            return _context.Meal.Any(e => e.ID == id);
        }
    }
}
