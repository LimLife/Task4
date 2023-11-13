using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ItemManagementSystem.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace ItemManagementSystem.Model.DataBase.FluentApi.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(primalKey => primalKey.Id);
            builder.HasOne(orderId => orderId.OrderId).WithMany(order => order.Items);
            builder.Property<string>(name => name.Name).HasColumnType("nvarchar(max)");
            builder.Property<decimal>(quantity => quantity.Quantity).HasColumnType("decimal(18,3)");
            builder.Property<string>(unit => unit.Unit).HasColumnType("nvarchar(max)");
        }
    }
}

