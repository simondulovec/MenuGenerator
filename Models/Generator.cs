using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MenuGenerator.Models
{
    public class Generator
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string NazovProfilu { get; set; }
        
        public int PocetUnikatnych { get; set; }

        public ICollection<GeneratorOptions> GeneratorOptions { get; set; }
    }
}
