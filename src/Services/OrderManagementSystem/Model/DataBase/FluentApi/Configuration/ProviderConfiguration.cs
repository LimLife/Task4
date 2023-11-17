using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementSystem.Model.DataBase.FluentApi.Configuration
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable(nameof(Provider));
            builder.HasKey(primalKey => primalKey.Id);
            builder.Property<string>(name => name.Name).HasColumnType("nvarchar(max)");
        }
    }
}
