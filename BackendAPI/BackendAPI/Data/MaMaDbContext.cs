using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Data
{
    public class MaMaDbContext(DbContextOptions<MaMaDbContext> options) : DbContext(options)
    {
        // lav en test tabel og se om den kommer ned på databasen 

        public DbSet<Hairdresser> Hairdressers => Set<Hairdresser>();

        // dummy data - test if connection to DB
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hairdresser>().HasData(
                new Hairdresser { ID = 1},
                new Hairdresser { ID = 2}
                );
        }
    }
}
