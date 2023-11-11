using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;


namespace Providers.Model.DataBase
{
    public class ProviderContextFactory : IDesignTimeDbContextFactory<ProviderDB>
    {
        public ProviderDB CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<ProviderDB>();
            ConfigurationBuilder builder = new();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot configurationRoot = builder.Build();

            string conntectionString = configurationRoot.GetConnectionString("orderDB");
            options.UseSqlite(conntectionString);
            return new ProviderDB(options.Options);
        }
    }
}
