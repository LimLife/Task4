using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Model.Repository.ProviderRepository
{
    public interface IProviderRepository
    {
        public Task<List<Provider>?> GetProvidersAsync();
    }
}
