using OrderManagementSystem.Model.EntityDTO;
using OrderManagementSystem.Model.DataBase;
using OrderManagementSystem.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementSystem.Model.Repository
{
    public class Repository : IRepository
    {
        private readonly OrderDB _context;
        public Repository(OrderDB context) => _context = context;


        #region Provider
        public async Task<List<Provider>?> GetProvidersAsync() =>
            await _context
            .Providers
            .AsNoTracking()
            .ToListAsync();
        #endregion
        #region Order  
        public async Task<Order?> GetOrderByIdAsync(int id) =>
            await _context
            .Orders
            .AsNoTracking()
            .Include(provider => provider.Provider)
            .FirstOrDefaultAsync(o => o.Id == id);
        public async Task<List<Order>?> GetOrdersAsync() =>
            await _context
            .Orders
            .AsNoTracking()
            .Include(provider => provider.Provider)
            .ToListAsync();
        public async Task<bool> IsContainsNameInOrderAsync(int orderItemId, string nameToItemOrder)
        {
            var item = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == orderItemId);
            if (item is not null && item.Number == nameToItemOrder)
                return true;
            return false;
        }
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
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        #endregion
        #region OrderItem
        public async Task<List<OrderItem>?> GetOrdersItemByOrderIdAsync(int id) =>
            await _context
            .OrderItems
            .AsNoTracking()
            .Where(item => item.Order.Id == id)
            .ToListAsync();
        public async Task<List<OrderItem>?> GetOrdersItemsAsync() =>
           await _context
           .OrderItems
           .AsNoTracking()
           .ToListAsync();
        public async Task<OrderItem?> GetOrderItemByIdAsync(int id) =>
            await _context
            .OrderItems
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);
        public async Task<OrderItem?> CreateOrderItemAsync(OrderItem item)
        {
            if (item is null) return null;
            _context.Entry<OrderItem>(item).State = EntityState.Added;
            await _context.SaveChangesAsync();
            var createdItem = await _context
                .OrderItems
                .AsNoTracking()
                .FirstOrDefaultAsync(id => id.Id == item.Id);
            if (createdItem is null) return null;
            return createdItem;
        }
        public async Task<OrderItem?> UpdateOrderItemAsync(OrderItem item)
        {
            if (item is null) return null;
            _context.Entry<OrderItem>(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var createdItem = await _context
                .OrderItems
                .AsNoTracking()
                .FirstOrDefaultAsync(id => id.Id == item.Id);
            if (createdItem is null) return null;
            return createdItem;
        }
        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var item = new OrderItem() { Id = id };
            _context.Entry<OrderItem>(item).State = EntityState.Deleted;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        #endregion
        #region Filter    
        public async Task<List<Order>?> GetOrdersByFilterAsync(Filter filter) =>
            await _context
            .Orders
            .AsNoTracking()
            .Include(e => e.Provider)
            .ThenInclude(item => item.Orders)
            .Where(order =>
            (string.IsNullOrEmpty(filter.Number) || order.Number == filter.Number)
            && order.ProviderId == filter.ProviderID
            && (filter.Start == DateTime.MinValue || order.Date >= filter.Start)
            && (filter.End == DateTime.MinValue || order.Date <= filter.End)
            && (string.IsNullOrEmpty(filter.Name) || order.OrderItems.All(item => item.Name == filter.Name))
            && (string.IsNullOrEmpty(filter.Unit) || order.OrderItems.All(item => item.Unit == filter.Unit)))
            .ToListAsync();
        #endregion
        public async Task<bool> IsConnectAsync() =>
           await _context.Database.CanConnectAsync();
    }
}
