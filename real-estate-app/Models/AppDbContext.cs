using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace real_estate_app.Models
{
    
        public class AppDbContext : DbContext
        {
           

            public DbSet<Emlak> Emlaklar { get; set; }
            public DbSet<Satici> Saticilar { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql("server=localhost;port=5432;database=real-estate-app;user id=postgres;password=Recep.2003;");
            
            }
    }
    
}
