using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Model.DataBase.FluentApi.Configuration
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(primalKey=>primalKey.Id);
            builder.Property<int>(providerId=>providerId.Id);
        }
    }
}
