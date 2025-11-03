using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Data
{
    public class MaMaDbContext(DbContextOptions<MaMaDbContext> options) : DbContext(options)
    {
        // tabels on the DB
        public DbSet<Hairdresser> Hairdressers => Set<Hairdresser>();

        // dummy data - test for connection to DB
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hairdresser>().HasData(
                new Hairdresser
                {
                    ID = 1,
                    SalonName = "Hairværk",
                    Website = "https://hairvaerkvejle.dk/",
                    Lat = 55.710032, 
                    Lng = 9.534729
                },
                new Hairdresser
                {
                    ID = 2,
                    SalonName = "Salon No. 24", 
                    Website = "https://salonno24.dk/",
                    Lat = 55.612773,
                    Lng = 9.566805
                },
                new Hairdresser
                {
                    ID = 3,
                    SalonName = "Frisør PARK Styling Vejle",
                    Website = "https://parkstyling.dk/pages/salon/vejle",
                    Lat = 55.711891,
                    Lng = 9.536530
                },
                new Hairdresser
                {
                    ID = 4,
                    SalonName = "City Frisørerne",
                    Website = "https://parkstyling.dk/pages/salon/vejle-city",
                    Lat = 55.710152, 
                    Lng = 9.535270
                });
        }
    }
}
