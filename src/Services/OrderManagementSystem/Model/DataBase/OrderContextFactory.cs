using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OrderManagementSystem.Model.DataBase
{
    public class OrderContextFactory : IDesignTimeDbContextFactory<OrderDB>
    {
        public OrderDB CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<OrderDB>();
            ConfigurationBuilder builder = new();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot configurationRoot = builder.Build();

            string conntectionString = configurationRoot.GetConnectionString("orderConnection");
            options.UseSqlServer(conntectionString);
            return new OrderDB(options.Options);
        }
    }
}
