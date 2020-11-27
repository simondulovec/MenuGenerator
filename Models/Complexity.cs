using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MenuGenerator.Models
{
    public class Complexity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Názov")]
        public string Nazov { get; set; }

        public ICollection<Meal> Meals { get; set; }
    }
}
