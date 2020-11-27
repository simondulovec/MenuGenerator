using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuGenerator.Models
{
    public class Schedule
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Dátum podávania")]
        public DateTime DatumPodavania { get; set; }

        [ForeignKey("Menu")]
        [Display(Name = "Menu Id")]
        public int MenuID { get; set; }
        public Menu Menu { get; set; }

        [ForeignKey("Meal")]
        [Display(Name = "Jedlo Id")]
        public int MealID { get; set; }
        public Meal Meal { get; set; }

        public int PoradieJedla { get; set; }
    }
}
