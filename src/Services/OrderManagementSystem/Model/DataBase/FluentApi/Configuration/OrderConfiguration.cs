using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementSystem.Model.DataBase.FluentApi.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));
            builder.HasKey(prymalKey => prymalKey.Id);
            builder.Property<string>(number => number.Number).HasColumnType("nvarchar(max)");//nvarchar(max) for SqlServer 2byte varchar(max) PostgresSQL utf-8 or Unicode 1byte
            builder.HasOne(provider => provider.Provider).WithMany(order => order.Orders);
            builder.Property<DateTime>(date => date.Date).HasColumnType("datetime2(7)");
        }
    }
}
