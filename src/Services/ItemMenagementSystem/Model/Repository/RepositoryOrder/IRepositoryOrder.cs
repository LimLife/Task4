using ItemManagementSystem.Model.Entity;

namespace ItemManagementSystem.Model.Repository.RepositoryOrder
{
    public interface IRepositoryOrder
    {
        public Task<Order?> GetOrderByIdAsync(int id);
        public Task<List<Order>?> GetOrdersAsync();
    }
}
