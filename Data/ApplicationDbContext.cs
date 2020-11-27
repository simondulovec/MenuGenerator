using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MenuGenerator.Models;

namespace MenuGenerator.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MenuGenerator.Models.Complexity> Complexity { get; set; }
        public DbSet<MenuGenerator.Models.Meal> Meal { get; set; }
        public DbSet<MenuGenerator.Models.MealKind> MealKind { get; set; }
        public DbSet<MenuGenerator.Models.Menu> Menu { get; set; }
        public DbSet<MenuGenerator.Models.Popularity> Popularity { get; set; }
        public DbSet<MenuGenerator.Models.Price> Price { get; set; }
        public DbSet<MenuGenerator.Models.Schedule> Schedule { get; set; }
        public DbSet<MenuGenerator.Models.SoupKind> SoupKind { get; set; }
        public DbSet<MenuGenerator.Models.Volume> Volume { get; set; }
        public DbSet<MenuGenerator.Models.Weight> Weight { get; set; }
        public DbSet<MenuGenerator.Models.WeekDay> WeekDay { get; set; }
        public DbSet<MenuGenerator.Models.Generator> Generator { get; set; }
        public DbSet<MenuGenerator.Models.GeneratorOptions> GeneratorOptions { get; set; }
    }
}
