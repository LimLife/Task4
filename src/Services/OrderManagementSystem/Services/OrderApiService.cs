using OrderManagementSystem.Model.Repository.OrderRepository;
using OrderManagementSystem.Grpc.OrderService;
using OrderManagementSystem.Model.Repository;
using Google.Protobuf.WellKnownTypes;
using OrderManagementSystem.Tools;
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
        public override async Task<BoolValue> IsStringParameterNumber(IsStringParameterRequest request, ServerCallContext context)
        {
            if (request is null || string.IsNullOrEmpty(request.Str))
            {
                _ = new RpcException(new Status(StatusCode.NotFound, $"Order by Id {request.IdOrder}"));
            }
            var result = await _repository.IsContainsNameInOrderAsync(request.IdOrder, request.Str);
            return new BoolValue { Value = result };
        }
        public override async Task<OrderReply> GetOrder(GetOrderRequest request, ServerCallContext context)
        {
            var item = await _repository.GetOrderByIdAsync(request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Order with id: {request.Id}"));
            return RpcCovert.GetOrderReply(item);
        }
        public override async Task<ListOrderRiply> GetListOrders(Empty request, ServerCallContext context)
        {
            var listOrder = await _repository.GetOrdersAsync() ?? throw new RpcException(new Status(StatusCode.NotFound, "Not any order found"));
            return RpcCovert.GetOrderReply(listOrder);
        }
        public override async Task<OrderReply> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var order = RpcCovert.ConvertRequestToOrder(request);
            var createdItem = await _repository.TryCreateOrderAsync(order) ?? throw new RpcException(new Status(StatusCode.Unknown, "Internal exception"));
            return RpcCovert.GetOrderReply(createdItem);
        }
        public override async Task<OrderReply> UpdateOrder(UpdateOrderRequest request, ServerCallContext context)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            var order = RpcCovert.ConvertRequestToOrder(request);
            var update = await _repository.TryUpdateOrderAsync(order) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Element with id: {request.Id}"));
            return RpcCovert.GetOrderReply(update);
        }
        public override async Task<BoolValue> DeleteOrder(DeleteOrderRequest request, ServerCallContext context)
        {
            var result = await _repository.TryDeleteOrderAsync(request.Id);
            if (!result)
                throw new RpcException(new Status(StatusCode.NotFound, "Element not Found or any exception"));
            return new BoolValue() { Value = result };
        }

    }
}
