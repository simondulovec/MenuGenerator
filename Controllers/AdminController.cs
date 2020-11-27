using ExcelDataReader;
using MenuGenerator.Data;
using MenuGenerator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuGenerator.Controllers

{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;
        private Random _rnd;
        private List<int> _generated_week_meals;
        private MyMealsViewModel _meals;
        private List<string> _week;
        private int _mealsInMenu;
        List<int> _complexities;

        public AdminController(UserManager<IdentityUser> userManager,
            ApplicationDbContext context,
            IWebHostEnvironment hostEnviroment)
        {
            _userManager = userManager;
            _context = context;
            _hostEnviroment = hostEnviroment;
            _rnd = new Random();
            _generated_week_meals = new List<int>();
            _week = new List<string>() { "Pondelok", "Utorok", "Streda", "Štvrtok", "Piatok", "Sobota", "Nedeľa" };
            _meals = new MyMealsViewModel() { MenuMeals = new List<Meal>(), AllMeals = _context.Meal, Week = _week, Generators = _context.Generator };
            _mealsInMenu = 22;
            _complexities = new List<int>();

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListUsersAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync("User");
            return View(users);
        }

        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportFileAsync(IFormFile excelFile)
        {
            bool success;
            if (excelFile == null)
            {
                TempData["import"] = "error";
                return RedirectToAction("Import", "Admin");
            }
            else
            {
                // create path to files folder
                string folderPath = Path.Combine(_hostEnviroment.WebRootPath, "files");
                // create unique identificator for file
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + excelFile.FileName;
                // create completed path with file identificator + file name
                string filePath = Path.Combine(folderPath, uniqueFileName);
                // create stream
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await excelFile.CopyToAsync(fileStream);
                }

                success = await UploadDataAsync(filePath);
            }

            if (success)
            {
                TempData["import"] = "success";
                return RedirectToAction("Import", "Admin");
            }
            TempData["import"] = "failed";
            return RedirectToAction("Import", "Admin");
        }

        public async Task<bool> UploadDataAsync(string fName)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(fName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    ExcelDataSetConfiguration conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };

                    DataTableCollection sheets = reader.AsDataSet(conf).Tables;

                    //jedla
                    int MEAL_KIND = 0;
                    int NAME = 1;
                    int WEIGHT = 2;
                    int ALERGENS = 3;
                    int COMPLEXITY = 4;
                    int POPULARITY = 5;
                    int PRICE = 7;

                    foreach (DataTable sheet in sheets)
                    {
                        foreach (DataRow row in sheet.Rows)
                        {
                            Meal meal = new Meal();

                            /*TYP_JEDLA*/
                            string mealKindVal = row[MEAL_KIND].ToString();
                            if (_context.MealKind.Where(kind => kind.Nazov == mealKindVal).FirstOrDefault() == null)
                            {
                                //novy typ jedla, preto ho najprv pridam do databazy
                                await _context.MealKind.AddAsync(new MealKind() { Nazov = mealKindVal });
                                await _context.SaveChangesAsync();
                            }
                            //nacitam id ulozeneho typu z databazy
                            int mealKindID = _context.MealKind.Where(kind => kind.Nazov == mealKindVal).FirstOrDefault().ID;
                            meal.MealKindID = mealKindID;

                            /*NAZOV**************************************************************************************************************/
                            meal.Nazov = row[NAME].ToString();
                            /********************************************************************************************************************/

                            /*HMOTNOST***********************************************************************************************************/
                            string weightVal = row[WEIGHT].ToString();
                            if (_context.Weight.Where(weight => weight.Hmotnost == weightVal).FirstOrDefault() == null)
                            {
                                //nova hmotnost jedla, preto ju najprv pridam do databazy
                                await _context.Weight.AddAsync(new Weight() { Hmotnost = weightVal });
                                await _context.SaveChangesAsync();
                            }
                            int weightID = _context.Weight.Where(weight => weight.Hmotnost == weightVal).FirstOrDefault().ID;
                            meal.WeightID = weightID;
                            /*********************************************************************************************************************/

                            /*ALERGENY************************************************************************************************************/
                            if (row[ALERGENS].ToString() == "")
                            {
                                meal.Alergeny = "???";
                            }
                            else
                            {
                                meal.Alergeny = row[ALERGENS].ToString();
                            }
                            /**********************************************************************************************************************/

                            /*NAROCNOST************************************************************************************************************/
                            string complexityVal = row[COMPLEXITY].ToString();
                            if (_context.Complexity.Where(complexity => complexity.Nazov == complexityVal).FirstOrDefault() == null)
                            {
                                //nova narocnost jedla, preto ju najprv pridam do databazy
                                await _context.Complexity.AddAsync(new Complexity() { Nazov = complexityVal });
                                await _context.SaveChangesAsync();
                            }
                            int complexityID = _context.Complexity.Where(complexity => complexity.Nazov == complexityVal).FirstOrDefault().ID;
                            meal.ComplexityID = complexityID;
                            /************************************************************************************************************************/

                            /*OBLUBENOST*************************************************************************************************************/
                            string popularityVal = row[POPULARITY].ToString();
                            if (_context.Popularity.Where(popularity => popularity.Nazov == popularityVal).FirstOrDefault() == null)
                            {
                                //nova oblubenost jedla, preto ju najprv pridam do databazy
                                await _context.Popularity.AddAsync(new Popularity() { Nazov = popularityVal });
                                await _context.SaveChangesAsync();
                            }
                            int popularityID = _context.Popularity.Where(popularity => popularity.Nazov == popularityVal).FirstOrDefault().ID;
                            meal.PopularityID = popularityID;
                            /************************************************************************************************************************/

                            /*CENA*************************************************************************************************************/
                            decimal priceVal = Convert.ToDecimal(row[PRICE].ToString());
                            if (_context.Price.Where(price => price.Cena == priceVal).FirstOrDefault() == null)
                            {
                                //nova cena jedla, preto ju najprv pridam do databazy
                                await _context.Price.AddAsync(new Price() { Cena = priceVal });
                                await _context.SaveChangesAsync();
                            }
                            int priceID = _context.Price.Where(price => price.Cena == priceVal).FirstOrDefault().ID;
                            meal.PriceID = priceID;
                            /************************************************************************************************************************/

                            /*ADDING_MEAL************************************************************************************************************/
                            await _context.Meal.AddAsync(meal);
                            await _context.SaveChangesAsync();
                            /************************************************************************************************************************/
                        }
                    }
                }
            }
            //kontrola ci ulozilo data zo suboru
            bool retValue = false;
            if (_context.Meal.Any())
            {
                retValue = true;
            }
            return retValue;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveMenuAsync(List<int> mealIDs, DateTime date)
        {
            //chyba ukladania
            if (mealIDs.Contains(-1) || date.Date.Year.ToString() != DateTime.Now.Date.Year.ToString())
            {
                foreach (int id in mealIDs)
                {
                    if (id == -1)
                    {
                        _meals.MenuMeals.Add(new Meal() { Nazov = "???", ID = -1 });
                    }
                    else
                    {
                        _meals.MenuMeals.Add(_context.Meal.Where(meal => meal.ID == id).FirstOrDefault());
                    }
                    AddCompareMenus();
                }
                TempData["saveMenu"] = "error";
                return View("GenerateMenu", _meals);
            }
            else
            {
                //datum zo vstupu
                var mealDate = date.Date;
                //pred pridavanim jedal najprv vytvorim menu
                Menu menu = new Menu { DatumVytvorenia = DateTime.Now.Date, DatumPondelka = mealDate };
                await _context.Menu.AddAsync(menu);
                await _context.SaveChangesAsync();

                int menuId = menu.ID;
                int index = 0;
                int mealsInDay = 4;
                int saturday = 21;
                int sunday = 22;
                foreach (int id in mealIDs)
                {
                    //zvysujem datum len ked prekrocim dany pocet jedal v jeden den (4) alebo pride narad sobota(21) alebo nedela(22)
                    if (index % mealsInDay == 0 && index != saturday && index != sunday && index != 0)
                    {
                        mealDate = mealDate.AddDays(1);
                    }
                    else if (index == saturday || index == sunday)
                    {
                        mealDate = mealDate.AddDays(1);
                    }

                    await _context.Schedule.AddAsync(new Schedule() { DatumPodavania = mealDate, MealID = id, MenuID = menuId, PoradieJedla = index });

                    index += 1;
                }
                //ukladam jedla do rozvrhu
                await _context.SaveChangesAsync();
            }
            TempData["saveMenu"] = "success";
            return View("GenerateMenu", _meals);
        }

        public void AddCompareMenus()
        {
            var menuIDs = _context.Menu.OrderBy(menu => menu.DatumPondelka).Select(menu => menu.ID).ToList();

            if (_context.Menu.Count() == 1)
            {
                var twoLast = menuIDs.TakeLast(1).ToList();
                _meals.PrvePorovnanie = _context.Schedule.Where(sch => sch.MenuID == twoLast[0]).
                    Join(_context.Meal,
                    sch => sch.MealID,
                    meal => meal.ID,
                    (sch, meal) => new OrderNameViewModel() { Nazov = meal.Nazov, PoradieJedla = sch.PoradieJedla }).
                    OrderBy(meal => meal.PoradieJedla).
                    Select(onvm => onvm.Nazov).
                    ToList();
            }
            else if (_context.Menu.Count() >= 2)
            {
                var twoLast = menuIDs.TakeLast(2).ToList();
                _meals.PrvePorovnanie = _context.Schedule.Where(sch => sch.MenuID == twoLast[0]).
                    Join(_context.Meal,
                    sch => sch.MealID,
                    meal => meal.ID,
                    (sch, meal) => new OrderNameViewModel() { Nazov = meal.Nazov, PoradieJedla = sch.PoradieJedla }).
                    OrderBy(meal => meal.PoradieJedla).
                    Select(onvm => onvm.Nazov).
                    ToList();

                _meals.DruhePorovnanie = _context.Schedule.Where(sch => sch.MenuID == twoLast[1]).
                    Join(_context.Meal,
                    sch => sch.MealID,
                    meal => meal.ID,
                    (sch, meal) => new OrderNameViewModel() { Nazov = meal.Nazov, PoradieJedla = sch.PoradieJedla }).
                    OrderBy(meal => meal.PoradieJedla).
                    Select(onvm => onvm.Nazov).
                    ToList();
            }
        }

        public IActionResult GenerateMenu(int profileID = 1)
        {
            /****************************NACITAVAM_2_POSLEDNE_MENU*****************************/

            AddCompareMenus();

            /****************************GENERUJEM_MENU******************************************************/
            int daysInWeek = 7;
            int mealsInWeek = 4;
            int FIRSTMEAL = 1;

            var profile = _context.GeneratorOptions.Where(gen => gen.GeneratorID == profileID);
            int _uniqueLength = _context.Generator.Where(gen => gen.ID == profileID).Select(gen => gen.PocetUnikatnych).FirstOrDefault();

            int menus = _context.Menu.Count();
            if (menus * _mealsInMenu < _uniqueLength)
            {
                _generated_week_meals = _context.Schedule.Select(item => item.MealID).ToList();
            }
            else
            {
                _generated_week_meals = _context.Schedule.OrderBy(item => item.DatumPodavania).TakeLast(_uniqueLength).Select(item => item.MealID).ToList();
            }

            _meals.MenuMeals.Clear();
            //prejdem vsetky dni v tyzdni
            for (int i = 1; i <= daysInWeek; i++)
            {
                var day = profile.Where(prof => prof.DayID == i);
                //ce tyzden generujem styri jedla
                if (i < 6)
                {
                    _complexities.Clear();
                    for (int j = 1; j <= mealsInWeek; j++)
                    {
                        List<int> mealKinds = day.Where(day => day.PoradieJedla == j).Select(day => day.MealKindID).ToList();
                        int popularity = day.Where(day => day.PoradieJedla == j).Select(day => day.PopularityID).FirstOrDefault();
                        _meals.MenuMeals.Add(FindMeal(popularity, mealKinds, _rnd, _generated_week_meals, _complexities, _context.Meal));
                    }
                }
                //cez vikend generujem jedno jedlo
                else
                {
                    _complexities.Clear();
                    List<int> mealKinds = day.Where(day => day.PoradieJedla == FIRSTMEAL).Select(day => day.MealKindID).ToList();
                    int popularity = day.Where(day => day.PoradieJedla == FIRSTMEAL).Select(day => day.PopularityID).FirstOrDefault();
                    _meals.MenuMeals.Add(FindMeal(popularity, mealKinds, _rnd, _generated_week_meals, _complexities, _context.Meal));
                }
            }

            return View(_meals);
        }

        public IQueryable<Meal> CheckComplexity(IQueryable<Meal> meals, List<int> complexities)
        {
            int LOW = 1;
            int MEDIUM = 2;
            int HIGH = 3;

            if (complexities.Contains(HIGH) || complexities.Count(meal => meal.Equals(MEDIUM)) == 2)
            {
                //prve jedlo je tazke, ostatne jedla budu lahke
                meals = meals.Where(meal => meal.ComplexityID == LOW);
            }
            else if (complexities.Contains(MEDIUM))
            {
                //prve jedlo stredne, mozem este vybrat dalsie stredne alebo jednoduhe
                meals = meals.Where(meal => meal.ComplexityID != HIGH);
            }
            return meals;
        }

        //pomocna funkcia, ktora z iQueryable vyberie unikatne jedlo, skontroluje a ulozi narocnost, a vyberie nahodne jedlo, ktore vyhovujuce podmienkam pre dany den
        public Meal GetMeal(List<int> mealKindIDs, Random random, List<int> generated_meals, List<int> complexities, IQueryable<Meal> meals)
        {
            List<Meal> mealsList = new List<Meal>();
            Meal meal = null;
            Meal no_meal = new Meal() { Nazov = "???", ID = -1 };
            int index = 0;
            int LOW = 1;

            //do iniciacneho IQueryable ulozim jedla podla prveho indexu mealKindIDs
            IQueryable<Meal> mealKinds = meals.Where(meal => meal.MealKindID == mealKindIDs[0]);
            List<Meal> mealKindsList = mealKinds.ToList();

            for (int i = 1; i < mealKindIDs.Count(); i++)
            {
                mealKindsList.AddRange(meals.Where(meal => meal.MealKindID == mealKindIDs[i]).ToList());
            }

            mealKinds = mealKindsList.AsQueryable<Meal>();

            //vyberiem unikatne jedlo
            meals = mealKinds.Where(meal => !generated_meals.Contains(meal.ID));

            mealsList = CheckComplexity(meals, complexities).ToList();

            if (!mealsList.Any())
            {
                return no_meal;
            }
            else
            {
                index = random.Next(0, mealsList.Count);
                meal = mealsList[index];
                int complexity = meal.ComplexityID;
                if (complexity != LOW)
                {
                    complexities.Add(complexity);
                }
                return meal;
            }
        }

        public Meal FindMeal(int popularity, List<int> mealKindIDs, Random random, List<int> generated_meals, List<int> complexities, DbSet<Meal> database_meals)
        {
            int LOW = 1;
            int MEDIUM = 2;
            int HIGH = 3;
            Meal meal = null;

            if (popularity == HIGH)
            {
                IQueryable<Meal> meals = database_meals.Where(meal => meal.PopularityID == HIGH);

                if (!meals.Any())
                {
                    meals = _context.Meal.Where(meal => meal.PopularityID == MEDIUM);
                }
                if (!meals.Any())
                {
                    meals = _context.Meal.Where(meal => meal.PopularityID == LOW);
                }
                // v meals mam ulozene jedla s urcitou popularitou
                meal = GetMeal(mealKindIDs, random, generated_meals, complexities, meals);

            }
            else if (popularity == MEDIUM)
            {
                var meals = database_meals.Where(meal => meal.PopularityID == MEDIUM);
                if (!meals.Any())
                {
                    meals = _context.Meal.Where(meal => meal.PopularityID == LOW);
                }

                meal = GetMeal(mealKindIDs, random, generated_meals, complexities, meals);
            }
            else if (popularity == LOW)
            {
                var meals = _context.Meal.Where(meal => meal.PopularityID == LOW);
                meal = GetMeal(mealKindIDs, random, generated_meals, complexities, meals);
            }

            //ak som nasiel vyhovujuce jedlo pridam ho do vygenerovanych ak nie iba ho vratim
            if (meal.Nazov != "???")
            {
                generated_meals.Add(meal.ID);
            }
            return meal;
        }

    }
}

