using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MenuGenerator.Models
{
    public class Weight
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Zadajte hmotnosť")]
        [Display(Name = "Hmotnosť")]
        public string Hmotnost { get; set; }

        public ICollection<Meal> Meals { get; set; }
    }
}
