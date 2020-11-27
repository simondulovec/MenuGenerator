using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace MenuGenerator.Models
{
    public class MyMealsViewModel
    {
        public List<Meal> MenuMeals { get; set; }
        public DbSet<Meal> AllMeals { get; set; }
        public List<string> Week { get; set; }
        public DbSet<Generator> Generators { get; set; }
        public List<string> PrvePorovnanie { get; set; }
        public DateTime PrvyPondelok { get; set; }
        public List<string> DruhePorovnanie { get; set; }
        public DateTime DruhyPondelok { get; set; }
    }
}