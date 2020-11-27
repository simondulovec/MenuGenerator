using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MenuGenerator.Models
{
    public class MealKind
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Zadajte názov")]
        [Display(Name = "Názov")]
        public string Nazov { get; set; }

        public ICollection<Meal> Meals { get; set; }
        public ICollection<GeneratorOptions> GeneratorOptions { get; set; }
    }
}
