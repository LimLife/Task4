using OrderManagementSystem.Model.EntityDTO;
using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Model.Repository.OrderItemRepository
{
    public interface IOrderItemRepository
    {
        public Task<OrderItem?> GetOrderItemByIdAsync(int id);
        public Task<List<OrderItem>?> GetOrdersItemsAsync();    
        public Task<OrderItem?> CreateOrderItemAsync(OrderItem item);
        public Task<OrderItem?> UpdateOrderItemAsync(OrderItem item);
        public Task<bool> DeleteOrderItemAsync(int id);
    }
}
