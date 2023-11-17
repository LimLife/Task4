using OrderManagementSystem.Model.DataBase.FluentApi.Configuration;
using OrderManagementSystem.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementSystem.Model.DataBase
{
    public class OrderDB : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public OrderDB(DbContextOptions<OrderDB> op) : base(op) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());


            //Add beginning data
            modelBuilder.Entity<Provider>().HasData(new List<Provider> {
                new Provider { Id = 1, Name = "At Alesya"},
                new Provider { Id = 2, Name = "Golden Path"},
                new Provider { Id = 3, Name = "Lizards"},
                new Provider { Id = 4, Name = "Bagdan Inc"},
                new Provider { Id = 5, Name = "Ancient Rus"},
                new Provider { Id = 6, Name = "The Beast Company" },
                new Provider { Id = 7, Name = "Trade Federation of Planets"},
                new Provider { Id = 8, Name = "Another beast Company"},
                new Provider { Id = 9, Name = "Horns and hooves"},
                new Provider { Id = 10, Name = "Midas"}
            });
        }
    }
}
