using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Model.Repository.ProviderRepository
{
    public interface IProviderRepository
    {
        public Task<Provider?> ProviderByIdAsync(int id);
        public Task<Provider?> ProviderByNameAsync(string name);
        public Task<List<Provider>?> ProvidersAsync();
    }
}
