using System;
using System.ComponentModel.DataAnnotations;

namespace MenuGenerator.Models
{
    public class OrderNameViewModel
    {
        [Display(Name = "Poradie Jedla")]
        public int PoradieJedla { get; set; }

        [Display(Name = "Názov")]
        public string Nazov { get; set; }
    }
}
