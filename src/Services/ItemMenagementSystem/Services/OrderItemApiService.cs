using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using ItemManagementSystem.Grpc.OrderItem;
using ItemManagementSystem.Model.Repository;

namespace ItemManagementSystem.Services
{
    public class OrderItemApiService : OrderItemService.OrderItemServiceBase
    {
        private readonly IRepository _repository;
        public OrderItemApiService(IRepository repository) => _repository = repository;

        public override Task<OrderItemReply> CreateOrderItem(CreateOrderItemReques request, ServerCallContext context)
        {
            return base.CreateOrderItem(request, context);
        }
        public override Task<ListReply> GetListOrder(Empty request, ServerCallContext context)
        {
            return base.GetListOrder(request, context);
        }
        public override Task<OrderItemReply> GetOrderItem(GetOrderItemReques request, ServerCallContext context)
        {
            return base.GetOrderItem(request, context);
        }
        public override Task<OrderItemReply> UpdateOrderItem(UpdateOrderItemReques request, ServerCallContext context)
        {
            return base.UpdateOrderItem(request, context);
        }
        public override Task<BoolValue> DeleteOrderItem(DeleteOrderItemReques request, ServerCallContext context)
        {
            return base.DeleteOrderItem(request, context);
        }
        
    }
}
