using ItemManagementSystem.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemManagementSystem.Model.DataBase.FluentApi.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(primalKey => primalKey.Id);
            builder.HasMany(item => item.Items).WithOne(order => order.Order);
        }
    }
}
