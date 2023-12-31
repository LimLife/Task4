﻿using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Model.Repository.OrderRepository
{
    public interface IOrderRepository
    {
        public Task<List<Order>?> GetOrdersAsync();
        public Task<Order?> GetOrderByIdAsync(int id);
        public Task<bool> IsContainsProviderInOrderAsync(int providerId, string nameOrder);
        public Task<bool> IsContainsNameInOrderAsync(int orderItemId, string nameToItemOrder);
        public Task<Order?> TryCreateOrderAsync(Order order);
        public Task<Order?> TryUpdateOrderAsync(Order order);
        public Task<bool> TryDeleteOrderAsync(int id);
    }
}
