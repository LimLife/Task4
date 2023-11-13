using OrderManagementSystem.Model.DataBase;
using OrderManagementSystem.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementSystem.Model.Repository
{
    public class Repository : IRepository
    {
        private readonly OrderDB _context;
        public Repository(OrderDB context) => _context = context;


        public async Task<Order?> GetOrderByIdAsync(int id) =>
            await _context
            .Orders
            .AsNoTracking()
            .Include(provider => provider.Provider)
            .FirstOrDefaultAsync(o => o.Id == id);

        public async Task<Provider?> GetProviderByIdAsync(int id) =>
            await _context
            .Providers
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);


        public async Task<List<Order>?> GetOrdersAsync() =>
            await _context
            .Orders
            .AsNoTracking()
            .Include(provider => provider.Provider)
            .ToListAsync();


        public async Task<List<Provider>?> GetProvidersAsync() =>
            await _context
            .Providers
            .AsNoTracking()
            .ToListAsync();


        public async Task<Order?> TryCreateOrderAsync(Order order)
        {
            if (order is null) return null;
            _context.Entry<Order>(order).State = EntityState.Added;
            await _context.SaveChangesAsync();
            var created = await _context.Orders.Include(provider => provider.Provider).FirstOrDefaultAsync(id => id.Id == order.Id);
            if (created is null) return null;
            return created;
        }

        public async Task<Order?> TryUpdateOrderAsync(Order order)
        {
            if (order is null) return null;
            _context.Entry<Order>(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task<bool> TryDeleteOrderAsync(int id)
        {
            var order = new Order { Id = id };
            _context.Entry<Order>(order).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            var isOrder = _context.Orders.Any(o => o.Id == id);
            return !isOrder;
        }

        public async Task<bool> IsConnectAsync()
        {
            return await _context.Database.CanConnectAsync();
        }
    }
}
