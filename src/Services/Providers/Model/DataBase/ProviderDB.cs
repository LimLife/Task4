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
                new Provider() {  Name = "Oleg and K.O"},
                new Provider() {  Name = "Magomed Inc"},
                new Provider() {  Name = "At Olesya's"},
                new Provider() {  Name = "At Home"},
                new Provider() {  Name = "Horns and hooves"},
                new Provider() {  Name = "The best company"},
                new Provider() {  Name = "Another best company"},
                new Provider() {  Name = "Midass"},
                new Provider() {  Name = "Golden Path"},
                new Provider() {  Name = "Fun beavers"},
                new Provider() {  Name = "Pi 3.14"}
            });

        }


    }
}
