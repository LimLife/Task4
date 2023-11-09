using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Model.DataBase.FluentApi.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(prymalKey => prymalKey.Id);
            builder.Property<string>(number => number.Number).HasColumnType("nvarchar(max)");
            builder.HasOne(provider => provider.Provider).WithMany();
            builder.Property<DateTime>(date => date.Date).HasColumnType("datetime2(7)");
        }
    }
}
