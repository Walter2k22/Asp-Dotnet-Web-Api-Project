using Microsoft.EntityFrameworkCore;
using SPO_Data.Models;


namespace SPO_Data.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Student> Studenti { get; set; }
        public DbSet<Predmet> Predmeti { get; set; }
        public DbSet<Ocjena> Ocjene { get; set; }
    }
}
