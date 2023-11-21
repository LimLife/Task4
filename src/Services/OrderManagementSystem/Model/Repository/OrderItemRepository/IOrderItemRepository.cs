using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Model.Repository.OrderItemRepository
{
    public interface IOrderItemRepository
    {
        public Task<List<OrderItem>?> GetOrdersItemsAsync();
        public Task<List<OrderItem>?> GetOrdersItemByOrderIdAsync(int id);
        public Task<OrderItem?> GetOrderItemByIdAsync(int id);
        public Task<OrderItem?> CreateOrderItemAsync(OrderItem item);
        public Task<OrderItem?> TryUpdateOrderItemAsync(OrderItem item);
        public Task<bool> TryDeleteOrderItemAsync(int id);
    }
}
