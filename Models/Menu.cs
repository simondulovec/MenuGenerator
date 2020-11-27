using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MenuGenerator.Models
{
    public class Menu
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Dátum vytvorenia")]
        public DateTime DatumVytvorenia { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Dátum pondelka")]
        public DateTime DatumPondelka { get; set; }

        public ICollection<Schedule> Schedules { get; set; }

    }
}
