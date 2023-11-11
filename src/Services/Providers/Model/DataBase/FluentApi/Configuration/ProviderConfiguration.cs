using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Providers.Model.Entity;

namespace Providers.Model.DataBase.FluentApi.Configuration
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(primalKey => primalKey.Id);
            builder.Property<string>(name => name.Name);
        }
    }
}
