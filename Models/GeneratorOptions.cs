using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuGenerator.Models
{
    public class GeneratorOptions
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("WeekDay")]
        [Display(Name = "Deň")]
        public int DayID { get; set; }
        public WeekDay WeekDay { get; set; }

        [ForeignKey("Generator")]
        [Display(Name = "Generátor")]
        public int GeneratorID { get; set; }
        public Generator Generator { get; set; }

        [ForeignKey("MealKind")]
        [Display(Name = "Druh jedla")]
        public int MealKindID { get; set; }
        public MealKind MealKind { get; set; }

        [ForeignKey("Popularity")]
        [Display(Name = "Obľúbenosť")]
        public int PopularityID { get; set; }
        public Popularity Popularity { get; set; }

        public int PoradieJedla { get; set; }

    }
}
