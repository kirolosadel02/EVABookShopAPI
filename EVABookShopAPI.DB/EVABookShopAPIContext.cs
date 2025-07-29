using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace EVABookShopAPI.DB
{
    public class EVABookShopAPIContext : DbContext
    {
        public EVABookShopAPIContext(DbContextOptions<EVABookShopAPIContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Seed data using the seeder class
            DataSeeder.SeedData(modelBuilder);
        }
    }
}
