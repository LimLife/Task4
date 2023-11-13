﻿using Providers.Model.Entity;

namespace Providers.Model.Repository
{
    public interface IRepository
    {
        public Task<Provider?> ProviderByIdAsync(int id);
        public Task<Provider?> ProviderByNameAsync(string name);
        public Task<List<Provider>?> ProvidersAsync();
        public Task<bool> IsConnectAsync();
    }
}
