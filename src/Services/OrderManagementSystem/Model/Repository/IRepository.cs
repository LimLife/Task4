using OrderManagementSystem.Model.Repository.OrderItemRepository;
using OrderManagementSystem.Model.Repository.ProviderRepository;
using OrderManagementSystem.Model.Repository.FilterRepository;
using OrderManagementSystem.Model.Repository.OrderRepository;

namespace OrderManagementSystem.Model.Repository
{
    public interface IRepository : IOrderRepository, IProviderRepository, IOrderItemRepository, IFilterRepository
    {
        public Task<bool> IsConnectAsync();
    }
}
