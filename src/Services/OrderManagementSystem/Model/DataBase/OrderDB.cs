using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Model.DataBase.FluentApi.Configuration;
using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Model.DataBase
{
    public class OrderDB : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public OrderDB(DbContextOptions<OrderDB> op) : base(op) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderConfiguration());
        }
    }
}
