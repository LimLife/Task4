using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementSystem.Model.DataBase.FluentApi.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable(nameof(OrderItem));
            builder.HasKey(primalKey => primalKey.Id);
            builder.HasOne(orderId => orderId.Order).WithMany(order => order.OrderItems);
            builder.Property<string>(name => name.Name).HasColumnType("nvarchar(max)");
            builder.Property<decimal>(quantity => quantity.Quantity).HasColumnType("decimal(18,3)");
            builder.Property<string>(unit => unit.Unit).HasColumnType("nvarchar(max)");
        }
    }
}
