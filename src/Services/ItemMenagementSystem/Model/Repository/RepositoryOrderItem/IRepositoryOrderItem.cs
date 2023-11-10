using ItemManagementSystem.Model.Entity;

namespace ItemManagementSystem.Model.Repository.RepositoryOrderItem
{
    public interface IRepositoryOrderItem
    {
        public Task<OrderItem?> GetOrderItemByIdAsync(int id);
        public Task<List<OrderItem>?> GetOrdersItemsAsync();
        public Task<OrderItem?> CreateOrderItemAsync(OrderItem item);
        public Task<OrderItem?> UpdateOrderItemAsync(OrderItem item);
        public Task<bool> DeleteOrderItemAsync(int id);

    }
}
