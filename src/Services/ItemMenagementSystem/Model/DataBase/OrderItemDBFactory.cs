using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ItemManagementSystem.Model.DataBase
{
    public class OrderItemDBFactory : IDesignTimeDbContextFactory<OrderItemDB>
    {
        public OrderItemDB CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<OrderItemDB>();
            ConfigurationBuilder builder = new();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot configurationRoot = builder.Build();

            string conntectionString = configurationRoot.GetConnectionString("orderDB");
            options.UseNpgsql(conntectionString);
            return new OrderItemDB(options.Options);
        }
    }
}
