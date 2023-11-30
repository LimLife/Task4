using OrderManagementSystem.Model.Repository.OrderRepository;
using OrderManagementSystem.Grpc.OrderService;
using OrderManagementSystem.Model.Repository;
using OrderManagementSystem.Model.Entity;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace OrderManagementSystem.Services
{
    public class OrderApiService : OrderService.OrderServiceBase
    {
        private readonly IOrderRepository _repository;
        public OrderApiService(IRepository repository)
        {
            _repository = repository;
        }
        public override async Task<ListOrderRiply> GetListOrders(Empty request, ServerCallContext context)
        {
            try
            {
                return await _repository.GetOrdersAsync() ?? throw new RpcException(new Status(StatusCode.NotFound, $"Orders not found"));
            }
            catch (Exception)
            {
                return new ListOrderRiply();
            }
        }
        public override async Task<BoolValue> IsContainNumberOrder(IsConstainStringOrderRequest request, ServerCallContext context)
        {
            if (request is null || string.IsNullOrEmpty(request.Str))
            {
                _ = new RpcException(new Status(StatusCode.NotFound, $"Order by Id {request.IdOrder}"));
            }
            try
            {
                var result = await _repository.IsContainsNameInOrderAsync(request.IdOrder, request.Str);
                return new BoolValue { Value = result };
            }
            catch (Exception)
            {
                return new BoolValue { Value = false };
            }
        }
        public override async Task<BoolValue> IsContainProviderOrder(IsConstainProviderInOrderRequest request, ServerCallContext context)
        {
            if (request is null || string.IsNullOrEmpty(request.Number))
            {
                _ = new RpcException(new Status(StatusCode.NotFound, $"Order by Id {request.Number}"));
            }
            try
            {
                var result = await _repository.IsContainsProviderInOrderAsync(request.ProviderId, request.Number);
                return new BoolValue { Value = result };
            }
            catch (Exception)
            {
                return new BoolValue { Value = false };
            }
        }

        public override async Task<OrderReply> GetOrder(GetOrderRequest request, ServerCallContext context)
        {
            try
            {
                return await _repository.GetOrderByIdAsync(request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Order with id: {request.Id}"));
            }
            catch (Exception)
            {
                return new OrderReply();
            }
        }
        public override async Task<OrderReply> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            try
            {
                var order = new Order
                {
                    Number = request.Number,
                    Provider = request.Provider,
                    Date = request.Date.ToDateTime(),
                };
                return await _repository.TryCreateOrderAsync(order) ?? throw new RpcException(new Status(StatusCode.Unknown, "Internal exception"));
            }
            catch (Exception)
            {
                return new OrderReply();
            }
        }
        public override async Task<OrderReply> UpdateOrder(UpdateOrderRequest request, ServerCallContext context)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            try
            {
                var order = new Order
                {
                    Id = request.Id,
                    Number = request.Number,
                    Provider = request.Provider,
                    Date = request.Date.ToDateTime()
                };
                return await _repository.TryUpdateOrderAsync(order) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Element with id: {request.Id}"));
            }
            catch (Exception)
            {
                return new OrderReply();
            }
        }
        public override async Task<BoolValue> DeleteOrder(DeleteOrderRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _repository.TryDeleteOrderAsync(request.Id);
                return new BoolValue() { Value = result };

            }
            catch (Exception)
            {
                return new BoolValue() { Value = false };
            }
        }

    }
}
