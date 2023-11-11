using Providers.Model.DataBase;
using Providers.Model.Entity;

namespace Providers.Model.Repository
{
    public class Repository : IRepository
    {
        private readonly ProviderDB _context;
        public Repository(ProviderDB context) => _context = context;

        public Task<Provider?> ProviderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Provider>?> ProvidersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
