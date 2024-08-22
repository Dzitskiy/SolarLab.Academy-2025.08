using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.Domain.Advert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Infrastructure
{
    public class BoardDbContext:DbContext
    {
        public DbSet<Advert> Adverts { get; set; }

        public BoardDbContext(DbContextOptions<BoardDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Advert>().HasData(
       new Advert 
       {
           Id = new Guid("e5878c94 - 90eb - 47f5 - a52f - c1c9e65171cc"), 
           Name = "car sale",
           Description = " new car for sale .",
           
       }
   );

            base.OnModelCreating(modelBuilder);



        }
    }
}
