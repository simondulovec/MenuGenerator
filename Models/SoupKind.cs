using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MenuGenerator.Models
{
    public class SoupKind
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Názov")]
        public string Nazov { get; set; }

        public ICollection<Meal> Meals { get; set; }
    }
}
