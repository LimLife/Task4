using Microsoft.EntityFrameworkCore;
using Providers.Model.DataBase.FluentApi.Configuration;
using Providers.Model.Entity;

namespace Providers.Model.DataBase
{
    public class ProviderDB : DbContext
    {
        public DbSet<Provider> Providers { get; set; }
        public ProviderDB(DbContextOptions<ProviderDB> op) : base(op) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProviderConfiguration());

            modelBuilder.Entity<Provider>().HasData(new List<Provider>()
            {
                new Provider() { Id =1, Name = "Oleg and K.O"},
                new Provider() { Id=2, Name = "Magomed Inc"},
                new Provider() { Id= 3, Name = "At Olesya's"},
                new Provider() { Id=4, Name = "At Home"},
                new Provider() { Id = 5, Name = "Horns and hooves"},
                new Provider() { Id= 6, Name = "The best company"},
                new Provider() { Id=7, Name = "Another best company"},
                new Provider() { Id =8, Name = "Midass"},
                new Provider() { Id =9, Name = "Golden Path"},
                new Provider() { Id=10, Name = "Fun beavers"},
                new Provider() { Id =11, Name = "Pi 3.14"}
            });

        }


    }
}
