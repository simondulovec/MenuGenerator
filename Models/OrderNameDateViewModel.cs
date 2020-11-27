using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MenuGenerator.Models
{
    public class OrderNameDateViewModel
    {
        public IOrderedQueryable<OrderNameViewModel> JedlaPoradie { get; set; }
        public DateTime DatumPondelka { get; set; }
    }
}
