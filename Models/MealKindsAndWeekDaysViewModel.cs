using System.Linq;

namespace MenuGenerator.Models
{
    public class MealKindsAndWeekDaysViewModel
    {
        public IQueryable<WeekDay> Tyzden { get; set; }
        public IQueryable<MealKind> DruhJedla { get; set; }
        public IQueryable<Popularity> Oblubenost { get; set; }
    }
}
