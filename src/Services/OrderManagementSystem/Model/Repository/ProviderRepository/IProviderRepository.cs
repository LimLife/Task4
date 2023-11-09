using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Model.Repository.ProviderRepository
{
    public interface IProviderRepository
    {
        public Task<Provider?> GetProviderByIdAsync(int id);
        public Task<List<Provider>?> GetProvidersAsync(); 
    }
}
