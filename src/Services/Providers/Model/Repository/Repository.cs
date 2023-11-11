using Microsoft.EntityFrameworkCore;
using Providers.Model.DataBase;
using Providers.Model.Entity;

namespace Providers.Model.Repository
{
    public class Repository : IRepository
    {
        private readonly ProviderDB _context;
        public Repository(ProviderDB context) => _context = context;

        public async Task<Provider?> ProviderByIdAsync(int id) =>
            await _context
            .Providers
            .AsNoTracking()
            .FirstOrDefaultAsync(idProvider=>idProvider.Id == id);

        public async Task<Provider?> ProviderByNameAsync(string name)=>
            await _context
            .Providers
            .AsNoTracking()
            .FirstOrDefaultAsync(provider=>provider.Name == name);
      
        public async Task<List<Provider>?> ProvidersAsync()=>
            await _context
            .Providers
            .AsNoTracking()
            .ToListAsync();
       
    }
}
