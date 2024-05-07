using System.Data.Entity;

namespace _4_Cars {
    public class SamochodDB : DbContext{
        public DbSet<Samochod> Samochody { get; set;}
    }
}
