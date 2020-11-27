using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuGenerator.Models
{
    public class Meal
    {
        [Key]
        public int ID { get; set; }

        //MEAL KIND FOREIGN KEY
        [ForeignKey("MealKind")]
        [Display(Name = "Druh jedla")]
        public int? MealKindID { get; set; }    //NULLABLE
        public MealKind MealKind { get; set; }

        //Soup KIND FOREIGN KEY
        [ForeignKey("SoupKind")]
        [Display(Name = "Druh polievky")]
        public int? SoupKindID { get; set; }    //NULLABLE
        public SoupKind SoupKind { get; set; }

        //NAME
        [Required(ErrorMessage = "Zadajte názov")]
        [Display(Name = "Názov")]
        public string Nazov { get; set; }

        //POPULARITY FOREIGN KEY
        [ForeignKey("Popularity")]
        [Display(Name = "Obľúbenosť")]
        public int PopularityID { get; set; }
        public Popularity Popularity { get; set; }

        //COMPLEXITY FOREIGN KEY
        [ForeignKey("Complexity")]
        [Display(Name = "Náročnosť prípravy")]
        public int ComplexityID { get; set; }
        public Complexity Complexity { get; set; }

        //PRICE FOREIGN KEY
        [ForeignKey("Price")]
        [Display(Name = "Cena")]
        public int PriceID { get; set; }
        public Price Price { get; set; }

        //WEIGHT FOREIGN KEY
        [ForeignKey("Weight")]
        [Display(Name = "Hmotnosť")]
        public int? WeightID { get; set; } //NULLABLE
        public Weight Weight { get; set; }

        //Volume FOREIGN KEY
        [ForeignKey("Volume")]
        [Display(Name = "Objem")] 
        public int? VolumeID { get; set; } //NULLABLE
        public Volume Volume { get; set; }

        [Display(Name = "Alregény")]
        public string Alergeny { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }
}
