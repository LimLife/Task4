using OrderManagementSystem.Model.Repository.OrderItemRepository;
using ItemManagementSystem.Grpc.OrderItemService;
using OrderManagementSystem.Model.Repository;
using OrderManagementSystem.Tools;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Services
{
    public class OrderItemApiService : OrderItemService.OrderItemServiceBase
    {
        private readonly IOrderItemRepository _repositorty;
        public OrderItemApiService(IRepository repository) => _repositorty = repository;

        public override async Task<ListOrderItemReply> GetListOrderItemsByOrderId(GetListOrderItemsByOrderIdRequest request, ServerCallContext context)
        {
            var items = await _repositorty.GetOrdersItemByOrderIdAsync(request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Element with id: {request.Id}"));
            var reply = new ListOrderItemReply();
            var replyList = items.Select(item => new OrderItemReply
            {
                Id = item.Id,
                Name = item.Name,
                Unit = item.Unit,
                Quantity = RpcCovert.GetReplyDecimal(item.Quantity),
            });
            reply.Order.AddRange(replyList);
            return reply;
        }
        public override async Task<ListOrderItemReply> GetListOrder(Empty request, ServerCallContext context)
        {
            var items = await _repositorty.GetOrdersItemsAsync() ?? throw new RpcException(new Status(StatusCode.NotFound, $"Elements not found"));
            var reply = new ListOrderItemReply();
            var replyList = items.Select(item => new OrderItemReply
            {
                Id = item.Id,
                Name = item.Name,
                Unit = item.Unit,
                Quantity = RpcCovert.GetReplyDecimal(item.Quantity),
            });
            reply.Order.AddRange(replyList);
            return reply;
        }
        public override async Task<OrderItemReply> CreateOrderItem(CreateOrderItemReques request, ServerCallContext context)
        {
            var item = await _repositorty.CreateOrderItemAsync(new OrderItem
            {
                Name = request.Name,
                Quantity = RpcCovert.GetDecimal(request.Quantity),
                Unit = request.Unit,
                OrderId = request.Order
            }) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Elements not found"));
            return new OrderItemReply { Id = item.Id, Name = item.Name, Quantity = RpcCovert.GetReplyDecimal(item.Quantity), Unit = item.Unit };
        }
        public override async Task<OrderItemReply> UpdateOrderItem(UpdateOrderItemReques request, ServerCallContext context)
        {
            var item = await _repositorty.UpdateOrderItemAsync(new OrderItem
            {
                Id = request.Id,
                Name = request.Name,
                Quantity = RpcCovert.GetDecimal(request.Quantity),
                Unit = request.Unit
            }) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Elements not found"));

            return new OrderItemReply { Id = item.Id, Name = item.Name, Quantity = RpcCovert.GetReplyDecimal(item.Quantity), Unit = item.Unit };
        }
        public override async Task<BoolValue> DeleteOrderItem(DeleteOrderItemReques request, ServerCallContext context)
        {
            var result = await _repositorty.DeleteOrderItemAsync(request.Id);
            if (!result)
                throw new RpcException(new Status(StatusCode.NotFound, "Element not Found or any exception"));
            return new BoolValue() { Value = result };
        }
    }
}
