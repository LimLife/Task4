using OrderManagementSystem.Model.Repository.ProviderRepository;
using OrderManagementSystem.Model.Repository.OrderRepository;

namespace OrderManagementSystem.Model.Repository
{
    public interface IRepository : IOrderRepository, IProviderRepository
    {
        public Task<bool> IsConnectAsync();
    }
}
