using ItemManagementSystem.Model.DataBase.FluentApi.Configuration;
using ItemManagementSystem.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace ItemManagementSystem.Model.DataBase
{
    public class OrderItemDB : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OrderItemDB(DbContextOptions<OrderItemDB> op) : base(op) { }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        }
    }
}
