using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Model.DataBase;
using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Model.Repository
{
    public class Repository : IRepository //fix Create needed return new item with id
    {
        private readonly OrderDB _context;
        public Repository(OrderDB context) => _context = context;


        public async Task<Order?> GetOrderByIdAsync(int id) =>
            await _context
            .Orders
            .AsNoTracking()
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
            return order;
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
            var isOrder = _context.Orders.Any(o => o.Id == id);
            if (!isOrder) return false;

            var order = new Order { Id = id };
            _context.Entry<Order>(order).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
