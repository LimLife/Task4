using OrderManagementSystem.Model.EntityDTO;
using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Model.Repository.FilterRepository
{
    public interface IFilterRepository
    {
        public Task<List<Order>?> GetOrdersByFilterAsync(Filter filter);       

    }
}
