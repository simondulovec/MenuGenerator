using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MenuGenerator.Models
{
    public class Volume
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Zadajte objem")]
        public int Objem { get; set; }
        public ICollection<Meal> Meals { get; set; }

    }
}
