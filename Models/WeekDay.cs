using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MenuGenerator.Models
{
    public class WeekDay
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string Nazov { get; set; }

        public ICollection<GeneratorOptions> GeneratorOptions { get; set; }
    }
}
