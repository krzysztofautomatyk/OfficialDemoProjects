using Microsoft.EntityFrameworkCore;

namespace WebApplication_Basic_EntiyFrameworkCore.Entities
{
    public class ApiDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Basic_EF_DB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        
    }
}
