using ItemManagementSystem.Model.DataBase;
using ItemManagementSystem.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace ItemManagementSystem.Model.Repository
{
    public class Repository : IRepository
    {
        private readonly OrderItemDB _context;
        public Repository(OrderItemDB context) => _context = context;

        public async Task<List<OrderItem>?> GetOrdersItemsAsync() =>
            await _context
            .OrderItems
            .AsNoTracking()
            .ToListAsync();

        public async Task<Order?> GetOrderByIdAsync(int id) =>
            await _context
            .Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);

        public async Task<List<Order>?> GetOrdersAsync() =>
            await _context
            .Orders
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
            var changes = await _context.SaveChangesAsync();
            var createdItem = await _context
                .OrderItems
                .AsNoTracking()
                .FirstOrDefaultAsync(id => id.Id == item.Id);
            if (createdItem is null || changes == 0) return null;
            return createdItem;
        }

        public async Task<OrderItem?> UpdateOrderItemAsync(OrderItem item)
        {
            if (item is null) return null;
            _context.Entry<OrderItem>(item).State = EntityState.Modified;
            var changes = await _context.SaveChangesAsync();//????

            var createdItem = await _context
                .OrderItems
                .AsNoTracking()
                .FirstOrDefaultAsync(id => id.Id == item.Id);
            if (createdItem is null) return null;
            return createdItem;
        }
        public Task<bool> DeleteOrderItemAsync(int id)
        {
            var item = new OrderItem() { Id = id };
            _context.Entry<OrderItem>(item).State = EntityState.Deleted;
            var isEntity = _context
                .OrderItems
                .Any(entity => item.Id == entity.Id);
            return Task.FromResult(!isEntity);
        }

        public async Task<bool> IsConnectAsync()
        {
            return await _context.Database.CanConnectAsync();
        }
    }
}
