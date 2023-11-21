using OrderManagementSystem.Model.Repository.OrderRepository;
using OrderManagementSystem.Grpc.OrderService;
using OrderManagementSystem.Model.Repository;
using OrderManagementSystem.Model.Entity;

using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

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
            var listReply = new ListOrderRiply();
            var items = await _repository.GetOrdersAsync() ?? throw new RpcException(new Status(StatusCode.NotFound, $"Orders not found"));
            var reply = items.Select(item => new OrderReply
            {
                Id = item.Id,
                Number = item.Number,
                Date = item.Date.ToUniversalTime().ToTimestamp(),
                Provider = item.Provider
            });
            listReply.Orders.AddRange(reply);
            return listReply;
        }
        public override async Task<BoolValue> IsConstainNumberOrder(IsConstainStringOrderRequest request, ServerCallContext context)
        {
            if (request is null || string.IsNullOrEmpty(request.Str))
            {
                _ = new RpcException(new Status(StatusCode.NotFound, $"Order by Id {request.IdOrder}"));
            }
            var result = await _repository.IsContainsNameInOrderAsync(request.IdOrder, request.Str);
            return new BoolValue { Value = result };
        }
        public override async Task<BoolValue> IsConstainProviderOrder(IsConstainProviderInOrderRequest request, ServerCallContext context)
        {
            if (request is null || string.IsNullOrEmpty(request.Number))
            {
                _ = new RpcException(new Status(StatusCode.NotFound, $"Order by Id {request.Number}"));
            }
            var result = await _repository.IsContainsProviderInOrderAsync(request.ProviderId, request.Number);
            return new BoolValue { Value = result };
        }

        public override async Task<OrderReply> GetOrder(GetOrderRequest request, ServerCallContext context)
        {
            var item = await _repository.GetOrderByIdAsync(request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Order with id: {request.Id}"));
            return new OrderReply
            {
                Id = item.Id,
                Number = item.Number,
                Date = item.Date.ToUniversalTime().ToTimestamp(),
                Provider = item.Provider,
            };
        }
        public override async Task<OrderReply> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var order = new Order
            {
                Number = request.Number,
                Provider = request.Provider,
                Date = request.Date.ToDateTime()
            };
            var createdItem = await _repository.TryCreateOrderAsync(order) ?? throw new RpcException(new Status(StatusCode.Unknown, "Internal exception"));
            return new OrderReply
            {
                Id = createdItem.Id,
                Number = createdItem.Number,
                Date = createdItem.Date.ToUniversalTime().ToTimestamp()
            };
        }
        public override async Task<OrderReply> UpdateOrder(UpdateOrderRequest request, ServerCallContext context)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            var order = new Order
            {
                Id = request.Id,
                Number = request.Number,
                Provider = request.Provider,
                Date = request.Date.ToDateTime()
            };
            var update = await _repository.TryUpdateOrderAsync(order) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Element with id: {request.Id}"));
            return new OrderReply
            {
                Id = update.Id,
                Number = update.Number,
                Date = update.Date.ToUniversalTime().ToTimestamp()
            };
        }
        public override async Task<BoolValue> DeleteOrder(DeleteOrderRequest request, ServerCallContext context)
        {
            var result = await _repository.TryDeleteOrderAsync(request.Id);
            return new BoolValue() { Value = result };
        }

    }
}
