using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuGenerator.Models
{
    public class Price
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Zadajte cenu")]
        [Column(TypeName = "decimal(4,2)")]
        public decimal Cena { get; set; }

        public ICollection<Meal> Meals { get; set; }
    }
}
