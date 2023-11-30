using OrderManagementSystem.Model.Repository.OrderItemRepository;
using ItemManagementSystem.Grpc.OrderItemService;
using OrderManagementSystem.Model.Repository;
using OrderManagementSystem.Model.Entity;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace OrderManagementSystem.Services
{
    public class OrderItemApiService : OrderItemService.OrderItemServiceBase
    {
        private readonly IOrderItemRepository _repositorty;
        public OrderItemApiService(IRepository repository) => _repositorty = repository;

        public override async Task<ListOrderItemReply> GetListOrderItemsByOrderId(GetListOrderItemsByOrderIdRequest request, ServerCallContext context)
        {
            try
            {
                return await _repositorty.GetOrdersItemByOrderIdAsync(request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Element with id: {request.Id}"));
            }
            catch (Exception)
            {
                return new ListOrderItemReply();
            }
        }
        public override async Task<ListOrderItemReply> GetListOrder(Empty request, ServerCallContext context)
        {
            try
            {
                return await _repositorty.GetOrdersItemsAsync() ?? throw new RpcException(new Status(StatusCode.NotFound, $"Elements not found"));
            }
            catch (Exception)
            {
                return new ListOrderItemReply();
            }
        }
        public override async Task<OrderItemReply> CreateOrderItem(CreateOrderItemReques request, ServerCallContext context)
        {
            try
            {
                return await _repositorty.CreateOrderItemAsync(new OrderItem
                {
                    Name = request.Name,
                    Quantity = request.Quantity,
                    Unit = request.Unit,
                    OrderId = request.Order
                }) ?? throw new RpcException(new Status(StatusCode.NotFound, "Can`t create"));
            }
            catch (Exception)
            {
                return new OrderItemReply();
            }
        }
        public override async Task<OrderItemReply> UpdateOrderItem(UpdateOrderItemReques request, ServerCallContext context)
        {
            try
            {
                return await _repositorty.TryUpdateOrderItemAsync(new OrderItem
                {
                    Id = request.Id,
                    Name = request.Name,
                    Quantity = request.Quantity,
                    Unit = request.Unit,
                }) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Elements not found"));
            }
            catch (Exception)
            {
                return new OrderItemReply();
            }

        }
        public override async Task<BoolValue> DeleteOrderItem(DeleteOrderItemReques request, ServerCallContext context)
        {
            try
            {
                var result = await _repositorty.TryDeleteOrderItemAsync(request.Id);
                if (!result)
                    throw new RpcException(new Status(StatusCode.NotFound, "Element not Found or any exception"));
                return new BoolValue() { Value = result };
            }
            catch (Exception)
            {
                return new BoolValue() { Value = false };
            }
        }
    }
}
